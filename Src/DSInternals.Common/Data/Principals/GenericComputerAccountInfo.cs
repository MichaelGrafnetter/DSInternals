namespace DSInternals.Common.Data
{
    using DSInternals.Common.Properties;
    using System;

    public class GenericComputerAccountInfo
    {
        public GenericComputerAccountInfo(DirectoryObject dsObject)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, nameof(dsObject));

            if (!dsObject.IsComputerAccount)
            {
                throw new ArgumentException(Resources.ObjectNotAccountMessage);
            }

            data_len = this.LoadGenericComputerAccountInfo(dsObject);
        }

        public ulong data_len = 0;

        public string DNSHostName
        {
            get;
            private set;
        }

        public string ManagedBy
        {
            get;
            private set;
        }

        public string CanonicalName
        {
            get;
            private set;
        }

        public string Location
        {
            get;
            private set;
        }

        public string OperatingSystem
        {
            get;
            private set;
        }

        public string OperatingSystemVersion
        {
            get;
            private set;
        }

        public string OperatingSystemHotfix
        {
            get;
            private set;
        }

        public string OperatingSystemServicePack
        {
            get;
            private set;
        }

        public string WebPage
        {
            get;
            private set;
        }

        public string Company
        {
            get;
            private set;
        }

        protected ulong LoadGenericComputerAccountInfo(DirectoryObject dsObject)
        {
            ulong ret = 0;

            // dNSHostName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.DNSHostName, out string dnshostname);
            if (!String.IsNullOrEmpty(dnshostname))
                ret += (ulong)dnshostname.Length;
            this.DNSHostName = dnshostname;

            // managedBy:
            dsObject.ReadAttribute(CommonDirectoryAttributes.ManagedBy, out byte[] binaryManagedBy);
            string managedBy = dsObject.ParseDSDN(binaryManagedBy);
            if (!String.IsNullOrEmpty(managedBy))
                ret += (ulong)managedBy.Length;
            this.ManagedBy = managedBy;

            dsObject.ReadAttribute(CommonDirectoryAttributes.CanonicalName, out string canonicalName);
            if (!String.IsNullOrEmpty(canonicalName))
                ret += (ulong)canonicalName.Length;
            this.CanonicalName = canonicalName;

            dsObject.ReadAttribute(CommonDirectoryAttributes.Location, out string location);
            if (!String.IsNullOrEmpty(location))
                ret += (ulong)location.Length;
            this.Location = location;

            dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystem, out string operatingSystem);
            if (!String.IsNullOrEmpty(operatingSystem))
                ret += (ulong)operatingSystem.Length;
            this.OperatingSystem = operatingSystem;

            dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemVersion, out string operatingSystemVersion);
            if (!String.IsNullOrEmpty(operatingSystemVersion))
                ret += (ulong)operatingSystemVersion.Length;
            this.OperatingSystemVersion = operatingSystemVersion;

            dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemHotfix, out string operatingSystemHotfix);
            if (!String.IsNullOrEmpty(operatingSystemHotfix))
                ret += (ulong)operatingSystemHotfix.Length;
            this.OperatingSystemHotfix = operatingSystemHotfix;

            dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemServicePack, out string operatingSystemServicePack);
            if (!String.IsNullOrEmpty(operatingSystemServicePack))
                ret += (ulong)operatingSystemServicePack.Length;
            this.OperatingSystemServicePack = operatingSystemServicePack;

            // WebPage:
            dsObject.ReadAttribute(CommonDirectoryAttributes.WebPage, out string webPage);
            if (!String.IsNullOrEmpty(webPage))
                ret += (ulong)webPage.Length;
            this.WebPage = webPage;

            // Company:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Company, out string company);
            if (!String.IsNullOrEmpty(company))
                ret += (ulong)company.Length;
            this.Company = company;

            return ret;
        }
    }
}
