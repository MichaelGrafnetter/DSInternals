param($installPath, $toolsPath, $package, $project)

# Remove unnecessary references from the project
$project.Object.References |
	where { $_.Name -in 'DSInternals.Common','Microsoft.Isam.Esent.Interop','Microsoft.Database.Isam' } |
	foreach { $_.Remove() }


