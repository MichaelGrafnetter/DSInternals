#Requires -Version 2.0

function Invoke-MsBuild
{
<#
	.SYNOPSIS
	Builds the given Visual Studio solution or project file using MsBuild.

	.DESCRIPTION
	Executes the MsBuild.exe tool against the specified Visual Studio solution or project file.
	Returns a hash table with properties for determining if the build succeeded or not, as well as other information (see the OUTPUTS section for list of properties).
	If using the PathThru switch, the process running MsBuild is returned instead.

	.PARAMETER Path
	The path of the Visual Studio solution or project to build (e.g. a .sln or .csproj file).

	.PARAMETER MsBuildParameters
	Additional parameters to pass to the MsBuild command-line tool. This can be any valid MsBuild command-line parameters except for the path of
	the solution/project to build.

	See http://msdn.microsoft.com/en-ca/library/vstudio/ms164311.aspx for valid MsBuild command-line parameters.

	.PARAMETER Use32BitMsBuild
	If this switch is provided, the 32-bit version of MsBuild.exe will be used instead of the 64-bit version when both are available.

	.PARAMETER BuildLogDirectoryPath
	The directory path to write the build log files to.
	Defaults to putting the log files in the users temp directory (e.g. C:\Users\[User Name]\AppData\Local\Temp).
	Use the keyword "PathDirectory" to put the log files in the same directory as the .sln or project file being built.
	Two log files are generated: one with the complete build log, and one that contains only errors from the build.

	.PARAMETER LogVerbosity
	If set, this will set the verbosity of the build log. Possible values are: q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic].

	.PARAMETER AutoLaunchBuildLogOnFailure
	If set, this switch will cause the build log to automatically be launched into the default viewer if the build fails.
	This log file contains all of the build output.
	NOTE: This switch cannot be used with the PassThru switch.

	.PARAMETER AutoLaunchBuildErrorsLogOnFailure
	If set, this switch will cause the build errors log to automatically be launched into the default viewer if the build fails.
	This log file only contains errors from the build output.
	NOTE: This switch cannot be used with the PassThru switch.

	.PARAMETER KeepBuildLogOnSuccessfulBuilds
	If set, this switch will cause the MsBuild log file to not be deleted on successful builds; normally it is only kept around on failed builds.
	NOTE: This switch cannot be used with the PassThru switch.

	.PARAMETER ShowBuildOutputInNewWindow
	If set, this switch will cause a command prompt window to be shown in order to view the progress of the build.
	By default the build output is not shown in any window.
	NOTE: This switch cannot be used with the ShowBuildOutputInCurrentWindow switch.

	.PARAMETER ShowBuildOutputInCurrentWindow
	If set, this switch will cause the build process to be started in the existing console window, instead of creating a new one.
	By default the build output is not shown in any window.
	NOTE: This switch will override the ShowBuildOutputInNewWindow switch.
	NOTE: There is a problem with the -NoNewWindow parameter of the Start-Process cmdlet; this is used for the ShowBuildOutputInCurrentWindow switch.
		The bug is that in some PowerShell consoles, the build output is not directed back to the console calling this function, so nothing is displayed.
		To avoid the build process from appearing to hang, PromptForInputBeforeClosing only has an effect with ShowBuildOutputInCurrentWindow when running
		in the default "ConsoleHost" PowerShell console window, as we know it works properly with that console (it does not in other consoles like ISE, PowerGUI, etc.).

	.PARAMETER PromptForInputBeforeClosing
	If set, this switch will prompt the user for input after the build completes, and will not continue until the user presses a key.
	NOTE: This switch only has an effect when used with the ShowBuildOutputInNewWindow and ShowBuildOutputInCurrentWindow switches (otherwise build output is not displayed).
	NOTE: This switch cannot be used with the PassThru switch.
	NOTE: The user will need to provide input before execution will return back to the calling script (so do not use this switch for automated builds).
	NOTE: To avoid the build process from appearing to hang, PromptForInputBeforeClosing only has an effect with ShowBuildOutputInCurrentWindow when running
		in the default "ConsoleHost" PowerShell console window, as we know it works properly with that console (it does not in other consoles like ISE, PowerGUI, etc.).

	.PARAMETER MsBuildFilePath
	By default this script will locate and use the latest version of MsBuild.exe on the machine.
	If you have MsBuild.exe in a non-standard location, or want to force the use of an older MsBuild.exe version, you may pass in the file path of the MsBuild.exe to use.

	.PARAMETER VisualStudioDeveloperCommandPromptFilePath
	By default this script will locate and use the latest version of the Visual Studio Developer Command Prompt to run MsBuild.
	If you installed Visual Studio in a non-standard location, or want to force the use of an older Visual Studio Command Prompt version, you may pass in the file path to
	the Visual Studio Command Prompt to use. The filename is typically VsDevCmd.bat.

	.PARAMETER BypassVisualStudioDeveloperCommandPrompt
	By default this script will locate and use the latest version of the Visual Studio Developer Command Prompt to run MsBuild.
	The Visual Studio Developer Command Prompt loads additional variables and paths, so it is sometimes able to build project types that MsBuild cannot build by itself alone.
	However, loading those additional variables and paths sometimes may have a performance impact, so this switch may be provided to bypass it and just use MsBuild directly.

	.PARAMETER PassThru
	If set, this switch will cause the calling script not to wait until the build (launched in another process) completes before continuing execution.
	Instead the build will be started in a new process and that process will immediately be returned, allowing the calling script to continue
	execution while the build is performed, and also to inspect the process to see when it completes.
	NOTE: This switch cannot be used with the AutoLaunchBuildLogOnFailure, AutoLaunchBuildErrorsLogOnFailure, KeepBuildLogOnSuccessfulBuilds, or PromptForInputBeforeClosing switches.

	.PARAMETER WhatIf
	If set, the build will not actually be performed.
	Instead it will just return the result hash table containing the file paths that would be created if the build is performed with the same parameters.

	.OUTPUTS
	When the -PassThru switch is provided, the process being used to run MsBuild.exe is returned.
	When the -PassThru switch is not provided, a hash table with the following properties is returned:

	BuildSucceeded = $true if the build passed, $false if the build failed, and $null if we are not sure.
	BuildLogFilePath = The path to the build's log file.
	BuildErrorsLogFilePath = The path to the build's error log file.
	ItemToBuildFilePath = The item that MsBuild ran against.
	CommandUsedToBuild = The full command that was used to invoke MsBuild. This can be useful for inspecting what parameters are passed to MsBuild.exe.
	Message = A message describing any problems that were encountered by Invoke-MsBuild. This is typically an empty string unless something went wrong.
	MsBuildProcess = The process that was used to execute MsBuild.exe.
	BuildDuration = The amount of time the build took to complete, represented as a TimeSpan.

	.EXAMPLE
	$buildResult = Invoke-MsBuild -Path "C:\Some Folder\MySolution.sln"

	if ($buildResult.BuildSucceeded -eq $true)
	{
		Write-Output ("Build completed successfully in {0:N1} seconds." -f $buildResult.BuildDuration.TotalSeconds)
	}
	elseif ($buildResult.BuildSucceeded -eq $false)
	{
		Write-Output ("Build failed after {0:N1} seconds. Check the build log file '$($buildResult.BuildLogFilePath)' for errors." -f $buildResult.BuildDuration.TotalSeconds)
	}
	elseif ($null -eq $buildResult.BuildSucceeded)
	{
		Write-Output "Unsure if build passed or failed: $($buildResult.Message)"
	}

	Perform the default MsBuild actions on the Visual Studio solution to build the projects in it, and returns a hash table containing the results.
	The PowerShell script will halt execution until MsBuild completes.

	.EXAMPLE
	$process = Invoke-MsBuild -Path "C:\Some Folder\MySolution.sln" -PassThru

	while (!$process.HasExited)
	{
		Write-Host "Solution is still building..."
		Start-Sleep -Seconds 1
	}

	Perform the default MsBuild actions on the Visual Studio solution to build the projects in it.
	The PowerShell script will not halt execution; instead it will return the process running MsBuild.exe back to the caller while the build is performed.
	You can check the process's HasExited property to check if the build has completed yet or not.

	.EXAMPLE
	if ((Invoke-MsBuild -Path $pathToSolution).BuildSucceeded -eq $true)
	{
		Write-Output "Build completed successfully."
	}

	Perform the build against the file specified at $pathToSolution and checks it for success in a single line.

	.EXAMPLE
	Invoke-MsBuild -Path "C:\Some Folder\MyProject.csproj" -MsBuildParameters "/target:Clean;Build" -ShowBuildOutputInNewWindow

	Cleans then Builds the given C# project.
	A window displaying the output from MsBuild will be shown so the user can view the progress of the build without it polluting their current terminal window.

	.EXAMPLE
	Invoke-MsBuild -Path "C:\Some Folder\MyProject.csproj" -ShowBuildOutputInCurrentWindow

	Builds the given C# project and displays the output from MsBuild in the current terminal window.

	.EXAMPLE
	Invoke-MsBuild -Path "C:\MySolution.sln" -Params "/target:Clean;Build /property:Configuration=Release;Platform=x64;BuildInParallel=true /verbosity:Detailed /maxcpucount"

	Cleans then Builds the given solution, specifying to build the project in parallel in the Release configuration for the x64 platform.
	Here the shorter "Params" alias is used instead of the full "MsBuildParameters" parameter name.

	.EXAMPLE
	Invoke-MsBuild -Path "C:\Some Folder\MyProject.csproj" -ShowBuildOutputInNewWindow -PromptForInputBeforeClosing -AutoLaunchBuildLogOnFailure

	Builds the given C# project.
	A window displaying the output from MsBuild will be shown so the user can view the progress of the build, and it will not close until the user
	gives the window some input after the build completes. This function will also not return until the user gives the window some input, halting the powershell script execution.
	If the build fails, the build log will automatically be opened in the default text viewer.

	.EXAMPLE
	Invoke-MsBuild -Path "C:\Some Folder\MyProject.csproj" -BuildLogDirectoryPath "C:\BuildLogs" -KeepBuildLogOnSuccessfulBuilds -AutoLaunchBuildErrorsLogOnFailure

	Builds the given C# project.
	The build log will be saved in "C:\BuildLogs", and they will not be automatically deleted even if the build succeeds.
	If the build fails, the build errors log will automatically be opened in the default text viewer.

	.EXAMPLE
	Invoke-MsBuild -Path "C:\Some Folder\MyProject.csproj" -BuildLogDirectoryPath PathDirectory

	Builds the given C# project.
	The keyword 'PathDirectory' is used, so the build log will be saved in "C:\Some Folder\", which is the same directory as the project being built (i.e. directory specified in the Path).

	.EXAMPLE
	Invoke-MsBuild -Path "C:\Database\Database.dbproj" -P "/t:Deploy /p:TargetDatabase=MyDatabase /p:TargetConnectionString=`"Data Source=DatabaseServerName`;Integrated Security=True`;Pooling=False`" /p:DeployToDatabase=True"

	Deploy the Visual Studio Database Project to the database "MyDatabase".
	Here the shorter "P" alias is used instead of the full "MsBuildParameters" parameter name.
	The shorter alias' of the MsBuild parameters are also used; "/t" instead of "/target", and "/p" instead of "/property".

	.EXAMPLE
	Invoke-MsBuild -Path "C:\Some Folder\MyProject.csproj" -WhatIf

	Returns the result hash table containing the same property values that would be created if the build was ran with the same parameters.
	The BuildSucceeded property will be $null since no build will actually be invoked.
	This will display all of the returned hash table's properties and their values.

	.EXAMPLE
	Invoke-MsBuild -Path "C:\Some Folder\MyProject.csproj" > $null

	Builds the given C# project, discarding the result hash table and not displaying its properties.

	.LINK
	Project home: https://github.com/deadlydog/Invoke-MsBuild

	.NOTES
	Name:   Invoke-MsBuild
	Author: Daniel Schroeder (originally based on the module at http://geekswithblogs.net/dwdii/archive/2011/05/27/part-2-automating-a-visual-studio-build-with-powershell.aspx)
	Version: 2.7.1
#>
	[CmdletBinding(SupportsShouldProcess, DefaultParameterSetName="Wait")]
	param
	(
		[parameter(Position=0,Mandatory=$true,ValueFromPipeline=$true,HelpMessage="The path to the file to build with MsBuild (e.g. a .sln or .csproj file).")]
		[ValidateScript({Test-Path -LiteralPath $_ -PathType Leaf})]
		[string] $Path,

		[parameter(Mandatory=$false)]
		[Alias("Parameters","Params","P")]
		[string] $MsBuildParameters,

		[parameter(Mandatory=$false)]
		[switch] $Use32BitMsBuild,

		[parameter(Mandatory=$false,HelpMessage="The directory path to write the build log file to. Use the keyword 'PathDirectory' to put the log file in the same directory as the .sln or project file being built.")]
		[ValidateNotNullOrEmpty()]
		[Alias("LogDirectory","L")]
		[string] $BuildLogDirectoryPath = $env:Temp,

		[parameter(Mandatory=$false)]
		[ValidateSet('q','quiet','m','minimal','n','normal','d','detailed','diag','diagnostic')]
		[string] $LogVerbosityLevel = 'normal',

		[parameter(Mandatory=$false,ParameterSetName="Wait")]
		[ValidateNotNullOrEmpty()]
		[switch] $AutoLaunchBuildLogOnFailure,

		[parameter(Mandatory=$false,ParameterSetName="Wait")]
		[ValidateNotNullOrEmpty()]
		[switch] $AutoLaunchBuildErrorsLogOnFailure,

		[parameter(Mandatory=$false,ParameterSetName="Wait")]
		[ValidateNotNullOrEmpty()]
		[switch] $KeepBuildLogOnSuccessfulBuilds,

		[parameter(Mandatory=$false)]
		[Alias("ShowBuildWindow")]
		[switch] $ShowBuildOutputInNewWindow,

		[parameter(Mandatory=$false)]
		[switch] $ShowBuildOutputInCurrentWindow,

		[parameter(Mandatory=$false,ParameterSetName="Wait")]
		[switch] $PromptForInputBeforeClosing,

		[parameter(Mandatory=$false)]
		[ValidateScript({Test-Path -LiteralPath $_ -PathType Leaf})]
		[string] $MsBuildFilePath,

		[parameter(Mandatory=$false)]
		[ValidateScript({Test-Path -LiteralPath $_ -PathType Leaf})]
		[string] $VisualStudioDeveloperCommandPromptFilePath,

		[parameter(Mandatory=$false)]
		[switch] $BypassVisualStudioDeveloperCommandPrompt,

		[parameter(Mandatory=$false,ParameterSetName="PassThru")]
		[switch] $PassThru
	)

	BEGIN { }
	END { }
	PROCESS
	{
		# Turn on Strict Mode to help catch syntax-related errors.
		# 	This must come after a script's/function's param section.
		# 	Forces a function to be the first non-comment code to appear in a PowerShell Script/Module.
		Set-StrictMode -Version Latest

		# Ignore cultural differences. This is so that when reading version numbers it does not change the '.' to ',' when the OS's language/culture is not English.
		[System.Threading.Thread]::CurrentThread.CurrentCulture = [CultureInfo]::InvariantCulture

		# Default the ParameterSet variables that may not have been set depending on which parameter set is being used. This is required for PowerShell v2.0 compatibility.
		if (!(Test-Path Variable:Private:AutoLaunchBuildLogOnFailure)) { $AutoLaunchBuildLogOnFailure = $false }
		if (!(Test-Path Variable:Private:AutoLaunchBuildErrorsLogOnFailure)) { $AutoLaunchBuildErrorsLogOnFailure = $false }
		if (!(Test-Path Variable:Private:KeepBuildLogOnSuccessfulBuilds)) { $KeepBuildLogOnSuccessfulBuilds = $false }
		if (!(Test-Path Variable:Private:PromptForInputBeforeClosing)) { $PromptForInputBeforeClosing = $false }
		if (!(Test-Path Variable:Private:PassThru)) { $PassThru = $false }

		# If the keyword was supplied, place the log in the same folder as the solution/project being built.
		if ($BuildLogDirectoryPath.Equals("PathDirectory", [System.StringComparison]::InvariantCultureIgnoreCase))
		{
			$BuildLogDirectoryPath = [System.IO.Path]::GetDirectoryName($Path)
		}

		# Always get the full path to the Log files directory.
		$BuildLogDirectoryPath = [System.IO.Path]::GetFullPath($BuildLogDirectoryPath)

		# Local Variables.
		$solutionFileName = (Get-ItemProperty -LiteralPath $Path).Name
		$buildLogFilePath = (Join-Path -Path $BuildLogDirectoryPath -ChildPath $solutionFileName) + ".msbuild.log"
		$buildErrorsLogFilePath = (Join-Path -Path $BuildLogDirectoryPath -ChildPath $solutionFileName) + ".msbuild.errors.log"
		$windowStyleOfNewWindow = if ($ShowBuildOutputInNewWindow) { "Normal" } else { "Hidden" }

		# Build our hash table that will be returned.
		$result = @{}
		$result.BuildSucceeded = $null
		$result.BuildLogFilePath = $buildLogFilePath
		$result.BuildErrorsLogFilePath = $buildErrorsLogFilePath
		$result.ItemToBuildFilePath = $Path
		$result.CommandUsedToBuild = [string]::Empty
		$result.Message = [string]::Empty
		$result.MsBuildProcess = $null
		$result.BuildDuration = [TimeSpan]::Zero

		# Try and build the solution.
		try
		{
			# Get the verbosity to use for the MsBuild log file.
			$verbosityLevel = switch ($LogVerbosityLevel) {
				{ ($_ -eq "q")    -or ($_ -eq "quiet") -or `
				  ($_ -eq "m")    -or ($_ -eq "minimal") -or `
				  ($_ -eq "n")    -or ($_ -eq "normal") -or `
				  ($_ -eq "d")    -or ($_ -eq "detailed") -or `
				  ($_ -eq "diag") -or ($_ -eq "diagnostic") } { ";verbosity=$_" ;break }
				default { "" }
			}

			# Build the arguments to pass to MsBuild.
			$buildArguments = """$Path"" $MsBuildParameters /fileLoggerParameters:LogFile=""$buildLogFilePath""$verbosityLevel /fileLoggerParameters1:LogFile=""$buildErrorsLogFilePath"";errorsonly"

			# Get the path to the MsBuild executable.
			$msBuildPath = $MsBuildFilePath
			[bool] $msBuildPathWasNotProvided = [string]::IsNullOrEmpty($msBuildPath)
			if ($msBuildPathWasNotProvided)
			{
				$msBuildPath = Get-LatestMsBuildPath -Use32BitMsBuild:$Use32BitMsBuild
			}

			# If we plan on trying to use the VS Command Prompt, we'll need to get the path to it.
			[bool] $vsCommandPromptPathWasFound = $false
			if (!$BypassVisualStudioDeveloperCommandPrompt)
			{
				# Get the path to the Visual Studio Developer Command Prompt file.
				$vsCommandPromptPath = $VisualStudioDeveloperCommandPromptFilePath
				[bool] $vsCommandPromptPathWasNotProvided = [string]::IsNullOrEmpty($vsCommandPromptPath)
				if ($vsCommandPromptPathWasNotProvided)
				{
					$vsCommandPromptPath = Get-LatestVisualStudioCommandPromptPath
				}
				$vsCommandPromptPathWasFound = ![string]::IsNullOrEmpty($vsCommandPromptPath)
			}

			# If we should use the VS Command Prompt, call MsBuild from that since it sets environmental variables that may be needed to build some projects types (e.g. XNA).
			$useVsCommandPrompt = !$BypassVisualStudioDeveloperCommandPrompt -and $vsCommandPromptPathWasFound
			if ($useVsCommandPrompt)
			{
				$cmdArgumentsToRunMsBuild = "/k "" ""$vsCommandPromptPath"" & ""$msBuildPath"" "
			}
			# Else we won't be using the VS Command Prompt, so just build using MsBuild directly.
			else
			{
				$cmdArgumentsToRunMsBuild = "/k "" ""$msBuildPath"" "
			}

			# Append the MsBuild arguments to pass into cmd.exe in order to do the build.
			$cmdArgumentsToRunMsBuild += "$buildArguments "

			# If necessary, add a pause to wait for input before exiting the cmd.exe window.
			# No pausing allowed when using PassThru or not showing the build output.
			# The -NoNewWindow parameter of Start-Process does not behave correctly in the ISE and other PowerShell hosts (doesn't display any build output),
			# so only allow it if in the default PowerShell host, since we know that one works.
			$pauseForInput = [string]::Empty
			if ($PromptForInputBeforeClosing -and !$PassThru `
				-and ($ShowBuildOutputInNewWindow -or ($ShowBuildOutputInCurrentWindow -and $Host.Name -eq "ConsoleHost")))
			{ $pauseForInput = "Pause & " }
			$cmdArgumentsToRunMsBuild += "& $pauseForInput Exit"" "

			# Record the exact command used to perform the build to make it easier to troubleshoot issues with builds.
			$result.CommandUsedToBuild = "cmd.exe $cmdArgumentsToRunMsBuild"

			# If we don't actually want to perform a build (i.e. the -WhatIf parameter was specified), return the object without actually doing the build.
			if (!($pscmdlet.ShouldProcess($result.ItemToBuildFilePath, 'MsBuild')))
			{
				$result.BuildSucceeded = $null
				$result.Message = "The '-WhatIf' switch was specified, so a build was not invoked."
				return $result
			}

			Write-Debug "Starting new cmd.exe process with arguments ""$cmdArgumentsToRunMsBuild""."

			# Perform the build.
			if ($PassThru)
			{
				if ($ShowBuildOutputInCurrentWindow)
				{
					return Start-Process cmd.exe -ArgumentList $cmdArgumentsToRunMsBuild -NoNewWindow -PassThru
				}
				else
				{
					return Start-Process cmd.exe -ArgumentList $cmdArgumentsToRunMsBuild -WindowStyle $windowStyleOfNewWindow -PassThru
				}
			}
			else
			{
				$performBuildScriptBlock =
				{
					if ($ShowBuildOutputInCurrentWindow)
					{
						$result.MsBuildProcess = Start-Process cmd.exe -ArgumentList $cmdArgumentsToRunMsBuild -NoNewWindow -PassThru
					}
					else
					{
						$result.MsBuildProcess = Start-Process cmd.exe -ArgumentList $cmdArgumentsToRunMsBuild -WindowStyle $windowStyleOfNewWindow -PassThru
					}

					Wait-Process -InputObject $result.MsBuildProcess
				}

				# Perform the build and record how long it takes.
				$result.BuildDuration = (Measure-Command -Expression $performBuildScriptBlock)
			}
		}
		# If the build crashed, return that the build didn't succeed.
		catch
		{
			$errorMessage = $_
			$result.Message = "Unexpected error occurred while building ""$Path"": $errorMessage"
			$result.BuildSucceeded = $false

			Write-Error ($result.Message)
			return $result
		}

		# If we can't find the build's log file in order to inspect it, write a warning and return null.
		if (!(Test-Path -LiteralPath $buildLogFilePath -PathType Leaf))
		{
			$result.BuildSucceeded = $null
			$result.Message = "Cannot find the build log file at '$buildLogFilePath', so unable to determine if build succeeded or not."

			Write-Warning ($result.Message)
			return $result
		}

		# Get if the build succeeded or not.
		[bool] $buildOutputDoesNotContainFailureMessage = $null -eq (Select-String -Path $buildLogFilePath -Pattern "Build FAILED." -SimpleMatch)
		[bool] $buildReturnedSuccessfulExitCode = $result.MsBuildProcess.ExitCode -eq 0
		$buildSucceeded = $buildOutputDoesNotContainFailureMessage -and $buildReturnedSuccessfulExitCode

		# If the build succeeded.
		if ($buildSucceeded)
		{
			$result.BuildSucceeded = $true

			# If we shouldn't keep the log files around, delete them.
			if (!$KeepBuildLogOnSuccessfulBuilds)
			{
				if (Test-Path -LiteralPath $buildLogFilePath -PathType Leaf) { Remove-Item -LiteralPath $buildLogFilePath -Force }
				if (Test-Path -LiteralPath $buildErrorsLogFilePath -PathType Leaf) { Remove-Item -LiteralPath $buildErrorsLogFilePath -Force }
			}
		}
		# Else at least one of the projects failed to build.
		else
		{
			$result.BuildSucceeded = $false
			$result.Message = "FAILED to build ""$Path"". Please check the build log ""$buildLogFilePath"" for details."

			# Write the error message as a warning.
			Write-Warning ($result.Message)

			# If we should show the build logs automatically, open them with the default viewer.
			if($AutoLaunchBuildLogOnFailure)
			{
				Open-BuildLogFileWithDefaultProgram -FilePathToOpen $buildLogFilePath -Result ([ref]$result)
			}
			if($AutoLaunchBuildErrorsLogOnFailure)
			{
				Open-BuildLogFileWithDefaultProgram -FilePathToOpen $buildErrorsLogFilePath -Result ([ref]$result)
			}
		}

		# Return the results of the build.
		return $result
	}
}

