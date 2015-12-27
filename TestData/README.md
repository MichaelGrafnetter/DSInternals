DSInternals PowerShell Module
=============================

Sample Database Files
---------------------

These files contain sample Active Directory Databases (ntds.dit files), that can be used to test the module functionality.

- Big - Large (1GB) database, Windows Server 2012 R2
- IFM - Install From Media Backup, Windows Server 2012 R2
- Initial - Initial databases from the system32 directory, Windows Server 2012 R2
- DCPromo - Clean database created by dcpromo, Windows Server 2012 R2
- Production - Database copied from live system,  Windows Server 2012 R2

All the database files are zipped, because JET database files contain many empty blocks, which results into quite high compression ratios.

TODO
-----

Additional test data are needed, including DBs taken from GCs of child domains and RODCs.
DBs from different Windows Server versions are welcome, too.
Just be advised not to publish any production databases.