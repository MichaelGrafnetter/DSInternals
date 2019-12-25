<#
.SYNOPSIS
The purpose of this script is to check the Release build for any obvious flaws without running all unit tests.
#>

$root = Join-Path $PSScriptRoot ..\
$dsInternalsModulePath = Join-Path $root Build\bin\Release\DSInternals
# If multiple versions of Pester are present, only load the latest one
$pesterModulePath = Join-Path $root Src\packages\Pester*\tools\Pester.psd1 -Resolve |
                        Sort-Object -Descending |
                        Select-Object -First 1
$testsPath = Join-Path $root Src\DSInternals.PowerShell\Tests\
$resultsPath = Join-Path $root 'TestResults'
$docPath = Join-Path $root 'Documentation'
$nuspecPath = Join-Path $root 'Src\DSInternals.PowerShell\Chocolatey\*.nuspec' -Resolve

# Create output dir if it does not exist
New-Item $resultsPath -ItemType Directory -Force | Out-Null

Import-Module $pesterModulePath -ErrorAction Stop
Invoke-Pester -OutputFile $resultsPath\SmokeTests.xml `
              -OutputFormat NUnitXml `
              -Script @{
                    Path =  "$testsPath\*.Smoke.Tests.ps1";
                    Parameters = @{
                        ModulePath = $dsInternalsModulePath;
                        MarkdownDocumentationPath = $docPath;
                        ChocolateySpecPath = $nuspecPath
                    }
              }