function Open-BuildLogFileWithDefaultProgram([string]$FilePathToOpen, [ref]$Result)
{
	if (Test-Path -LiteralPath $FilePathToOpen -PathType Leaf)
	{
		Start-Process -verb "Open" $FilePathToOpen
	}
	else
	{
		$message = "Could not auto-launch the build log because the expected file does not exist at '$FilePathToOpen'."
		$Result.Message += [System.Environment]::NewLine + $message
		Write-Warning $message
	}
}

function Get-LatestVisualStudioCommandPromptPath
{
<#
	.SYNOPSIS
		Gets the file path to the latest Visual Studio Command Prompt. Returns $null if a path is not found.

	.DESCRIPTION
		Gets the file path to the latest Visual Studio Command Prompt. Returns $null if a path is not found.
#>
	[string] $vsCommandPromptPath = Get-VisualStudioCommandPromptPathForVisualStudio2017AndNewer

	# If VS 2017 or newer VS Command Prompt was not found, check for older versions of VS Command Prompt.
	if ([string]::IsNullOrEmpty($vsCommandPromptPath))
	{
		$vsCommandPromptPath = Get-VisualStudioCommandPromptPathForVisualStudio2015AndPrior
	}

	return $vsCommandPromptPath
}

function Get-VisualStudioCommandPromptPathForVisualStudio2017AndNewer
{
	# Later we can probably make use of the VSSetup.PowerShell module to find the MsBuild.exe: https://github.com/Microsoft/vssetup.powershell
	# Or perhaps the VsWhere.exe: https://github.com/Microsoft/vswhere
	# But for now, to keep this script PowerShell 2.0 compatible and not rely on external executables, let's look for it ourselves in known locations.
	# Example of known locations:
	#	"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\VsDevCmd.bat"

	[string] $visualStudioDirectoryPath = Get-CommonVisualStudioDirectoryPath
	[bool] $visualStudioDirectoryPathDoesNotExist = [string]::IsNullOrEmpty($visualStudioDirectoryPath)
	if ($visualStudioDirectoryPathDoesNotExist)
	{
		return $null
	}

	# First search for the VS Command Prompt in the expected locations (faster).
	$expectedVsCommandPromptPathWithWildcards = "$visualStudioDirectoryPath\*\*\Common7\Tools\VsDevCmd.bat"
	$vsCommandPromptPathObjects = Get-Item -Path $expectedVsCommandPromptPathWithWildcards

	[bool] $vsCommandPromptWasNotFound = ($null -eq $vsCommandPromptPathObjects) -or ($vsCommandPromptPathObjects.Length -eq 0)
	if ($vsCommandPromptWasNotFound)
	{
		# Recursively search the entire Microsoft Visual Studio directory for the VS Command Prompt (slower, but will still work if MS changes folder structure).
		Write-Verbose "The Visual Studio Command Prompt was not found at an expected location. Searching more locations, but this will be a little slow."
		$vsCommandPromptPathObjects = Get-ChildItem -Path $visualStudioDirectoryPath -Recurse | Where-Object { $_.Name -ieq 'VsDevCmd.bat' }
	}

	$vsCommandPromptPathObjectsSortedWithNewestVersionsFirst = $vsCommandPromptPathObjects | Sort-Object -Property FullName -Descending

	$newestVsCommandPromptPath = $vsCommandPromptPathObjectsSortedWithNewestVersionsFirst | Select-Object -ExpandProperty FullName -First 1
	return $newestVsCommandPromptPath
}

