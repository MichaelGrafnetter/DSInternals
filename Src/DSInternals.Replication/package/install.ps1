param($installPath, $toolsPath, $package, $project)

# Remove unnecessary references from the project
$project.Object.References |
	where { $_.Name -like 'NDceRpc.*' -or $_.Name -in 'protobuf-net','DSInternals.Common' } |
	foreach { $_.Remove() }


