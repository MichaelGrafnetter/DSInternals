<#
.SYNOPSIS
    Checks for common issues that may prevent the 'DSInternals' module from working correctly.

.DESCRIPTION
    This script is automatically executed when the 'DSInternals' module is imported
    and checks for common issues that may prevent the module from working correctly.

    It performs the following checks:
    - Verifies that the current operating system is Windows.
    - Checks for the presence of the Universal C Runtime (UCRT).
    - Checks if the interop assembly is blocked by Windows security features.
    - Checks if only FIPS certified cryptographic algorithms are enabled in .NET, which can affect replication cmdlets.

    The script can also be executed manually to validate the module installation and provide guidance on resolving any detected issues.

.NOTES
    Copyright (c) Michael Grafnetter
#>

#Requires -Version 5.1

#
# Check if the current OS is Windows.
#

if ($env:OS -ne 'Windows_NT') {
    Write-Error -Message 'The DSInternals PowerShell module is only supported on Windows.' `
                -Category ([System.Management.Automation.ErrorCategory]::NotImplemented)
}

#
# Check the presence of the Universal C Runtime.
#

[string] $ucrtPath = Join-Path -Path  ([System.Environment]::SystemDirectory) -ChildPath 'ucrtbase.dll'
[bool] $ucrtPresent = Test-Path -Path $ucrtPath

if (-not $ucrtPresent) {
    # This can happen on systems prior to Windows 10 with missing updates.
    Write-Error -Message 'The Universal C Runtime is missing. Run Windows Update or install it manually and reload the DSInternals module afterwards.' `
                -Category ([System.Management.Automation.ErrorCategory]::NotInstalled)
}

#
# Check if the interop assembly is blocked (Windows PowerShell only).
#

if ($PSEdition -eq 'Desktop') {
    [string] $basePath = Join-Path -Path $PSScriptRoot -ChildPath 'net48'
    [string] $architectureSpecificPath = Join-Path -Path $basePath -ChildPath $env:PROCESSOR_ARCHITECTURE
    [string] $interopAssemblyName = 'DSInternals.Replication.Interop.dll'
    [string] $interopAssemblyPath = Join-Path -Path $architectureSpecificPath -ChildPath $interopAssemblyName
    [bool] $interopAssemblyExists = Test-Path -Path $interopAssemblyPath -PathType Leaf

    if (-not $interopAssemblyExists) {
        # Fallback to the parent directory
        $interopAssemblyPath = Join-Path -Path $basePath -ChildPath $interopAssemblyName
        $interopAssemblyExists = Test-Path -Path $interopAssemblyPath -PathType Leaf
    }

    if ($interopAssemblyExists) {
        [object] $zoneIdentifier = Get-Item -Path $interopAssemblyPath -Stream 'Zone.Identifier' -ErrorAction SilentlyContinue

        if ($null -ne $zoneIdentifier) {
            # This usually happens to users of the ZIP distribution who forget to unblock it before extracting the files.
            Write-Error -Message 'Unblock the interop assembly using either the Properties dialog or the Unblock-File cmdlet and reload the DSInternals module afterwards.' `
                        -Category ([System.Management.Automation.ErrorCategory]::SecurityError)
        }
    }
}

#
# Check if the MD5 hash function is available.
#

if ([System.Security.Cryptography.CryptoConfig]::AllowOnlyFipsAlgorithms) {
    [string] $message = 'Only FIPS certified cryptographic algorithms are enabled in .NET. DSInternals cmdlets that require the MD5 hash function will not work as expected.'
    [string] $configPath = [System.Diagnostics.Process]::GetCurrentProcess().Path + '.config'
    [string] $recommendedAction = 'Add the <enforceFIPSPolicy enabled="false"/> directive to the "{0}" file.' -f $configPath

    Write-Error -Message $message `
                -RecommendedAction $recommendedAction `
                -Category ([System.Management.Automation.ErrorCategory]::SecurityError)
}
