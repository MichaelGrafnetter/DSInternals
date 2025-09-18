Set-StrictMode -Version 'Latest'
$ErrorActionPreference = 'Stop'

# Check PowerShell Version
# Note: The #requires statement is not used in order to provide a custom message.
if ($PSVersionTable.PSVersion -lt [Version]'5.1')
{
    throw 'The minimum required version of PowerShell is 5.1. Please upgrade by running the "choco install powershell" command first.'
}

# Copy Files
[string] $rootDir = Split-Path -Parent -Path $PSScriptRoot
[string] $sourceDir = Join-Path -Path $rootDir -ChildPath 'DSInternals' -Resolve
[string] $destinationDir = Join-Path -Path $PSHOME -ChildPath 'Modules\DSInternals'
robocopy.exe $sourceDir $destinationDir /MIR /MOVE /NJS /NJH /NDL /NFL /NS /NP

# Cleanup
Remove-Item -Path $sourceDir -Recurse -Force -ErrorAction SilentlyContinue

# Create Start Menu Link
[string] $psPath = (Get-Command -Name 'powershell.exe').Path
[string] $psArguments = '-NoExit -Command "& { Import-Module -Name DSInternals; Get-Help -Name about_DSInternals }"'
[string] $shortcutPath = Join-Path -Path $env:ProgramData -ChildPath 'Microsoft\Windows\Start Menu\Programs\DSInternals PowerShell Module.lnk'
Install-ChocolateyShortcut -ShortcutFilePath $shortcutPath -TargetPath $psPath -Arguments $psArguments -WorkingDirectory "$env:SystemDrive\"
