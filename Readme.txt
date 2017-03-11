---------------------------------
| DSInternals PowerShell Module |
---------------------------------

The DSInternals PowerShell Module exposes several internal and undocumented features of Active Directory.

List of Cmdlets
---------------

To see the list of available cmdlets with their description, run this command:

PS > Get-Help about_DSInternals

System Requirements
-------------------

List of supported systems is available here:

https://github.com/MichaelGrafnetter/DSInternals/wiki/Installation

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

Source Codes
------------

https://github.com/MichaelGrafnetter/DSInternals