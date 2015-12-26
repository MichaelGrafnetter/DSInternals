namespace DSInternals.Common.Data
{
    using DSInternals.Common;
    using System;
    using System.DirectoryServices.ActiveDirectory;
    using System.Reflection;

    public struct DistinguishedNameComponent
    {
        public string Name;
        public string Value;

        public DistinguishedNameComponent(string name, string value)
        {
            Validator.AssertNotNullOrWhiteSpace(name, "name");
            Validator.AssertNotNullOrWhiteSpace(value, "value");
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            string rdn = String.Format("{0}={1}", this.Name, this.Value);
            return EscapeRDN(rdn);
        }

        private static string EscapeRDN(string input)
        {
            // HACK: Internal member System.DirectoryServices.ActiveDirectory.Utils.GetEscapedPath is used.
            var directoryServicesAssembly = Assembly.Load(@"System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            var utilsClass = directoryServicesAssembly.GetType("System.DirectoryServices.ActiveDirectory.Utils");
            var getEscapedPathMethod = utilsClass.GetMethod("GetEscapedPath", BindingFlags.Static | BindingFlags.NonPublic);
            try
            {
                return (string)getEscapedPathMethod.Invoke(null, new object[] { input });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
