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

[string] $interopAssemblyPath = Join-Path $PSScriptRoot "$env:PROCESSOR_ARCHITECTURE\DSInternals.Replication.Interop.dll"
try
{
    Add-Type -Path $interopAssemblyPath
}
catch [System.IO.IOException]
{
    #
    # Make the error message more meaningful by checking common failure reasons.
    #

    [string] $message = 'The Get-ADRepl* cmdlets will not work properly, because the "{0}" assembly could not be loaded.' -f $interopAssemblyPath
    [System.Management.Automation.ErrorCategory] $errorCategory = [System.Management.Automation.ErrorCategory]::OpenError

    # Check the presence of the Universal C Runtime
    [string] $ucrtPath = Join-Path ([System.Environment]::SystemDirectory) 'ucrtbase.dll'
    [bool] $ucrtPresent = Test-Path $ucrtPath

    if(-not $ucrtPresent)
    {
        # This can happen on systems prior to Windows 10 with missing updates.
        $message += ' The Universal C Runtime is missing. Run Windows Update or install it manually and reload the DSInternals module afterwards.'
        $errorCategory = [System.Management.Automation.ErrorCategory]::NotInstalled
    }

    # Check if the interop assembly is blocked
    [object] $zoneIdentifier = Get-Item $interopAssemblyPath -Stream 'Zone.Identifier' -ErrorAction SilentlyContinue

    if($zoneIdentifier -ne $null)
    {
        # This usually happens to users of the ZIP distribution who forget to unblock it before extracting the files.
        $message += ' Unblock the assembly using either the Properties dialog or the Unblock-File cmdlet and reload the DSInternals module afterwards.'
        $errorCategory = [System.Management.Automation.ErrorCategory]::SecurityError
    }

    # Build the error report
    Write-Error -Message $message `
                -Exception $PSItem.Exception `
                -Category $errorCategory `
                -CategoryTargetName $interopAssemblyPath `
                -CategoryActivity $PSItem.CategoryInfo.Activity `
                -CategoryReason $PSItem.CategoryInfo.Reason
}

#
# Type Data
# Note: *.types.ps1xml cannot be used for the following configuration, because it is processed before *.psm1 and would thus fail loading platform-specific assemblies. 

Update-TypeData -TypeName 'DSInternals.Common.Data.SupplementalCredentials' `
                -TypeConverter ([DSInternals.PowerShell.SupplementalCredentialsDeserializer]) `
                -Force

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
New-Alias -Name Get-SystemKey                    -Value Get-BootKey
New-Alias -Name Set-ADDBSysKey                   -Value Set-ADDBBootKey
New-Alias -Name Test-ADPasswordQuality           -Value Test-PasswordQuality
New-Alias -Name Test-ADDBPasswordQuality         -Value Test-PasswordQuality
New-Alias -Name Test-ADReplPasswordQuality       -Value Test-PasswordQuality
New-Alias -Name Get-KeyCredential                -Value Get-ADKeyCredential
New-Alias -Name Get-KeyCredentialLink            -Value Get-ADKeyCredential
New-Alias -Name Get-ADKeyCredentialLink          -Value Get-ADKeyCredential
New-Alias -Name New-ADKeyCredential              -Value Get-ADKeyCredential
New-Alias -Name New-ADKeyCredentialLink          -Value Get-ADKeyCredential
New-Alias -Name New-ADNgcKey                     -Value Get-ADKeyCredential
New-Alias -Name Get-LsaPolicy                    -Value Get-LsaPolicyInformation
New-Alias -Name Set-LsaPolicy                    -Value Set-LsaPolicyInformation
New-Alias -Name Write-ADReplNgcKey               -Value Add-ADReplNgcKey
New-Alias -Name Write-ADNgcKey                   -Value Add-ADReplNgcKey
New-Alias -Name Add-ADNgcKey                     -Value Add-ADReplNgcKey

# Export the aliases
Export-ModuleMember -Alias * -Cmdlet *