function Get-VisualStudioCommandPromptPathForVisualStudio2015AndPrior
{
	# Get some environmental paths.
	$vs2015CommandPromptPath = $env:VS140COMNTOOLS + 'VsDevCmd.bat'
	$vs2013CommandPromptPath = $env:VS120COMNTOOLS + 'VsDevCmd.bat'
	$vs2012CommandPromptPath = $env:VS110COMNTOOLS + 'VsDevCmd.bat'
	$vs2010CommandPromptPath = $env:VS100COMNTOOLS + 'vcvarsall.bat'
	$potentialVsCommandPromptPaths = @($vs2015CommandPromptPath, $vs2013CommandPromptPath, $vs2012CommandPromptPath, $vs2010CommandPromptPath)

	# Store the VS Command Prompt to do the build in, if one exists.
	$newestVsCommandPromptPath = $null
	foreach ($path in $potentialVsCommandPromptPaths)
	{
		[bool] $pathExists = (![string]::IsNullOrEmpty($path)) -and (Test-Path -LiteralPath $path -PathType Leaf)
		if ($pathExists)
		{
			$newestVsCommandPromptPath = $path
			break
		}
	}

	# Return the path to the VS Command Prompt if it was found.
	return $newestVsCommandPromptPath
}

function Get-LatestMsBuildPath_NotUsedYetButCouldBeIfNeeded
{
	[bool] $vsSetupExists = $null -ne (Get-Command Get-VSSetupInstance -ErrorAction SilentlyContinue)
	if (!$vsSetupExists)
	{
		Write-Verbose "Importing the VSSetup module in order to determine TF.exe path..." -Verbose
		Install-Module VSSetup -Scope CurrentUser -Force
	}
	[string] $visualStudioInstallationPath = (Get-VSSetupInstance | Select-VSSetupInstance -Latest -Require Microsoft.Component.MSBuild).InstallationPath
	$msBuildExecutableFilePath = (Get-ChildItem $visualStudioInstallationPath -Recurse -Filter "MsBuild.exe" | Select-Object -First 1).FullName
	return $msBuildExecutableFilePath
}

