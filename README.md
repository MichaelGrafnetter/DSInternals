DSInternals PowerShell Module and Framework
===========================================

![MIT License](https://img.shields.io/badge/License-MIT-green.svg) ![PowerShell 3 | 4 | 5](https://img.shields.io/badge/PowerShell-3%20|%204%20|%205-0000FF.svg) ![Windows Server 2008 R2 | 2012 R2 | 2016 | 2019](https://img.shields.io/badge/Windows%20Server-2008%20R2%20|%202012%20R2%20|%202016-007bb8.svg) ![.NET Framework 4.5.1+](https://img.shields.io/badge/.NET%20Framework-4.5.1%2B-007FFF.svg) ![Visual Studio 2015 | 2017](https://img.shields.io/badge/Visual%20Studio-2013%20|%202015%20|%202017-383278.svg)

> DISCLAIMER: Features exposed through these tools are not supported by Microsoft and are therefore not intended to be used in production environments. Improper use might cause irreversible damage to domain controllers or negatively impact domain security.

The DSInternals project consists of these two parts:
- The [DSInternals Framework](https://www.nuget.org/profiles/MichaelGrafnetter) exposes several internal features of Active Directory and can be used from any .NET application. The codebase has already been integrated into several 3rd party commercial products that use it in scenarios like Active Directory disaster recovery, identity management, cross-forest migrations and password strength auditing.
- The [DSInternals PowerShell Module](https://www.powershellgallery.com/packages/DSInternals/) provides easy-to-use cmdlets that are built on top of the Framework. The main features include offline [ntds.dit file](https://technet.microsoft.com/en-us/library/cc961761.aspx) manipulation and querying domain controllers through the [Directory Replication Service (DRS) Remote Protocol](https://msdn.microsoft.com/en-us/library/cc228086.aspx).

Quick Start Guide
-----------------
- [List of Cmdlets with Usage Examples](https://www.dsinternals.com/en/list-of-cmdlets-in-the-dsinternals-module/)
- [Wiki](https://github.com/MichaelGrafnetter/DSInternals/wiki)

Continuous Integration
----------------------

| Compilation  |  Unit Tests |
--- | ---
![Build Status](https://grafnetter.visualstudio.com/DefaultCollection/_apis/public/build/definitions/419499fa-9402-4b5b-96ad-1d9d235c1b8f/6/badge) | ![Build Status](https://grafnetter.visualstudio.com/DefaultCollection/_apis/public/build/definitions/419499fa-9402-4b5b-96ad-1d9d235c1b8f/7/badge)

Automatic builds are provided by [Visual Studio Team Services](https://www.visualstudio.com/en-us/products/visual-studio-team-services-vs.aspx).

Author
------

**Michael Grafnetter**
- [Blog](https://www.dsinternals.com/en)
- [LinkedIn](https://www.linkedin.com/in/grafnetter)
- [Twitter](https://twitter.com/mgrafnetter)
