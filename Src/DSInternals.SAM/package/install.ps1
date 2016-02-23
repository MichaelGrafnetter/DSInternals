param($installPath, $toolsPath, $package, $project)

# Remove unnecessary references from the project
$project.Object.References |
	where { $_.Name -eq 'DSInternals.Common' } |
	foreach { $_.Remove() }