function Get-LatestMsBuildPath([switch] $Use32BitMsBuild)
{
<#
	.SYNOPSIS
	Gets the path to the latest version of MsBuild.exe. Throws an exception if MsBuild.exe is not found.

	.DESCRIPTION
	Gets the path to the latest version of MsBuild.exe. Throws an exception if MsBuild.exe is not found.
#>

	[string] $msBuildPath = $null
	$msBuildPath = Get-MsBuildPathForVisualStudio2017AndNewer -Use32BitMsBuild $Use32BitMsBuild

	# If VS 2017 or newer MsBuild.exe was not found, check for older versions of MsBuild.
	if ([string]::IsNullOrEmpty($msBuildPath))
	{
		$msBuildPath = Get-MsBuildPathForVisualStudio2015AndPrior -Use32BitMsBuild $Use32BitMsBuild
	}

	[bool] $msBuildPathWasNotFound = [string]::IsNullOrEmpty($msBuildPath)
	if ($msBuildPathWasNotFound)
	{
		throw 'Could not determine where to find MsBuild.exe.'
	}

	[bool] $msBuildExistsAtThePathFound = (Test-Path -LiteralPath $msBuildPath -PathType Leaf)
	if(!$msBuildExistsAtThePathFound)
	{
		throw "MsBuild.exe does not exist at the expected path, '$msBuildPath'."
	}

	return $msBuildPath
}

