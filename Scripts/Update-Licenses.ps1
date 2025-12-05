<#
.SYNOPSIS
Updates the License.txt file that is part of binary module packages.

#>

$products = @(
    @{  Name = 'DSInternals PowerShell Module and Framework';
        LicenseUrl = 'https://raw.githubusercontent.com/MichaelGrafnetter/DSInternals/master/LICENSE.md'
     }, @{
        Name = 'ESENT Managed Interop';
        LicenseUrl = 'https://raw.githubusercontent.com/microsoft/ManagedEsent/master/LICENSE.md'
     }, @{
        Name = 'PBKDF2.NET';
        LicenseUrl = 'https://raw.githubusercontent.com/therealmagicmike/PBKDF2.NET/master/License.txt'
     }, @{
        Name = 'Bouncy Castle';
        LicenseUrl = 'https://raw.githubusercontent.com/bcgit/bc-csharp/master/crypto/License.html'
     }
)

$now = Get-Date
$licenses = New-Object -TypeName System.Text.StringBuilder

$licenses.Append('The binary distribution of the DSInternals PowerShell Module contains the following software products:') > $null

foreach($product in $products)
{
    $licenses.AppendLine() > $null
    $licenses.AppendLine() > $null

    # Product name
    $licenses.AppendLine('-' * $product.Name.Length) > $null
    $licenses.AppendLine($product.Name) > $null
    $licenses.AppendLine('-' * $product.Name.Length) > $null
    $licenses.AppendLine() > $null

    # Date and URI
    $note = '(License updated on {0:d} from {1}.)' -f $now,$product.LicenseUrl
    $licenses.AppendLine($note) > $null
    $licenses.AppendLine() > $null

    # License Text
    $license = Invoke-WebRequest -Uri $product.LicenseUrl -UseBasicParsing
	if($product.LicenseUrl.EndsWith('.html'))
	{
		# Remove HTML tags
		$tagsRemoved = $license.Content -replace '</?[^<]+>',''
		$license = $tagsRemoved.Trim()
	}
    $licenses.Append($license) > $null
}

$root = Join-Path $PSScriptRoot ..\
$licenseFilePath = Join-Path $root Src\DSInternals.PowerShell\License.txt

$licenses.ToString() | Out-File -FilePath $licenseFilePath -Encoding ascii
