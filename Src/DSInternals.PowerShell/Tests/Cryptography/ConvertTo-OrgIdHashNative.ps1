<#
.SYNOPSIS
    Calculates the OrgId hash of a given password using Entra ID Connect's library.

.DESCRIPTION
    This is only used to manually generate test data for C# Unit Tests.
#>
#Requires -Version 5.1
#Requires -Module DSInternals

param(
    [Parameter(Mandatory = $true)]
    [SecureString] $Password
)

## Load the Microsoft.Online.PasswordSynchronization.Cryptography.dll assembly from Entra ID Connect
# Location is typically 'C:\Program Files\Microsoft Azure AD Sync\', but it can be changed during installation.
$adSyncPath = (Get-ItemProperty -Path 'HKLM:\SOFTWARE\Microsoft\AD Sync').Location 
$cryptoAssemblyPath = Join-Path $adSyncPath 'Bin\Microsoft.Online.PasswordSynchronization.Cryptography.dll'
Add-Type -Path $cryptoAssemblyPath -ErrorAction Stop

$ntHash = ConvertTo-NTHash -Password $Password
return [Microsoft.Online.PasswordSynchronization.OrgIdHashGenerator]::Generate($ntHash.ToUpper())