function Get-MsBuildPathForVisualStudio2017AndNewer([bool] $Use32BitMsBuild)
{
	# Later we can probably make use of the VSSetup.PowerShell module to find the MsBuild.exe: https://github.com/Microsoft/vssetup.powershell
	# Or perhaps the VsWhere.exe: https://github.com/Microsoft/vswhere
	# But for now, to keep this script PowerShell 2.0 compatible and not rely on external executables, let's look for it ourselves in known locations.
	# Example of known locations:
	# 	"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe" - 32 bit
	# 	"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\amd64\MSBuild.exe" - 64 bit
	#	"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe" -32 bit
	#	"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\amd64\MSBuild.exe" - 64 bit

	[string] $visualStudioDirectoryPath = Get-CommonVisualStudioDirectoryPath
	[bool] $visualStudioDirectoryPathDoesNotExist = [string]::IsNullOrEmpty($visualStudioDirectoryPath)
	if ($visualStudioDirectoryPathDoesNotExist)
	{
		return $null
	}

	# First search for MsBuild in the expected 32 and 64 bit locations (faster).
	$expected32bitPathWithWildcards = "$visualStudioDirectoryPath\*\*\MsBuild\*\Bin\MsBuild.exe"
	$expected64bitPathWithWildcards = "$visualStudioDirectoryPath\*\*\MsBuild\*\Bin\amd64\MsBuild.exe"
	$msBuildPathObjects = Get-Item -Path $expected32bitPathWithWildcards, $expected64bitPathWithWildcards

	[bool] $msBuildWasNotFound = ($null -eq $msBuildPathObjects) -or ($msBuildPathObjects.Length -eq 0)
	if ($msBuildWasNotFound)
	{
		# Recursively search the entire Microsoft Visual Studio directory for MsBuild (slower, but will still work if MS changes folder structure).
		Write-Verbose "MsBuild.exe was not found at an expected location. Searching more locations, but this will be a little slow."
		$msBuildPathObjects = Get-ChildItem -Path $visualStudioDirectoryPath -Recurse | Where-Object { $_.Name -ieq 'MsBuild.exe' }
	}

	$msBuildPathObjectsSortedWithNewestVersionsFirst = $msBuildPathObjects | Sort-Object -Property FullName -Descending

	$newest32BitMsBuildPath = $msBuildPathObjectsSortedWithNewestVersionsFirst | Where-Object { $_.Directory.Name -ine 'amd64' } | Select-Object -ExpandProperty FullName -First 1
	$newest64BitMsBuildPath = $msBuildPathObjectsSortedWithNewestVersionsFirst | Where-Object { $_.Directory.Name -ieq 'amd64' } | Select-Object -ExpandProperty FullName -First 1

	if ($Use32BitMsBuild)
	{
		return $newest32BitMsBuildPath
	}
	return $newest64BitMsBuildPath
}

