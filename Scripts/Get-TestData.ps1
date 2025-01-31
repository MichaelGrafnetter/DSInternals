<#
.SYNOPSIS
Downloads the sample data from Azure storage.

.NOTES
Uses AzCopy, which can be installed by running winget.exe install AzCopy.

#>

#Requires -Version 3

# Test that AzCopy is available
Get-Command -Name azcopy.exe -CommandType Application -ErrorAction Stop | Out-Null

# Download the test data
[string] $parentDirectory = Split-Path -Path $PSScriptRoot -Parent -ErrorAction Stop
[string] $destinationDirectory = Join-Path -Path $parentDirectory -ChildPath TestData -ErrorAction Stop
azcopy.exe copy https://dsinternals.blob.core.windows.net/databases/* $destinationDirectory --recursive --skip-version-check --output-level essential
