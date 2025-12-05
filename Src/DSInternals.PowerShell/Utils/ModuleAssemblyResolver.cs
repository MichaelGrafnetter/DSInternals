using System.Management.Automation;
using System.Reflection;

namespace DSInternals.PowerShell
{
    /// <summary>
    /// ModuleAssemblyResolver is a class that implements the IModuleAssemblyInitializer and IModuleAssemblyCleanup interfaces.
    /// This class is used to handle the assembly resolve event when the module is imported and removed.
    /// This is a workaround for .NET assembly binding redirects not being supported in PowerShell modules.
    /// </summary>
    public class ModuleAssemblyResolver : IModuleAssemblyInitializer, IModuleAssemblyCleanup
    {
        private static readonly string[] BundledAssemblies =
        [
            "Microsoft.Bcl.AsyncInterfaces",
            "Microsoft.Bcl.HashCode",
            "System.Buffers",
            "System.Formats.Asn1",
            "System.Formats.Cbor",
            "System.IO.Pipelines",
            "System.Memory",
            "System.Numerics.Vectors",
            "System.Runtime.CompilerServices.Unsafe",
            "System.Text.Encodings.Web",
            "System.Text.Json",
            "System.Threading.Tasks.Extensions"
        ];

        /// <summary>
        /// OnImport is called when the module is imported.
        /// </summary>
        public void OnImport()
        {
#if NETFRAMEWORK
            AppDomain.CurrentDomain.AssemblyResolve += LoadAssemblyFromModuleDirectory;
#endif
        }

        /// <summary>
        /// Called when the module is removed from the PowerShell session.
        /// </summary>
        public void OnRemove(PSModuleInfo psModuleInfo)
        {
#if NETFRAMEWORK
            AppDomain.CurrentDomain.AssemblyResolve -= LoadAssemblyFromModuleDirectory;
#endif
        }

        /// <summary>
        /// Handles assembly resolution for dependencies shipped with the module.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Information about the assembly to resolve.</param>
        /// <returns>The loaded assembly or <c>null</c> if not found.</returns>
        private static Assembly LoadAssemblyFromModuleDirectory(object sender, ResolveEventArgs args)
        {
            AssemblyName requestedAssemblyName = new(args.Name);

            if (!BundledAssemblies.Contains(requestedAssemblyName.Name))
            {
                // Do not try to resolve assemblies not bundled with this module.
                return null;
            }

            string requestedAssemblyFileName = requestedAssemblyName.Name + ".dll";
            string dependencyDirectory = Path.GetDirectoryName(typeof(ModuleAssemblyResolver).Assembly.Location);
            string assemblyPath = Path.Combine(dependencyDirectory, requestedAssemblyFileName);

            if (File.Exists(assemblyPath))
            {
                // Load the assembly from the module's dependency directory.
                return Assembly.LoadFrom(assemblyPath);
            }
            else
            {
                // For other assemblies, return null to allow other resolutions to continue.
                return null;
            }
        }
    }
}
