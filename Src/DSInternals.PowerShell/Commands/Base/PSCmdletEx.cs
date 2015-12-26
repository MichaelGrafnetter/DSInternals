namespace DSInternals.PowerShell.Commands
{
    using Microsoft.PowerShell.Commands;
    using System.Management.Automation;

    public abstract class PSCmdletEx : PSCmdlet
    {
        protected string ResolveSinglePath(string path)
        {
            if (path == null)
            {
                return null;
            }
            ProviderInfo provider;
            // This throws ItemNotFoundException if the path is not found
            var resolvedPath = this.GetResolvedProviderPathFromPSPath(path, out provider);
            if (provider.ImplementingType != typeof(FileSystemProvider))
            {
                // TODO: Extract as resource
                throw new ItemNotFoundException("The path provided does not point to the file system.");
            }
            if (resolvedPath.Count != 1)
            {
                // TODO: Extract as resource
                throw new ItemNotFoundException("Could not resolve the path provided to a single file or directory.");
            }
            return resolvedPath[0];
        }
    }
}
