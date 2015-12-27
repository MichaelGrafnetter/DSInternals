---------------------------------
| DSInternals PowerShell Module |
---------------------------------

The DSInternals PowerShell Module exposes several internal and undocumented features of Active Directory.

List of Cmdlets
---------------

Offline AD Database Access:

Get-ADDBAccount
Get-ADDBDomainController
Get-BootKey
Get-ADDBBackupKey
Get-ADDBSchemaAttribute
Add-ADDBSidHistory
Set-ADDBPrimaryGroup
Set-ADDBDomainController
Set-ADDBBootKey
Remove-ADDBObject

Online AD Database Access:

Get-ADReplAccount
Get-ADReplBackupKey
Set-SamAccountPasswordHash

Password Hash Calculation:

ConvertTo-NTHash
ConvertTo-LMHash
ConvertTo-OrgIdHash

Password Decryption:

ConvertFrom-ADManagedPasswordBlob
ConvertFrom-UnicodePassword
ConvertTo-UnicodePassword
ConvertFrom-GPPrefPassword
ConvertTo-GPPrefPassword

Misc:

ConvertTo-Hex
Save-DPAPIBlob


System Requirements
-------------------

- Windows PowerShell 3+ 64-bit
- .NET Framework 4.5+

The cmdlets have been tested on these operating systems:

Windows Server 2012 R2
Windows Server 2008 R2
Windows 10 64-bit
Windows 8.1 64-bit
Windows 7 64-bit

The cmdlets working with the AD database do not support Windows 2000 domain functional level and require 2008+ schema.

Installation
------------

Option 1:

In PowerShell 5, you can install the DSInternals module from PowerShell Gallery by running this command:

Install-Module DSInternals

Option 2a:

Extract the ZIP file and copy the DSInternals directory to your PowerShell modules directory, e.g.
C:\Windows\system32\WindowsPowerShell\v1.0\Modules\DSInternals or
C:\Users\John\Documents\WindowsPowerShell\Modules\DSInternals

Option 2b:

Extract the ZIP file to any location import the DSInternals module using the Import-Module cmdlet, e.g.

cd C:\Users\John\Downloads\DSInternals
Import-Module .\DSInternals

Note:

Before extracting any files from the archive, do not forget to "unblock" the ZIP file in the Properties dialog.
If you fail to do so, all the extracted DLLs will inherit this attribute and PowerShell will refuse to load them.

Author
------

Michael Grafnetter

Homepage
--------

https://www.dsinternals.com