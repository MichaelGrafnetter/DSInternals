DSInternals PowerShell Module
=============================

Strong Name Key Files
---------------------

The `DSInternals.Public.snk` file contains the public key that can be used to verify
the strong name signature of the official binaries.

The corresponding private key, contained in the D`SInternals.Private.snk` file,
is not publicly available. If you want to strong sign the resulting assemblies,
you have to generate your own key pair.

Signing Policy
--------------

Signing policy is controlled by the `Directory.Build.props` file for all projects. The default behavior is as follows:

- The strong name key signing is only performed during a Release build.
- If the private key is not present, not even delayed signing is performed.

Generating a Custom Key Pair
----------------------------

A custom key pair can be generated using the `sn.exe` tool, which is part of Windows SDK.
