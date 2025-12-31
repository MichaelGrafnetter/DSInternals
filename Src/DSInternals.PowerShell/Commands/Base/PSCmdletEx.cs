using System.Management.Automation;
using Microsoft.PowerShell.Commands;

namespace DSInternals.PowerShell.Commands;
public abstract class PSCmdletEx : PSCmdlet
{
    protected const string InvalidParameterSetMessage = "Invalid parameter set.";
    protected const string WarningMessage = "This command performs very advanced and unsupported operations that may cause irreversible damage to your domain controller. Never use it on production databases. To suppress this warning, reissue the command specifying the Force parameter.";

    protected string ResolveDirectoryPath(string path)
    {
        if (path == null)
        {
            // This is probably a value passed from an optional parameter.
            return null;
        }

        string resolvedPath = this.ResolveSinglePath(path);
        bool isDirectory = File.GetAttributes(resolvedPath).HasFlag(FileAttributes.Directory);

        if (!isDirectory)
        {
            throw new DirectoryNotFoundException("The path provided does not point to a directory.");
        }

        return resolvedPath;
    }

    protected string ResolveFilePath(string path)
    {
        if (path == null)
        {
            // This is probably a value passed from an optional parameter.
            return null;
        }

        string resolvedPath = this.ResolveSinglePath(path);
        bool isDirectory = File.GetAttributes(resolvedPath).HasFlag(FileAttributes.Directory);

        if (isDirectory)
        {
            throw new FileNotFoundException("The path provided does not point to a file.");
        }

        return resolvedPath;
    }

    private string ResolveSinglePath(string path)
    {
        ProviderInfo provider;
        // This throws ItemNotFoundException if the path is not found
        var resolvedPaths = this.GetResolvedProviderPathFromPSPath(path, out provider);

        if (provider.ImplementingType != typeof(FileSystemProvider))
        {
            throw new ItemNotFoundException("The path provided does not point to a file system.");
        }

        if (resolvedPaths.Count != 1)
        {
            throw new ItemNotFoundException("Could not resolve the path provided to a single file or directory.");
        }

        return resolvedPaths[0];
    }
}
