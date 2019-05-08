$ErrorActionPreference = 'Stop'

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
