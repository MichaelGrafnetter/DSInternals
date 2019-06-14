<#
.SYNOPSIS
Restores the {DCName} domain controller from ntds.dit.

.REMARKS
This script should only be executed on a freshly installed {OSName}. Use at your own risk.
The DSInternals PowerShell module must be installed for all users on the target server.


Author: Michael Grafnetter

#>
#Requires -Version 3 -Modules DSInternals,ServerManager,PSScheduledJob -RunAsAdministrator

# Perform a VSS backup before doing anything else.
Write-Host 'Creating a snapshot of the system drive to make rollback possible...'
$vssResult = ([wmiclass] 'Win32_ShadowCopy').Create("$env:SystemDrive\", 'ClientAccessible')

# The PS module must be present as workflows cannot contain non-existing activities.
Write-Host 'Installing the Active Directory module for Windows PowerShell...'
Add-WindowsFeature -Name RSAT-AD-PowerShell -ErrorAction Stop 

# All the other operations will be executed by a restartable workflow running in SYSTEM context.
Write-Host 'Registering restartable workflows...'

# Delete any pre-existing scheduled jobs with the same names before registering new ones.
Unregister-ScheduledJob -Name DSInternals-RFM-Initializer,DSInternals-RFM-Resumer -Force -ErrorAction SilentlyContinue

# The DSInternals-RFM-Initializer job will only be executed once in order to register the workflow and to invoke it for the first time.
$initTask = Register-ScheduledJob -Name DSInternals-RFM-Initializer -ScriptBlock {
    workflow Restore-DomainController
    {
        if ($env:COMPUTERNAME -ne '{DCName}')
        {
            # A server rename operation is required.
            Rename-Computer -NewName '{DCName}' -Force
            
            # We explicitly suspend the workflow as Restart-Computer with the -Wait parameter does not survive local reboots.
            shutdown.exe /r /t 5
            Suspend-Workflow -Label 'Waiting for reboot'
        }

        if ((Get-Service NTDS -ErrorAction SilentlyContinue) -eq $null)
        {
            # A DC promotion is required.
            # Note: In order to maintain compatibility with Windows Server 2008 R2, the ADDSDeployment module is not used.
            # Advice: It is recommenced to change the DSRM password after DC promotion.
            dcpromo.exe /unattend /ReplicaOrNewDomain:Domain /NewDomain:Forest /NewDomainDNSName:"{DomainName}" /DomainNetBiosName:"{NetBIOSDomainName}" /DomainLevel:{DomainMode} /ForestLevel:{ForestMode} '/SafeModeAdminPassword:"{DSRMPassword}"' /DatabasePath:"{TargetDBDirPath}" /LogPath:"{TargetLogDirPath}" /SysVolPath:"{TargetSysvolPath}" /AllowDomainReinstall:Yes /CreateDNSDelegation:No /DNSOnNetwork:No /InstallDNS:Yes /RebootOnCompletion:No
            Set-ItemProperty -Path registry::HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\ServerManager\Roles\10 -Name ConfigurationStatus -Value 2 -Force

            <# Alternative approach for Winows Server 2012+
            Install-WindowsFeature -Name AD-Domain-Services
            Install-ADDSForest -DomainName '{DomainName}' `
                               -DomainNetbiosName '{NetBIOSDomainName}' `
                               -ForestMode {ForestModeString} `
                               -DomainMode {DomainModeString} `
                               -DatabasePath "{TargetDBDirPath}" `
                               -LogPath "{TargetLogDirPath}" `
                               -SysvolPath "{TargetSysvolPath}" `
                               -InstallDns `
                               -CreateDnsDelegation:$false `
                               -NoDnsOnNetwork `
                               -SafeModeAdministratorPassword (ConvertTo-SecureString -String '{DSRMPassword}' -AsPlainText -Force)`
                               -NoRebootOnCompletion `
                               -Force
            #>
        }

        # Reboot the computer into the Directory Services Restore Mode.
        bcdedit.exe /set safeboot dsrepair 
        shutdown.exe /r /t 5
        Suspend-Workflow -Label 'Waiting for reboot'

        # Re-encrypt the DB with the new boot key.
        $currentBootKey = Get-BootKey -Online
        Set-ADDBBootKey -DatabasePath '{SourceDBPath}' -LogPath '{SourceLogDirPath}' -OldBootKey {OldBootKey} -NewBootKey $currentBootKey

        # Clone the DC account password.
        $ntdsParams = Get-ItemProperty -Path registry::HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NTDS\Parameters
        InlineScript {
            # Note: SupplementalCredentials do not get serialized properly without using the InlineScript activity.
            $dcAccount = Get-ADDBAccount -SamAccountName '{DCName}$' -DatabasePath $using:ntdsParams.'DSA Database file' -LogPath $using:ntdsParams.'Database log files path' -BootKey $using:currentBootKey
            Set-ADDBAccountPasswordHash -ObjectGuid {DCGuid} -NTHash $dcAccount.NTHash -SupplementalCredentials $dcAccount.SupplementalCredentials -DatabasePath '{SourceDBPath}' -LogPath '{SourceLogDirPath}' -BootKey $using:currentBootKey
        }

        # Replace the database and transaction logs.
        robocopy.exe '{SourceDBDirPath}' $ntdsParams.'DSA Working Directory' *.dit *.edb *.chk *.jfm /MIR /NP /NDL /NJS
        robocopy.exe '{SourceLogDirPath}' $ntdsParams.'Database log files path' *.log *.jrs /MIR /NP /NDL /NJS

        # Replace SYSVOL.
        robocopy.exe '{SourceSysvolPath}\{DomainName}' "{TargetSysvolPath}\domain" /MIR /XD DfsrPrivate /XJ /COPYALL /SECFIX /TIMFIX /NP /NDL

        # Reconfigure LSA policies. We would get into a BSOD loop if they do not match the corresponding values in the database.
        Set-LsaPolicyInformation -DomainName '{NetBIOSDomainName}' -DnsDomainName '{DomainName}' -DnsForestName '{ForestName}' -DomainGuid {DomainGuid} -DomainSid {DomainSid}

        # Tell the DC that its DB has intentionally been restored. A new InvocationID will be generated as soon as the service starts.
        Set-ItemProperty -Path registry::HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NTDS\Parameters -Name 'Database restored from backup' -Value 1 -Force
        Remove-ItemProperty -Path registry::HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NTDS\Parameters -Name 'DSA Database Epoch' -Force

        # Disable DSRM and do one last reboot.
        bcdedit.exe /deletevalue safeboot
        shutdown.exe /r /t 5
        Suspend-Workflow -Label 'Waiting for reboot'

        # Reconfigure SYSVOL replication in case it has been restored to a different path.

        # Update DFS-R subscription if present in AD.
        $dfsrSubscriptionDN = 'CN=SYSVOL Subscription,CN=Domain System Volume,CN=DFSR-LocalSettings,{DCDistinguishedName}'
        $dfsrSubscription = Set-ADObject -Identity $dfsrSubscriptionDN -Server localhost -PassThru -ErrorAction SilentlyContinue -Replace @{
            'msDFSR-RootPath' = "{TargetSysvolPath}\domain";
            'msDFSR-StagingPath' = "{TargetSysvolPath}\staging areas\{DomainName}"
        }

        if($dfsrSubscription -ne $null)
        {
            # Download the updated DFS-R configuration from AD.
            Invoke-WmiMethod -Class DfsrConfig -Name PollDsNow -ArgumentList localhost -Namespace ROOT\MicrosoftDfs
        }

        # Update FRS subscription if present in AD.
        $frsSubscriptionDN = 'CN=Domain System Volume (SYSVOL share),CN=NTFRS Subscriptions,{DCDistinguishedName}'
        $frsSubscription = Set-ADObject -Identity $frsSubscriptionDN -Server localhost -PassThru -ErrorAction SilentlyContinue -Replace @{
            'fRSRootPath' = "{TargetSysvolPath}\domain";
            'fRSStagingPath' = "{TargetSysvolPath}\staging\domain"
        }

        if($frsSubscription -ne $null)
        {
            # Download the updated FRS configuration from AD.
            InlineScript { ntfrsutl.exe poll /now }
        }
    }

    # Delete any pre-existing workflows with the same name before starting a new one.
    Remove-Job -Name DSInternals-RFM-Workflow -Force -ErrorAction SilentlyContinue

    # Start the workflow.
    Restore-DomainController -JobName DSInternals-RFM-Workflow
}

# The DSInternals-RFM-Resumer job will be executed after each reboot to unpause the workflow.
$resumeTask = Register-ScheduledJob -Name DSInternals-RFM-Resumer -Trigger (New-JobTrigger -AtStartup) -ScriptBlock {
    # Unregister the one-time task that must already have been executed.
    Unregister-ScheduledJob -Name DSInternals-RFM-Initializer -Force -ErrorAction SilentlyContinue

    # Resume the workflow after the computer is rebooted.
    Resume-Job -Name DSInternals-RFM-Workflow -Wait | Wait-Job | where State -In Completed,Failed,Stopped | foreach {
        # Perform cleanup when finished.
        Remove-Job -Job $PSItem -Force
        Unregister-ScheduledJob -Name DSInternals-RFM-Resumer -Force
    }
}

# Configure the scheduled tasks to run under the SYSTEM account.
# Note: In order to maintain compatibility with Windows Server 2008 R2, the ScheduledTasks module is not used.
schtasks.exe /Change /TN '\Microsoft\Windows\PowerShell\ScheduledJobs\DSInternals-RFM-Initializer' /RU SYSTEM | Out-Null
schtasks.exe /Change /TN '\Microsoft\Windows\PowerShell\ScheduledJobs\DSInternals-RFM-Resumer' /RU SYSTEM | Out-Null

# Start the workflow task and let the magic happen.
Write-Host 'The {DCName} domain controller will now be restored from media. Up to 3 reboots will follow.'
pause
$initTask.RunAsTask()
