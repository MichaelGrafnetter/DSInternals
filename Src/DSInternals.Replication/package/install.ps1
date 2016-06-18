param($installPath, $toolsPath, $package, $project)

# Remove unnecessary references from the project
$project.Object.References |
	where { ($_.Name -like 'NDceRpc.*' -and $_.Name -ne 'NDceRpc.Microsoft') -or $_.Name -eq 'protobuf-net' } |
	foreach { $_.Remove() }