function Get-MsBuildPathForVisualStudio2015AndPrior([bool] $Use32BitMsBuild)
{
	$registryPathToMsBuildToolsVersions = 'HKLM:\SOFTWARE\Microsoft\MSBuild\ToolsVersions\'
	if ($Use32BitMsBuild)
	{
		# If the 32-bit path exists, use it, otherwise stick with the current path (which will be the 64-bit path on 64-bit machines, and the 32-bit path on 32-bit machines).
		$registryPathTo32BitMsBuildToolsVersions = 'HKLM:\SOFTWARE\Wow6432Node\Microsoft\MSBuild\ToolsVersions\'
		if (Test-Path -Path $registryPathTo32BitMsBuildToolsVersions)
		{
			$registryPathToMsBuildToolsVersions = $registryPathTo32BitMsBuildToolsVersions
		}
	}

	# Backup the current culture and force using English US to ensure version numbers are in the expected format (e.g. 17.0).
	$previousCulture = [System.Threading.Thread]::CurrentThread.CurrentCulture
	[System.Threading.Thread]::CurrentThread.CurrentCulture = 'en-US'

	# Get the path to the directory that the latest version of MsBuild is in.
	$msBuildToolsVersionsStrings = Get-ChildItem -Path $registryPathToMsBuildToolsVersions | Where-Object { $_ -match '[0-9]+\.[0-9]' } | Select-Object -ExpandProperty PsChildName
	$msBuildToolsVersions = @{}
	$msBuildToolsVersionsStrings | ForEach-Object {$msBuildToolsVersions.Add($_ -as [double], $_)}
	$largestMsBuildToolsVersion = ($msBuildToolsVersions.GetEnumerator() | Sort-Object -Descending -Property Name | Select-Object -First 1).Value
	$registryPathToMsBuildToolsLatestVersion = Join-Path -Path $registryPathToMsBuildToolsVersions -ChildPath ("{0:n1}" -f $largestMsBuildToolsVersion)
	$msBuildToolsVersionsKeyToUse = Get-Item -Path $registryPathToMsBuildToolsLatestVersion
	$msBuildDirectoryPath = $msBuildToolsVersionsKeyToUse | Get-ItemProperty -Name 'MSBuildToolsPath' | Select-Object -ExpandProperty 'MSBuildToolsPath'

	# Restore the previous culture now that we're done comparing version numbers.
	[System.Threading.Thread]::CurrentThread.CurrentCulture = $previousCulture

	if(!$msBuildDirectoryPath)
	{
		return $null
	}

	# Build the expected path to the MsBuild executable.
	$msBuildPath = (Join-Path -Path $msBuildDirectoryPath -ChildPath 'MsBuild.exe')

	return $msBuildPath
}

