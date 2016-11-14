#
# Script module file for the 'DSInternals' module.
#
# Copyright (c) Michael Grafnetter
#

#
# Load the platform-specific libraries.
# Note: This operation cannot be done in the module manifest,
#       as it only supports restricted language mode.
#

$interopAssemblyPath = Join-Path $PSScriptRoot "$env:PROCESSOR_ARCHITECTURE\DSInternals.Replication.Interop.dll"
try
{
    Add-Type -Path $interopAssemblyPath
}
catch [System.IO.FileLoadException]
{
    # This usually happens to users of the ZIP distribution who forget to unblock it before extracting the files.
    $message = 'Could not load assembly "{0}". Try unblocking it using either the Properties dialog or the Unblock-File cmdlet and reload the DSInternals module afterwards. ' -f $interopAssemblyPath
    Write-Error -Message $message `
                -Exception $PSItem.Exception `
                -Category SecurityError `
                -CategoryTargetName $interopAssemblyPath `
                -CategoryActivity $PSItem.CategoryInfo.Activity `
                -CategoryReason $PSItem.CategoryInfo.Reason
}

#
# Cmdlet aliases
#

New-Alias -Name Set-ADAccountPasswordHash        -Value Set-SamAccountPasswordHash
New-Alias -Name Set-WinUserPasswordHash          -Value Set-SamAccountPasswordHash
New-Alias -Name Get-ADPasswordPolicy             -Value Get-SamPasswordPolicy
New-Alias -Name Get-ADDefaultPasswordPolicy      -Value Get-SamPasswordPolicy
New-Alias -Name ConvertFrom-UnattendXmlPassword  -Value ConvertFrom-UnicodePassword
New-Alias -Name ConvertTo-AADHash                -Value ConvertTo-OrgIdHash
New-Alias -Name ConvertTo-MsoPasswordHash        -Value ConvertTo-OrgIdHash
New-Alias -Name Get-ADReplicationAccount         -Value Get-ADReplAccount
New-Alias -Name ConvertFrom-ManagedPasswordBlob  -Value ConvertFrom-ADManagedPasswordBlob
New-Alias -Name Get-SysKey                       -Value Get-BootKey
New-Alias -Name Set-ADDBSysKey                   -Value Set-ADDBBootKey
New-Alias -Name New-NTHashDictionary             -Value ConvertTo-NTHashDictionary
New-Alias -Name Test-ADPasswordQuality           -Value Test-PasswordQuality
New-Alias -Name Test-ADDBPasswordQuality         -Value Test-PasswordQuality
New-Alias -Name Test-ADReplPasswordQuality       -Value Test-PasswordQuality

# Export the aliases
Export-ModuleMember -Alias * -Cmdlet *