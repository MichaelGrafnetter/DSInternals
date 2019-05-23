Set-StrictMode -Version 'Latest'
$ErrorActionPreference = 'Stop'

# Check PowerShell Version
# Note: The #requires statement is not used in order to provide a custom message.
if ($PSVersionTable.PSVersion -lt [Version]'3.0')
{
    throw 'The minimum required version of PowerShell is 3. Please upgrade by running the "choco install powershell" command first.'
}

# Copy Files
$toolsDir = Split-Path -Parent ($MyInvocation.MyCommand.Definition)
$sourceDir = Join-Path -Path $toolsDir -ChildPath '..\DSInternals' -Resolve
$destinationDir = Join-Path -Path $PSHOME -ChildPath 'Modules\DSInternals'
robocopy $sourceDir $destinationDir /MIR /MOVE /NJS /NJH /NDL /NFL /NS /NP

# Cleanup
Remove-Item -Path $sourceDir -Recurse -Force -ErrorAction SilentlyContinue

# Create Start Menu Link
$psPath = (Get-Command -Name 'powershell.exe').Path
$psArguments = '-NoExit -Command "& { Import-Module -Name DSInternals; Get-Help -Name about_DSInternals }"'
$shortcutPath = Join-Path -Path $env:ProgramData -ChildPath 'Microsoft\Windows\Start Menu\Programs\DSInternals PowerShell Module.lnk'
Install-ChocolateyShortcut -ShortcutFilePath $shortcutPath -TargetPath $psPath -Arguments $psArguments -WorkingDirectory "$env:SystemDrive\"
