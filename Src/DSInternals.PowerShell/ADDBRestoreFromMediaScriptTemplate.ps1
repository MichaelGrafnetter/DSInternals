<#
.SYNOPSIS
Restores the {DCName} domain controller from ntds.dit.

.REMARKS
This script should only be executed on a freshly installed {OSName}. Use at your own risk

.AUTHOR
Michael Grafnetter
#>
#Requires -Version 3 -Modules DSInternals -RunAsAdministrator

if($env:COMPUTERNAME -ne '{DCName}')
{
	# A server rename operation is required.
	Rename-Computer -NewName '{DCName}' -Force
	Restart-Computer -Force -Wait
	return
}

$ntdsService = Get-Service NTDS -ErrorAction SilentlyContinue
if($ntds -eq $null)
{
	# A DC promotion is required.
	dcpromo.exe /unattend /ReplicaOrNewDomain:Domain /NewDomain:Forest /NewDomainDNSName:"{DomainName}" /DomainNetBiosName:"{NetBIOSDomainName}" /DomainLevel:{DomainMode} /ForestLevel:{ForestMode} '/SafeModeAdminPassword:"{DSRMPassword}"' /DatabasePath:"{TargetDBDirPath}" /LogPath:"{TargetLogDirPath}" /SysVolPath:"{TargetSysvolPath}" /AllowDomainReinstall:Yes /CreateDNSDelegation:No /DNSOnNetwork:No /InstallDNS:Yes /RebootOnCompletion:No
	Restart-Computer -Force -Wait
	return
}

# Create a snapshot of the current state as a precaution.
ntdsutil.exe 'activate instance ntds' snapshot create quit quit

# Re-encrypt the DB with the new boot key.
$currentBootKey = Get-BootKey -Online
Set-ADDBBootKey -DBPath '{SourceDBPath}' -LogPath '{SourceLogDirPath}' -OldBootKey {OldBootKey} -NewBootKey $currentBootKey

# The AD service must be stopped before the database can be replaced.
Stop-service -Name NTDS -Force

# Clone the DC account password.
$ntdsParams = Get-ItemProperty -Path HKLM:\SYSTEM\CurrentControlSet\Services\NTDS\Parameters
$dcAccount = Get-ADDBAccount -SamAccountName '{DCName}$' -DBPath $ntdsParams.'DSA Database file' -LogPath $ntdsParams.'Database log files path' -BootKey $currentBootKey
Set-ADDBAccountPasswordHash -ObjectGuid {DCGuid} -NTHash $dcAccount.NTHash -SupplementalCredentials $dcAccount.SupplementalCredentials -DBPath '{SourceDBPath}' -LogPath '{SourceLogDirPath}' -BootKey $currentBootKey

# Replace the database and transaction logs
robocopy.exe '{SourceDBDirPath}' $ntdsParams.'DSA Working Directory' *.dit * *.edb .chk *.jfm /MIR
robocopy.exe '{SourceLogDirPath}' $ntdsParams.'Database log files path' *.log *.jrs /MIR

# Replace SYSVOL
$netlogonParams = Get-ItemProperty -Path HKLM:\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters -Name SysVol
robocopy.exe '{SourceSysvolPath}' (Join-Path -Path $netlogonParams.SysVol -ChildPath 'domain') /COPYALL /SECFIX /TIMFIX /DCOPY:DAT /MIR

# Reconfigure LSA policies
Set-LsaPolicyInformation -DomainName '{NetBIOSDomainName}' -DnsDomainName '{NetBIOSDomainName}' -DnsForestName '{ForestName}' -DomainGuid {DomainGuid} -DomainSid {DomainSid}

# Tell the DC that its DB has intentionally been restored.
Remove-ItemProperty -Path HKLM:\SYSTEM\CurrentControlSet\Services\NTDS\Parameters -Name 'DSA Database Epoch' -Force

# Start the AD service with the new DB.
Start-Service -Name NTDS

# A reboot is now strongly recommended.