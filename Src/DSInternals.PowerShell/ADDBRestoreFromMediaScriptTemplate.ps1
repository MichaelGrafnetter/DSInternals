<#
.SYNOPSIS
Restores the {DCHostName} domain controller from its ntds.dit file.

.DESCRIPTION

This script performs a multi-phase domain controller restore from an IFM backup:
- Phase 0: Initiate the restore process and create a VSS backup.
- Phase 1: Set the local Administrator password if empty.
- Phase 2: Rename the computer and reboot if necessary.
- Phase 3: Install the required Windows features and reboot if needed.
- Phase 4: Promote the server to a domain controller.
- Phase 5: Restore the AD database, re-encrypt it, and reconfigure LSA policies.
- Phase 6: Replace the SYSVOL directory, restore ACLs if available, and reboot the server.
- Phase 7: Reconfigure the SYSVOL replication subscription.

Script exection logs can be found in the C:\Windows\Logs\DSInternals-RestoreFromMedia.txt file.

.PARAMETER Phase
Specifies the phase of the restore operation to execute. Used to orchestrate the integrated recovery workflow.

.NOTES
This script should only be executed on a freshly installed {OSName}. Use at your own risk.
The DSInternals PowerShell module must be installed for all users on the target server.
It is recommended to change the DSRM password after DC promotion.

Author:  Michael Grafnetter
Version: 2.2

#>

#Requires -Version 3 -Modules DSInternals -RunAsAdministrator

param(
    [Parameter(Mandatory = $false)]
    [ValidateRange(0, 7)]
    [int] $Phase = 0
)

# Make sure that the required data types and cmdlets are available.
Import-Module -Name DSInternals -ErrorAction Stop

