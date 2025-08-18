using System.Management.Automation;
using System.Reflection;

namespace DSInternals.PowerShell
{
    /// <summary>
    /// ModuleAssemblyResolver is a class that implements the IModuleAssemblyInitializer and IModuleAssemblyCleanup interfaces.
    /// This class is used to handle the assembly resolve event when the module is imported and removed.
    /// </summary>
    public class ModuleAssemblyResolver : IModuleAssemblyInitializer, IModuleAssemblyCleanup
    {
        /// <summary>
        /// OnImport is called when the module is imported.
        /// </summary>
        public void OnImport()
        {
#if NETFRAMEWORK
            AppDomain.CurrentDomain.AssemblyResolve += MyResolveEventHandler;
#endif
        }

        /// <summary>
        /// Called when the module is removed from the PowerShell session.
        /// </summary>
        public void OnRemove(PSModuleInfo psModuleInfo)
        {
#if NETFRAMEWORK
            AppDomain.CurrentDomain.AssemblyResolve -= MyResolveEventHandler;
#endif
        }

        /// <summary>
        /// Handles assembly resolution for dependencies shipped with the module.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Information about the assembly to resolve.</param>
        /// <returns>The loaded assembly or <c>null</c> if not found.</returns>
        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            string dependencyDirectory = Path.GetDirectoryName(typeof(ModuleAssemblyResolver).Assembly.Location);
            string requestedAssemblyName = new AssemblyName(args.Name).Name + ".dll";
            string assemblyPath = Path.Combine(dependencyDirectory, requestedAssemblyName);

            if (File.Exists(assemblyPath))
            {
                // Load the assembly from the module's dependency directory.
                return Assembly.LoadFrom(assemblyPath);
            }

            // For other assemblies, return null to allow other resolutions to continue.
            return null;
        }
    }
}
