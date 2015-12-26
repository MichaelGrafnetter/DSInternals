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
Add-Type -Path $interopAssemblyPath

#
# Cmdlet aliases
#

New-Alias -Name Set-ADAccountPasswordHash        -Value Set-SamAccountPasswordHash
New-Alias -Name Set-WinUserPasswordHash          -Value Set-SamAccountPasswordHash
New-Alias -Name ConvertFrom-UnattendXmlPassword  -Value ConvertFrom-UnicodePassword
New-Alias -Name ConvertTo-AADHash                -Value ConvertTo-OrgIdHash
New-Alias -Name ConvertTo-MsoPasswordHash        -Value ConvertTo-OrgIdHash
New-Alias -Name Get-ADReplicationAccount         -Value Get-ADReplAccount
New-Alias -Name ConvertFrom-ManagedPasswordBlob  -Value ConvertFrom-ADManagedPasswordBlob
New-Alias -Name Get-SysKey                       -Value Get-BootKey
New-Alias -Name Set-ADDBSysKey                   -Value Set-ADDBBootKey

# Export the aliases
Export-ModuleMember -Alias * -Cmdlet *