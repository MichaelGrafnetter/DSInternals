$ErrorActionPreference = 'Stop'

# Remove Module Files
$destinationDir = Join-Path -Path $PSHOME -ChildPath 'Modules\DSInternals'
Remove-Item -Path $destinationDir -Recurse -Force -ErrorAction SilentlyContinue

# Remove Start Menu Link
$shortcutPath = Join-Path -Path $env:ProgramData -ChildPath 'Microsoft\Windows\Start Menu\Programs\DSInternals PowerShell Module.lnk'
Remove-Item -Path $shortcutPath -Force -ErrorAction SilentlyContinue
