function Invoke-MsTest
{
<#
	.SYNOPSIS
		Run tests for a given Visual Studio solution or project using MsTest
	
	.DESCRIPTION
		Parses each Visual Studio solution or project and extracts test containers.
		Test each file using MsTest.exe to create a single .trx file.
		Optionally user may choose to only run specific test names.
		Optionally user may view or keep the MsTest.exe command window open to view results.
	
	.PARAMETER Path
		The path of the Visual Studio solution or project to build (e.g. a .sln or .csproj file).
	
	.PARAMETER Tests
		A string array with the test names to be ran. 
		By Default the script will run all test associated with the project or solution.
		
	.PARAMETER Unique
		Switch that will only run tests who’s name exactly matches those listed in Tests.
	
	.PARAMETER ResultsDirectoryPath
		The directory path to create the test result file (.trx)
		By Defaults the test file will be written to the users temp directory (e.g. C:\Users\[User Name]\AppData\Local\Temp).
		
	.PARAMETER Verbose
		Switch will print test information to the PowerShell console.
	
	.PARAMETER ShowTestWindow
		Switch will show the cmd window running MsTest.exe
		
	.PARAMETER PromptToCloseTestWindow
		Switch that gives users the option of keeping the cmd window running MsTest.exe open once all the tests are complete.
		
	.PARAMETER NoResults
		Switch will block the results object from returning (Cannot be used with IsAllPassed).
		
	.PARAMETER IsAllPassed
		Switch will return a boolean true if all test pass else a false (Cannot be used with NoResults).
		
	.PARAMETER MsTestParameters
		Additionally MsTest Parameters that the user may wish to include (http://msdn.microsoft.com/en-US/library/ms182489(v=vs.80).aspx)
		Note: This script uses /testcontainer to run tests and will not support /testmetadata parameters.
	
	.OUTPUTS
		If NoResults is not selected then the script will pass an object with Outcomes, Test names (or project/solution names), Storage (container dll), and Comments.
		If all tests passed, there will be no output.

	.EXAMPLE
		Import-Module "C:\Path To Module\Inoke-MsTest.psm1"
		$TestResults = Invoke-MsTest -Path "C:\Some Folder\MySolution.sln"
	
		Foreach ($TestResult in $TestResults)
		{
			if ($TestResult.Outcome -eq "Failed")
			{
				Write-Host $TestResult.Name -ForgroundColor Red
			}
		}
	
		The default test will run all test under a given Visual Studio solution or project and return a list of failed tests, project, or solutions.
		The above example will print the name of all test failures to the console.
	
	.EXAMPLE
		$TestFailures = Invoke-MsTest -Path "C:\Some Folder\MySolution.sln" -Tests MyTest1,MyTest2,MyTest3
	
		Listing tests will only run tests that match the test name given.
		Note: MyTest1 will match with MyTest10, MyTest11, MyTest100 to use only exact matching test names use the 'Unique' switch
	
	.EXAMPLE
		Invoke-MsTest -Path "C:\Some Folder\MyProject.csproj" -ResultsDirectoryPath "C:\Some Folder"
	
		Runs test and drops the test results file in the "C:\Some Folder" directory
		
	.EXAMPLE
		Invoke-MsTest -Path "C:\Some Folder\MyProject.csproj" -Verbose -NoResults
		
		Runs test and print test projess/results to the console. This will not pass back the results object.
		
	.EXAMPLE
		Invoke-MsTest -Path "C:\Some Folder\MySolution.sln" -PromptToCloseTestWindow
		
		Runs test and keep the cmd MsTest.exe window open awaiting user input.
	
	.LINK
		Project home: https://invokemstest.codeplex.com/
	
	.NOTES
		Name:   Invoke-MsTest
		Author: Paul Selles [http://paulselles.wordpress.com/]
		Version: 2.0
		Inspired by my colleague, Daniel Schroeder's Invoke-MsBuild [https://invokemsbuild.codeplex.com]
#>
	[CmdletBinding(DefaultParameterSetName="Wait")]
	param
	(
		# List the root path for your projects or solutions
		#	this will find all test containers within and run them
		[Parameter(Position=0,Mandatory=$true,ValueFromPipeline=$true)]
		[ValidateScript({(Test-Path $_ -PathType Leaf) -and (((Dir $_).Extension -eq ".sln") -or ((Dir $_).Extension -match '.*..*proj'))})]
		[String[]]$Path,	
		
		# List of names of the specific test to run, if nothing is
		# 	specified then run all tests in the test containers
		[parameter(Mandatory=$false)]
		[ValidateNotNullOrEmpty()]
		[String[]]$Tests=$null,
		
		# Will only run tests that exactly match the test name 'Tests' parameter
		[Parameter(Mandatory=$false)]
		[Alias("U")]
		[Switch]$Unique,
		
		# Option to select the location of the result files
		[parameter(Mandatory=$false)]
		[ValidateScript({Test-Path $_ -PathType Container})]
		[Alias("Results")]
		[Alias("R")]
		[string]$ResultsDirectoryPath=$env:Temp,
			
		# Show the tests running in a command window
		[parameter(Mandatory=$false)]
		[Alias("Show")]
		[Alias("S")]
		[switch] $ShowTestWindow,
		
		[parameter(Mandatory=$false)]
		[Alias("Prompt")]
		[switch] $PromptToCloseTestWindow,
		
		# Will not return test results
		[parameter(Mandatory=$false,ParameterSetName="NoResults")]
		[Switch]$NoResults,
		
		# Will return a boolean result, $true if all tests pass otherwise $false
		[parameter(Mandatory=$false,ParameterSetName="IsAllPassed")]
		[Alias("BoolResults")]
		[Switch]$IsAllPassed,
		
		# Desired test run parameters
		[parameter(Mandatory=$false)]
		[ValidateNotNullOrEmpty()]
		[Alias("Params")]
		[Alias("P")]
		[string]$MsTestParameters
	)
	
	BEGIN { }
	END { }
	PROCESS
	{
		Set-StrictMode -Version Latest
	
		foreach ($PathIteration in $Path) {
			# Array for our test results
			$TestResults = @()
		
			# Sets the verbose Parameter
			if ($PSBoundParameters['Verbose']) { $Verbose = $true }
			else { $Verbose = $false }
		
			# Get the path to the MsTest executable
			# Exit if MsTest path is invalid
			$MsTest = Get-MsTest
			if (-not (Test-Path $MsTest)) { return }
		
			# Array for out container and projects
			$TestContainers = @()
			$Projects = @()
			
			# Get a list of project under the supplied solution file
			if (((Dir $PathIteration).Extension -eq ".sln"))
			{
				Get-Content -Path $PathIteration | % {
					if ($_ -match '\s*Project.+=\s*.*,\s*\"\s*(.*proj)\s*\"\s*,\s*') {
						$Projects += Join-Path -Path (Dir $PathIteration).DirectoryName $matches[1]
					}
				}		
			}
			# If a project file we don't need to so much
			elseif ((Dir $PathIteration).Extension -match '.*..*proj')
			{
				$Projects += $PathIteration
			}
			
			# Loop through all project files and 
			Foreach ($Project in $Projects)
			{
				$ProjectXml = [Xml](Get-Content -Path $Project)
				$ns = New-Object Xml.XmlNamespaceManager $ProjectXml.NameTable
				$ns.AddNamespace('dns','http://schemas.microsoft.com/developer/msbuild/2003')
				
				# Test is the project includes the unit test class
				if ([Bool]($ProjectXml.SelectNodes('dns:Project//dns:ItemGroup/dns:Reference', $ns) | Where {$_.Include -match 'Microsoft.VisualStudio.QualityTools.UnitTestFramework'}))
				{			
					# Collect all possible configurations
					$Configurations = $ProjectXml.SelectNodes('dns:Project/dns:PropertyGroup[@Condition]', $ns).Condition
				
					# Collect all PropertyGroup Nodes referencing IntermediateOutputPath and BaseIntermediateOutputPath elements
					$IntermediateOutputPathNodes = $ProjectXml.SelectNodes('//dns:Project/dns:PropertyGroup[dns:IntermediateOutputPath]', $ns)
					$BaseIntermediateOutputPathNodes = $ProjectXml.SelectNodes('//dns:Project/dns:PropertyGroup[dns:BaseIntermediateOutputPath]', $ns)
				
					# Container for the IntermediateOutputPath relative to the proj directory
					$IntermediateOutputPaths = @()
				
					# IntermediateOutputPath take precedence over BaseIntermediateOutputPath 
					# Remove all BaseIntermediateOutputPaths that matches with an IntermediateOutputPath Configuration
					# Register the IntermediateOutputPath
					# Remove Configuration from Configurations list
					Foreach ($IntermediateOutputPathNode in $IntermediateOutputPathNodes)
					{
						$Configuration = $IntermediateOutputPathNode.Condition
						$BaseIntermediateOutputPathNodes = $BaseIntermediateOutputPathNodes | ? {$_.GetAttribute('Condition') -ne $Configuration}	
						$IntermediateOutputPaths += $IntermediateOutputPathNode.IntermediateOutputPath
						$Configurations = $Configurations | ? {$_ -ne $Configuration}
					}
					
					# Register the remaining BaseIntermediateOutputPath
					Foreach ($BaseIntermediateOutputPathNode in $BaseIntermediateOutputPathNodes)
					{
						$Configuration = $IntermediateOutputPathNode.Condition
						$IntermediateOutputPaths += $BaseIntermediateOutputPathNode.BaseIntermediateOutputPath
						$Configurations = $Configurations | ? {$_ -ne $Configuration}
					}
					
					$IntermediateOutputPaths = $IntermediateOutputPaths | % {Join-Path (Dir $Project) $_} | Where {Test-Path "$_\*FileListAbsolute.txt"}
					if ($Configurations) {
						$IntermediateOutputPaths += (Get-ChildItem -Path (Join-Path (Dir $Project).DirectoryName obj) -Directory).FullName | Where {Test-Path "$_\*FileListAbsolute.txt"}
					}
								
					# If all IntermediateOutputPaths are empty alert user
					if (-not $IntermediateOutputPaths)
					{
						if ($Verbose) { Write-Host "Error :"(Dir $Project).Name": Could not find IntermediateOutputPath contents. Please rebuild the solution/project and try again." -ForegroundColor Red }
						$TestResults += New-Object PSObject -Property @{Outcome="projerror";Name=(Dir $Project).Name;Storage="";Comments="Could not find IntermediateOutputPath contents. Please rebuild the solution/project and try again."}
						break
					}
					
					# Get the latest FileListAbsolute this is cumbersome because if it is an array of one element the string will return the first char
					$FileListAbsolute = (($IntermediateOutputPaths | % {(Get-ChildItem -Path $_ -File *.FileListAbsolute.txt)} | Sort -Descending {(Dir $_.FullName).LastWriteTime})[0]).FullName
			
					# Find all test containers from the DLL files submitted
					$ProjectTestContainers = @()			

					# Get a list of all the DLL in the project and find the test containers
					Get-Content -Path $FileListAbsolute | % { if ($_ -match '.*\\bin\\.*.dll' -and $_ -notmatch '.*nunit.core.dll') {
						if (-not (Test-Path $_))
						{
							if ($Verbose) { Write-Host "Error :"(Dir $Project).Name": Could not find library $_. Please rebuild the solution/project and try again." -ForegroundColor Red }
							$TestResults += New-Object PSObject -Property @{Outcome="projerror";Name=(Dir $Project).Name;Storage="$_";Comments="Could not find library $_. Please rebuild the solution/project and try again."}
						}
						elseif ([IO.File]::ReadAllText($_) -match 'TestClass')
						{
							$ProjectTestContainers += $_
						}
					}}
					
					# If there are not test containers, let the user know
					if (-not $ProjectTestContainers)
					{
						if ($Verbose) { Write-Host "Warning :"(Dir $Project).Name": Could not find test containers." -ForegroundColor Yellow }
						$TestResults += New-Object PSObject -Property @{Outcome="projwarning";Name=(Dir $Project).Name;Storage="";Comments="Could not find test containers."}
					}
					else
					{
						if ($Verbose) { Write-Host "Added :"(Dir $Project).Name": Found" $ProjectTestContainers.Count "test container(s)" -ForegroundColor Green }
					}
					
					$TestContainers += $ProjectTestContainers
				}
			}
			if (-not $TestContainers)
			{
				# By returning the unique TestResults we prevent spamming the same problem (ie. Multiple assemblies cannot be found with only report once)
				if ($Verbose) { Write-Host "Error :"(Dir $PathIteration).Name": Could not find any test containers."  -ForegroundColor Red }
				$TestResults += New-Object PSObject -Property @{Outcome="testerror";Name=(Dir $PathIteration).Name;Storage="";Comments="Could not find any test containers."}
				if ($IsAllPassed) {(!$TestResults)}
				elseif (!$NoResults) {$TestResults}
				return
			}
			
			# Add Parameters to test arguments
			$TestArguments = "${MsTestParameters}"
			
			# Add test names the arguments and unique, if chosen
			if ($Tests)
			{
				$TestArguments = "${TestArguments} /test:"
				foreach ($Test in $Tests) {$TestArguments = "${TestArguments}${Test},"}
				$TestArguments = $($TestArguments.Substring(0,$TestArguments.Length-1))
				if ($Unique) { $TestArguments = "${TestArguments} /unique" }
			}
			
			foreach ($TestContainer in $TestContainers)
			{
				if ($Verbose) { Write-Host "Loading Test: " (Dir $TestContainer).BaseName -ForegroundColor Cyan }
				$TestArguments = "${TestArguments} /testcontainer:${TestContainer}" 
			}
			
			# Select our window style
			$WindowStyle = if ($ShowTestWindow -or $PromptToCloseTestWindow) { "Normal" } else { "Hidden" }
		
			# Time Stamp will be added to our test files
			$TimeStamp = (Get-Date -Format "yyyy-MM-dd hh_mm_ss")
			
			# Add the test results file to the directory
			$TestResultsFile = "InvokeMsTestResults ${TimeStamp}.trx"
			$TestResultsFile = Join-Path $ResultsDirectoryPath $TestResultsFile
			$TestArguments = "${TestArguments} /resultsfile:`"${TestResultsFile}`""
		
			# Construct the test test cmd argument
			$PauseForInput = if ($PromptToCloseTestWindow) { "Pause & " } else { "" }
			$TestCmdArgument = "/k "" ""${MsTest}"" ${TestArguments} & ${PauseForInput} Exit"" "
		
			# Starts the MsTests
			Start-Process cmd.exe -ArgumentList $TestCmdArgument -WindowStyle $windowStyle -Wait
			
			if (Test-Path $TestResultsFile)
			{
				$TestResultsFileXml = [Xml](Get-Content -Path $TestResultsFile)
				$ns = New-Object Xml.XmlNamespaceManager $TestResultsFileXml.NameTable
				$ns.AddNamespace('dns','http://microsoft.com/schemas/VisualStudio/TeamTest/2010')
				
				$UnitTests = $TestResultsFileXml.SelectNodes('//dns:TestRun/dns:TestDefinitions/dns:UnitTest',$ns)
				$UnitTestResults = $TestResultsFileXml.SelectNodes('//dns:TestRun/dns:Results/dns:UnitTestResult',$ns)	

				if ($Verbose) { Write-Host "--------------------------------------------------------------------------------`r`nResults`r`n--------------------------------------------------------------------------------" }	

				Foreach ($UnitTestResult in $UnitTestResults)
				{	
					$Storage = ($UnitTests | Where {$_.id -eq $UnitTestResult.testId}).storage
					$TestResults += New-Object PSObject -Property @{Outcome=$UnitTestResult.outcome;Name=$UnitTestResult.testName;Storage=$Storage;Comments=""}
					if ($Verbose)
					{
						if ($UnitTestResult.outcome -eq "passed") { $Color = 'Green' }
						elseif ($UnitTestResult.outcome -eq "warning") { $Color = 'Yellow' }
						elseif ($UnitTestResult.outcome -eq "failed") { $Color = 'Red' }
						else { $Color = 'Gray' }
						
						Write-Host $UnitTestResult.outcome"`t"$UnitTestResult.testName"`t"$Storage -ForegroundColor $Color
						
					}
				}
			}
			else
			{
				if ($Verbose) { Write-Host "Error: Could not find or open test results file." }
				$TestResults += New-Object PSObject -Property @{Outcome="testerror";Name=(Dir $PathIteration).Name;Storage="";Comments="Could not find or open test results file."}
			}
			if ($IsAllPassed) {(($TestResults.outcome | Where {$_ -eq "Passed"}).Count -eq $TestResults.Count)}
			elseif (!$NoResults) {$TestResults}
		}
	}
}
################################################################################
function Get-MsTest
{
	<#
	.SYNOPSIS
		Gets path for latest version of MsTest.exe
	
	.DESCRIPTION
		Gets path for latest version of MsTest.exe
	#>
	
	
	$MsTest = Join-Path (Get-VsCommonTools) '..\IDE\MsTest.exe'
	if (Test-Path $MsTest) {$MsTest}
	else {Write-Error "Unable to find MsTest.exe"}
}
################################################################################
function Get-VsCommonTools
{
	<#
	.SYNOPSIS
		Gets path to the current VS common tools
	
	.DESCRIPTION
		Gets path to the current VS common tools. 
		Current list supports VS14, VS12, VS11 and VS10, you may need to add to this list
		to satisfy your needs.
	#>
    
    # We have to use the vswhere.exe tool to locate Visual Studio 2017
    $vsWhere = Join-Path $PSScriptRoot '..\..\Tools\vswhere.exe'
    $vs141Instance = & $vsWhere -nologo -format value -property installationPath -latest -requires 'Microsoft.VisualStudio.Component.VC.CLI.Support'
    $VS141COMNTOOLS = Join-Path $vs141Instance 'Common7\Tools'	
    
    # Now test which VS versions are present on the system
	$VsCommonToolsPath = @($VS141COMNTOOLS,$env:VS140COMNTOOLS,$env:VS120COMNTOOLS,$env:VS110COMNTOOLS,$env:VS100COMNTOOLS) |
        Where-Object { $_ -ne $null -and (Test-Path -Path $_ -PathType Container) } |
        Select-Object -First 1

    if ($VsCommonToolsPath -eq $null)
    {
        Write-Error "Unable to find Visual Studio Common Tool Path."
    }

    return $VsCommonToolsPath
}
################################################################################
Export-ModuleMember -Function Invoke-MsTest,Get-MsTest