function Get-CommonVisualStudioDirectoryPath
{
	# As of VS 2022 Visual Studio is 64-bit, so look in "Program Files" directory before "Program Files (x86)".
	[string] $programFilesDirectory = $null
	try
	{
		$programFilesDirectory = Get-Item 'Env:\ProgramFiles' -ErrorAction SilentlyContinue | Select-Object -ExpandProperty Value
	}
	catch
	{ }

	if ([string]::IsNullOrEmpty($programFilesDirectory))
	{
		$programFilesDirectory = 'C:\Program Files'
	}

	[string] $visualStudioDirectoryPath = Join-Path -Path $programFilesDirectory -ChildPath 'Microsoft Visual Studio'
	[bool] $visualStudioDirectoryPathExists = (Test-Path -LiteralPath $visualStudioDirectoryPath -PathType Container)
	if (!$visualStudioDirectoryPathExists)
	{
		# Look for Visual Studio in "Program Files (x86)" directory.
		try
		{
			$programFilesDirectory = Get-Item 'Env:\ProgramFiles(x86)' -ErrorAction SilentlyContinue | Select-Object -ExpandProperty Value
		}
		catch
		{
			$programFilesDirectory = $null
		}

		if ([string]::IsNullOrEmpty($programFilesDirectory))
		{
			$programFilesDirectory = 'C:\Program Files (x86)'
		}
	}

	$visualStudioDirectoryPath = Join-Path -Path $programFilesDirectory -ChildPath 'Microsoft Visual Studio'
	$visualStudioDirectoryPathExists = (Test-Path -LiteralPath $visualStudioDirectoryPath -PathType Container)
	if (!$visualStudioDirectoryPathExists)
	{
		return $null
	}
	return $visualStudioDirectoryPath
}

Export-ModuleMember -Function Invoke-MsBuild