function Main {
    [string] $script:LogFile = "$env:windir\Logs\DSInternals-RestoreFromMedia.txt"
    [System.Console]::OutputEncoding = [System.Text.Encoding]::UTF8
    Write-Log -Message "Starting script execution in phase $Phase..."

    # The script must be executed locally so that it is accessible even after a reboot.
    Test-ScriptPath

    switch($script:Phase)
    {
        0 {
            Write-Log 'The {DCName} domain controller will now be restored from media. Up to 3 reboots will follow shortly.'

            # Perform a VSS backup before doing anything else.
            New-VolumeShadowCopy -Volume $env:SystemDrive

            # Invoke the first phase in the background.
            Register-ScheduledScript -ExecutePhase 1
        }
        1 {
            # The local Administrator account must have a password set for dcpromo to succeed.
            Reset-LocalAdministratorPassword -NewPassword '{DSRMPassword}'

            # Continue to the next phase.
            Register-ScheduledScript -ExecutePhase 2
        }
        2 {
            Write-Log -Message 'Checking the computer name...'
            [bool] $computerRenameRequired = $env:COMPUTERNAME -ne '{DCName}'

            if ($computerRenameRequired) {
                # A server rename operation is required.
                # Note: The host name will automatically be truncated to 15 characters.
                Write-Log -Message 'Renaming the computer to {DCHostName}...'
                Rename-Computer -NewName '{DCHostName}' -Force -Verbose *>> $script:LogFile
            } else {
                Write-Log -Message 'The local system already has the correct name. Skipping the rename operation.'
            }

            # Perform an optional reboot and continue to the next phase.
            Register-ScheduledScript -ExecutePhase 3 -RebootRequired:$computerRenameRequired
        }
        3 {
            Write-Log -Message 'Installing the required Windows features...'

            # Note: The ServerManager module is not available during Safe Boot. It is therefore not imported globally.
            Import-Module -Name ServerManager -ErrorAction Stop

            # Notes:
            # The dcpromo.exe tool would install most of these features if absent.
            # The BitLocker Recovery Password Viewer is called RSAT-Bitlocker-RecPwd on Windows Server 2008 R2 and cannot be instaleld on non-domain computers.
            # The AD-Domain-Services component would try to install UNIX-related components on Windows Server 2008 R2, which cannot be installed on non-domain computers.
            [string[]] $featuresToInstall = @(
                'DNS',
                'GPMC',
                'RSAT-AD-AdminCenter',
                'RSAT-ADDS-Tools',
                'RSAT-AD-PowerShell',
                'RSAT-DNS-Server',
                'RSAT-DFS-Mgmt-Con', # dfsrdiag.exe is not installed by default
                'RSAT-Feature-Tools-BitLocker-BdeAducExt' # BitLocker Recovery Password Viewer is not installed by default
            )

            # Notes:
            # The Add-WindowsFeature alias is used instead of Install-WindowsFeature for compatibility reasons.
            # The -IncludeManagementTools parameter is not used because it is not available on Windows Server 2008 R2.
            [object[]] $featuresRequiringRestart = Get-WindowsFeature |
                Where-Object Name -in $featuresToInstall |
                Add-WindowsFeature -IncludeAllSubFeature |
                Where-Object RestartNeeded -ne ([Microsoft.Windows.ServerManager.Commands.RestartState]::No) 2>> $script:LogFile

            [bool] $restartNeeded = $null -ne $featuresRequiringRestart

            # Perform an optional reboot and continue to the next phase.
            Register-ScheduledScript -ExecutePhase 4 -RebootRequired:$restartNeeded
        }
        4 {
            # Check if the NTDS service is present and enabled, possibly by a previous script execution.
            Write-Log -Message 'Checking the state of the NTDS service...'
            [System.ServiceProcess.ServiceController] $ntdsService = Get-Service -Name NTDS -ErrorAction SilentlyContinue

            if ($null -eq $ntdsService -or $ntdsService.StartType -eq [System.ServiceProcess.ServiceStartMode]::Disabled) {
                # A DC promotion is required.
                Write-Log -Message 'Promoting the server to a domain controller...'

                # Note: In order to maintain compatibility with Windows Server 2008 R2, the ADDSDeployment PS module is not used.
                dcpromo.exe /unattend /ReplicaOrNewDomain:Domain /NewDomain:Forest /NewDomainDNSName:"{DomainName}" /DomainNetBiosName:"{NetBIOSDomainName}" /DomainLevel:{DomainMode} /ForestLevel:{ForestMode} '/SafeModeAdminPassword:"{DSRMPassword}"' /DatabasePath:"{TargetDBDirPath}" /LogPath:"{TargetLogDirPath}" /SysVolPath:"{TargetSysvolPath}" /AllowDomainReinstall:Yes /CreateDNSDelegation:No /DNSOnNetwork:No /InstallDNS:Yes /RebootOnCompletion:No *>> $script:LogFile
            } else {
                Write-Log -Message 'The server is already a domain controller. Skipping dcpromo execution.'
            }

            # Prevent the Server Manager from saying that additional configuration is required.
            # Note: The Roles key does not exist on Windows Server 2008 R2.
            Write-Log -Message 'Marking the AD DS role as already configured by the Server Manager...'
            Set-ItemProperty -Path 'registry::HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\ServerManager\Roles\10' `
                             -Name 'ConfigurationStatus' `
                             -Value 2 `
                             -Type DWord `
                             -Force `
                             -ErrorAction SilentlyContinue `
                             -Verbose *>> $script:LogFile

            # Avoid FSMO role holder being unavailable until it has completed replication of a writeable directory partition.
            Write-Log -Message 'Disabling the initial datatabse synchronization...'
            Set-ItemProperty -Path 'registry::HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\NTDS\Parameters' `
                             -Name 'Repl Perform Initial Synchronizations' `
                             -Value 0  `
                             -Type DWord `
                             -Force `
                             -Verbose *>> $script:LogFile

            # Continue with post-installation tasks.
            Register-ScheduledScript -ExecutePhase 5
        }
        5 {
            # Make sure that AD DS is not running.
            Write-Log -Message 'Checking the state of the NTDS service...'
            [System.ServiceProcess.ServiceController] $ntdsService = Get-Service -Name NTDS -ErrorAction SilentlyContinue

            if($null -eq $ntdsService) {
                Write-Log -Message 'Could not find the NTDS service. Terminating...'
                break
            } elseif ($ntdsService.Status -eq [System.ServiceProcess.ServiceControllerStatus]::Running) {
                Write-Log -Message 'Stopping the NTDS service...'
                Stop-Service -Name NTDS -Force -Verbose *>> $script:LogFile
            } else {
                # Note: This is the most common case, as AD DS should not be running before a reboot.
                Write-Log -Message 'The NTDS service is stopped. Proceeding with database restoration...'
            }

            # Replace the database files using robocopy.
            # Copy the database (*.dit, *.edb), checkpoint (*.chk), and flush map (*.jfm) files.
            # /MIR: Mirrors the directory tree
            # /NP: No progress
            # /NDL: No directory list
            # /NJS: No job summary
            Write-Log -Message 'Replacing the AD database files...'
            robocopy.exe '{SourceDBDirPath}' '{TargetDBDirPath}' *.dit *.edb *.chk *.jfm /MIR /NP /NDL /NJS *>> $script:LogFile

            # Replace the transaction logs using robocopy.
            # Copy the transaction logs (*.log) and reserved transaction log files (*.jrs).
            Write-Log -Message 'Replacing the AD database transaction log files...'
            robocopy.exe '{SourceLogDirPath}' '{TargetLogDirPath}' *.log *.jrs /MIR /NP /NDL /NJS *>> $script:LogFile

            # Re-encrypt the DB with the new boot key. We would get into a BSOD loop if the DC is unable to decrypt the database.
            Write-Log -Message 'Re-encrypting the database with the new boot key...'
            Set-ADDBBootKey -DatabasePath '{TargetDBPath}' `
                            -LogPath '{TargetLogDirPath}' `
                            -OldBootKey '{OldBootKey}' `
                            -NewBootKey '{CurrentBootKey}' `
                            -Force `
                            -Verbose *>> $script:LogFile

            # Reconfigure LSA policies. We would get into a BSOD loop if they did not match the corresponding values in the database.
            Write-Log -Message 'Reconfiguring the LSA policies...'
            Set-LsaPolicyInformation -DomainName '{NetBIOSDomainName}' `
                                     -DnsDomainName '{DomainName}' `
                                     -DnsForestName '{ForestName}' `
                                     -DomainGuid '{DomainGuid}' `
                                     -DomainSid '{DomainSid}' `
                                     -Verbose *>> $script:LogFile

            # Set the proper Configuration NC. This step is required for non-root domains.
            Write-Log -Message 'Changing the configuration naming context...'
            Set-ItemProperty -Path 'registry::HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NTDS\Parameters' `
                             -Name 'Configuration NC' `
                             -Value '{ConfigNC}' `
                             -Type String `
                             -Force `
                             -Verbose *>> $script:LogFile

            # Set the proper root domain distinguished name. This step is required for non-root domains.
            Write-Log -Message 'Changing the root domain...'
            Set-ItemProperty -Path 'registry::HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NTDS\Parameters' `
                             -Name 'Root Domain' `
                             -Value '{RootDomainNC}' `
                             -Type String `
                             -Force `
                             -Verbose *>> $script:LogFile

            # Set the distinguished name of NTDS Settings object. This step is required for non-default sites and non-root domains.
            Write-Log -Message 'Changing the machine distinguished name...'
            Set-ItemProperty -Path 'registry::HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NTDS\Parameters' `
                             -Name 'Machine DN Name' `
                             -Value '{NTDSSettingsObject}' `
                             -Type String `
                             -Force `
                             -Verbose *>> $script:LogFile

            # Tell the DC that its DB has intentionally been restored. A new InvocationID will be generated as soon as the service starts.
            Write-Log -Message 'Marking the database as restored from backup...'
            Set-ItemProperty -Path 'registry::HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NTDS\Parameters' `
                             -Name 'Database restored from backup' `
                             -Value 1 `
                             -Type DWord `
                             -Force `
                             -Verbose *>> $script:LogFile

            # Remove the DSA Database Epoch value to bypass the database rollback detection.
            Write-Log -Message 'Clearing the DSA Database Epoch value...'
            Remove-ItemProperty -Path 'registry::HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NTDS\Parameters' `
                                -Name 'DSA Database Epoch' `
                                -Force `
                                -Verbose *>> $script:LogFile

            # Note:
            # The DC account is created/modified during the first boot after promotion.
            # The machine account password thus does not need to be copied by this script.

            # Continue to the next phase.
            Register-ScheduledScript -ExecutePhase 6
        }
        6 {
            # Replace the SYSVOL directory.
            # /MIR: Mirrors the directory tree
            # /XD: Excludes the DfsrPrivate directory from being copied.
            # /XJ: Excludes junction points, several of which are present in SYSVOL.
            # /COPYALL: Copies all file information, including data, attributes, timestamps, NTFS ACLs (permissions), owner information, and auditing information.
            # /SECFIX: Fixes file security on all files, even skipped ones.
            # /TIMFIX: Fixes file times on all files, even skipped ones.
            # /NP: No progress
            # /NDL: No directory list
            Write-Log -Message 'Replacing the SYSVOL files...'
            [string] $sourcePath = Join-Path -Path '{SourceSysvolPath}' -ChildPath '{DomainName}'
            [string] $targetPath = Join-Path -Path '{TargetSysvolPath}' -ChildPath 'domain'
            robocopy.exe $sourcePath $targetPath /MIR /XD DfsrPrivate /XJ /COPYALL /SECFIX /TIMFIX /NP /NDL *>> $script:LogFile

            # Check if an optional SYSVOL Group Policy ACL backup is present.
            # This would be useful in situations where the SYSVOL is backed up to a non-NTFS file system.
            [string] $sysvolAclBackupPath = Join-Path -Path '{SourceSysvolPath}' -ChildPath 'PolicyPermissions.txt'
            if(Test-Path -Path $sysvolAclBackupPath -PathType Leaf) {
                Write-Log -Message 'Restoring the SYSVOL Group Policy ACLs...'
                [string] $aclRestorePath = Join-Path -Path '{TargetSysvolPath}' -ChildPath 'domain\Policies'
                icacls.exe $aclRestorePath /restore $sysvolAclBackupPath *>> $script:LogFile
            } else {
                Write-Log -Message 'No SYSVOL ACL backup found. Skipping the ACL restoration.'
            }

            # A reboot is required for AD to start.
            Register-ScheduledScript -ExecutePhase 7 -RebootRequired
        }
        7 {
            # Reconfigure SYSVOL replication in case it has been restored to a different path.

            # Make sure that AD Web Services are available.
            [System.ServiceProcess.ServiceController] $adws = Get-Service -Name NTDS -ErrorAction SilentlyContinue

            if($null -eq $adws -or $adws.Status -ne [System.ServiceProcess.ServiceControllerStatus]::Running) {
                Write-Log -Message 'AD Web Services are not available. Terminating...'
                break
            } else {
                Write-Log -Message 'The AD Web Services service is running. Proceeding with SYSVOL subscription reconfiguration...'
            }

            # Update DFS-R subscription if present in AD.
            Update-DfsrSubscription -DomainControllerDN '{DCDistinguishedName}' `
                                    -SysvolPath '{TargetSysvolPath}' `
                                    -DomainName '{DomainName}'

            # Update FRS subscription if present in AD.
            Update-FrsSubscription -DomainControllerDN '{DCDistinguishedName}' `
                                   -SysvolPath '{TargetSysvolPath}'
        }
    }

    if($Phase -ge 1) {
        [string] $taskName = "DSInternals-RFM-Phase$Phase"
        Write-Log -Message "Removing the scheduled task $taskName..."
        schtasks.exe /Delete /TN $taskName /F *>> $script:LogFile
    }

    Write-Log -Message "Execution of phase $Phase has finished."
}

#region Helper Functions

<#
.SYNOPSIS
Resets the password of the local Administrator account (RID=500)
if it has not been set yet.

.NOTES
This recovery script intentionally contains plaintext passwords.
Using a SecureString in this function would not provide any additional security.
The corresponding PSScriptAnalyzer warning is therefore suppressed.

#>
function Reset-LocalAdministratorPassword {
    [Diagnostics.CodeAnalysis.SuppressMessageAttribute("PSAvoidUsingPlainTextForPassword", "")]
    param(
        [Parameter(Mandatory = $true)]
        [string] $NewPassword
    )

    # Note: Due to compatibility reasons, the Get-LocalUser cmdlet is not used.
    Write-Log -Message 'Fetching built-in Administrator account information through WMI...'
    [wmi] $builtinAdminWMI = Get-WmiObject -Class Win32_UserAccount -Filter 'SID LIKE "%-500"' -Property Name

    Write-Log -Message 'Fetching built-in Administrator account information through ADSI...'
    [adsi] $builtinAdminADSI = 'WinNT://./{0},User' -f $builtinAdminWMI.Name
    [bool] $hasPassword = $builtinAdminADSI.PasswordAge.Value -gt 0

    if(-not $hasPassword) {
        Write-Log 'Setting a password for the local Administrator account...'
        $builtinAdminWMI.SetPassword($NewPassword) *>> $script:LogFile
    } else {
        Write-Log 'A password for the local Administrator account has already been set. No action is required.'
    }
}

<#
.SYNOPSIS
Creates a new volume shadow copy of the specified volume.
#>
function New-VolumeShadowCopy {
    param(
        [Parameter(Mandatory = $true)]
        [ValidatePattern('^[A-Z]:$')]
        [string] $Volume
    )

    Write-Log 'Creating a snapshot of the system drive to make rollback possible...'
    ([wmiclass] 'Win32_ShadowCopy').Create("$Volume\", 'ClientAccessible') *>> $script:LogFile
}

function Register-ScheduledScript {
    param(
        [Parameter(Mandatory = $true)]
        [int] $ExecutePhase,

        [Parameter(Mandatory = $false)]
        [switch] $RebootRequired
    )

    # Each phase of the script is executed by a separate scheduled task.
    [string] $taskName = "DSInternals-RFM-Phase$ExecutePhase"

    # Locate powershell.exe
    [string] $psPath = Get-Command -Name 'powershell.exe' -CommandType Application | Select-Object -ExpandProperty Path

    # Locate the current PS script
    [string] $scriptPath = $script:MyInvocation.MyCommand.Source

    # Generate the complete command line
    [string] $commandLine = '"{0}" -ExecutionPolicy Bypass -NonInteractive -NoProfile -NoLogo -File "{1}" -Phase {2}' -f $psPath,$scriptPath,$ExecutePhase

    # Note: The ScheduledTasks module is not used because it is not available on Windows Server 2008 R2.
    Write-Log -Message 'Registering the script to be executed as a scheduled task...'
    schtasks.exe /Create /TN $taskName /TR $commandLine /SC ONSTART /RU SYSTEM /RL HIGHEST /F *>> $script:LogFile

    if($RebootRequired) {
        # Reboot the computer and let it automatically execute the scheduled task.
        Write-Log -Message 'Rebooting the computer to continue the restore process...'
        shutdown.exe /r /t 5 /f *>> $script:LogFile
    } else {
        # Start the scheduled task immediately.
        Write-Log -Message 'Starting the scheduled task...'
        schtasks.exe /Run /I /TN $taskName *>> $script:LogFile
    }
}

<#
.SYNOPSIS
Checks whether the script is being executed from a local file.
#>
function Test-ScriptPath {
    Write-Log -Message 'Checking if the script is being executed from a local file...'

    # Check if the PSScriptRoot variable is set.
    [bool] $isScript = -not [string]::IsNullOrEmpty($PSScriptRoot)

    if(-not $isScript) {
        Write-Log -Message 'Not running as script. Terminating...'
        throw 'The script must be executed from a local file.'
    } else {
        if(-not ([uri] $PSScriptRoot).IsUnc) {
            Write-Log -Message 'The script is being executed from a local file.'
        } else {
            Write-Log -Message 'Running from a UNC path. Terminating...'
            throw 'The script must be executed from a local file.'
        }
    }
}

<#
.SYNOPSIS
Updates the DFS-R subscription if present in AD.

.PARAMETER DomainControllerDN
The distinguished name of the domain controller computer account.

.PARAMETER SysvolPath
The path to the SYSVOL share on the target domain controller.
#>
function Update-DfsrSubscription {
    param(
        [Parameter(Mandatory = $true)]
        [string] $DomainControllerDN,

        [Parameter(Mandatory = $true)]
        [string] $SysvolPath,

        [Parameter(Mandatory = $true)]
        [string] $DomainName
    )

    # Make sure that the required data types and cmdlets are available.
    Import-Module -Name ActiveDirectory -ErrorAction Stop

    Write-Log -Message 'Updating the FRS subscription object in AD...'
    [string] $dfsrSubscriptionDN = "CN=SYSVOL Subscription,CN=Domain System Volume,CN=DFSR-LocalSettings,$DomainControllerDN"
    [Microsoft.ActiveDirectory.Management.ADObject] $dfsrSubscription = Set-ADObject -Identity $dfsrSubscriptionDN -Server localhost -PassThru -ErrorAction SilentlyContinue -Replace @{
        'msDFSR-RootPath' = Join-Path -Path $SysvolPath -ChildPath 'domain'
        'msDFSR-StagingPath' = Join-Path -Path $SysvolPath -ChildPath "staging areas\$DomainName"
    }

    if($null -ne $dfsrSubscription) {
        # Download the updated DFS-R configuration from AD.
        Write-Log -Message 'Polling AD for DFS-R configuration changes...'
        Invoke-WmiMethod -Class DfsrConfig -Name PollDsNow -ArgumentList localhost -Namespace ROOT\MicrosoftDfs *>> $script:LogFile
    } else {
        Write-Log -Message 'DFS-R subscription was not found in AD. Has the domain not yet been migrated from FRS?'
    }
}

<#
.SYNOPSIS
Updates the FRS subscription if present in AD.

.PARAMETER DomainControllerDN
The distinguished name of the domain controller computer account.

.PARAMETER SysvolPath
The path to the SYSVOL share on the target domain controller.
#>
function Update-FrsSubscription {
    param(
        [Parameter(Mandatory = $true)]
        [string] $DomainControllerDN,

        [Parameter(Mandatory = $true)]
        [string] $SysvolPath
    )

    # Make sure that the required data types and cmdlets are available.
    Import-Module -Name ActiveDirectory -ErrorAction Stop

    # Update FRS subscription if present in AD.
    Write-Log -Message 'Updating the FRS subscription object in AD...'
    [string] $frsSubscriptionDN = "CN=Domain System Volume (SYSVOL share),CN=NTFRS Subscriptions,$DomainControllerDN"
    [Microsoft.ActiveDirectory.Management.ADObject] $frsSubscription = Set-ADObject -Identity $frsSubscriptionDN -Server localhost -PassThru  -Verbose -ErrorAction SilentlyContinue -Replace @{
        'fRSRootPath' = Join-Path -Path $SysvolPath -ChildPath 'domain'
        'fRSStagingPath' = Join-Path -Path $SysvolPath -ChildPath 'staging\domain'
    }

    if($null -ne $frsSubscription) {
        # Download the updated FRS configuration from AD.
        Write-Log -Message 'Polling AD for FRS configuration changes...'
        ntfrsutl.exe poll /now *>> $script:LogFile
    } else {
        Write-Log -Message 'FRS subscription was not found in AD. This is expected.'
    }
}

<#
.SYNOPSIS
Writes a message to both the console and a log file.

.PARAMETER Message
The message to be written.
#>
function Write-Log {
    param(
        [Parameter(Mandatory = $true)]
        [string] $Message
    )

    [string] $logMessage = '{0:yyyy-MM-dd HH:mm:ss} {1}' -f (Get-Date), $Message
    Write-Host $logMessage
    Add-Content -Path $script:LogFile -Value $logMessage -Encoding UTF8
}

#endregion Helper Functions

# Execute the main function
Main
