# TODO: Convert this into a Pester test
Get-ChildItem ..\Build\bin\Release\DSInternals -Recurse -Filter *.dll | foreach {
    try
    {
        $assemblyName = [System.Reflection.AssemblyName]::GetAssemblyName($PSItem.FullName)
        $isSigned = $assemblyName.Flags.HasFlag([System.Reflection.AssemblyNameFlags]::PublicKey)
        if($isSigned)
        {
            Write-Host ('Found assembly with strong name: {0}' -f $assemblyName.FullName)
        }
        else
        {
            throw "The assembly $PSItem does not have a strong name."
        }
    }
    catch [System.BadImageFormatException]
    {
        # The DLL file is not a .NET assembly. We can ignore this error, because it is probably the Visual C++ Runtime. 
    }
}