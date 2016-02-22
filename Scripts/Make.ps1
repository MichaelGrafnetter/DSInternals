<#
.SYNOPSIS
Builds the solution from scratch and ZIPs the resulting module.
#>

.\Restore-ReferencedPackages
.\Build-Solution
.\Run-Tests
.\Pack-Release