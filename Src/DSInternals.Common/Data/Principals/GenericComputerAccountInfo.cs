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
            //data_len += this.LoadBitlockerRecoveryInfo(dsObject);
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

        /*
        public string TPMOwnerInfo
        {
            get;
            private set;
        }

        public byte[] FVEKeyPackage
        {
            get;
            private set;
        }

        public byte[] FVEVolumeGuid
        {
            get;
            private set;
        }

        public byte[] FVERecoveryGuid
        {
            get;
            private set;
        }

        public string FVERecoveryPassword
        {
            get;
            private set;
        }
        */

        protected ulong LoadGenericComputerAccountInfo(DirectoryObject dsObject)
        {
            ulong ret = 0;

            // dNSHostName:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.DNSHostName))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.DNSHostName, out string dnshostname);
                if (!String.IsNullOrEmpty(dnshostname))
                    ret += (ulong)dnshostname.Length;
                this.DNSHostName = dnshostname;
            }

            // managedBy:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.ManagedBy))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.ManagedBy, out byte[] binaryManagedBy);
                string managedBy = dsObject.ParseDSDN(binaryManagedBy);
                if (!String.IsNullOrEmpty(managedBy))
                    ret += (ulong)managedBy.Length;
                this.ManagedBy = managedBy;
            }

            // canonicalName:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.CanonicalName))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.CanonicalName, out string canonicalName);
                if (!String.IsNullOrEmpty(canonicalName))
                    ret += (ulong)canonicalName.Length;
                this.CanonicalName = canonicalName;
            }

            // location:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Location))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Location, out string location);
                if (!String.IsNullOrEmpty(location))
                    ret += (ulong)location.Length;
                this.Location = location;
            }

            // operatingSystem:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.OperatingSystem))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystem, out string operatingSystem);
                if (!String.IsNullOrEmpty(operatingSystem))
                    ret += (ulong)operatingSystem.Length;
                this.OperatingSystem = operatingSystem;
            }

            // operatingSystemVersion:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.OperatingSystemVersion))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemVersion, out string operatingSystemVersion);
                if (!String.IsNullOrEmpty(operatingSystemVersion))
                    ret += (ulong)operatingSystemVersion.Length;
                this.OperatingSystemVersion = operatingSystemVersion;
            }

            // operatingSystemHotfix:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.OperatingSystemHotfix))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemHotfix, out string operatingSystemHotfix);
                if (!String.IsNullOrEmpty(operatingSystemHotfix))
                    ret += (ulong)operatingSystemHotfix.Length;
                this.OperatingSystemHotfix = operatingSystemHotfix;
            }

            // operatingSystemServicePack:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.OperatingSystemServicePack))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemServicePack, out string operatingSystemServicePack);
                if (!String.IsNullOrEmpty(operatingSystemServicePack))
                    ret += (ulong)operatingSystemServicePack.Length;
                this.OperatingSystemServicePack = operatingSystemServicePack;
            }

            // webPage:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.WebPage))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.WebPage, out string webPage);
                if (!String.IsNullOrEmpty(webPage))
                    ret += (ulong)webPage.Length;
                this.WebPage = webPage;
            }

            // company:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Company))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Company, out string company);
                if (!String.IsNullOrEmpty(company))
                    ret += (ulong)company.Length;
                this.Company = company;
            }

            return ret;
        }

        /*
        protected ulong LoadBitlockerRecoveryInfo(DirectoryObject dsObject)
        {
            ulong ret = 0;

            // TPMOwnerInfo:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.TPMOwnerInfo))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.TPMOwnerInfo, out string tpmOwnerInfo);
                if (!String.IsNullOrEmpty(tpmOwnerInfo))
                    ret += (ulong)tpmOwnerInfo.Length;
                this.TPMOwnerInfo = tpmOwnerInfo;
            }

            // FVEKeyPackage:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.FVEKeyPackage))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.FVEKeyPackage, out byte[] fveKeyPackage);
                if (fveKeyPackage != null)
                    ret += (ulong)fveKeyPackage.Length;
                this.FVEKeyPackage = fveKeyPackage;
            }

            // FVEVolumeGuid:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.FVEVolumeGuid))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.FVEVolumeGuid, out byte[] fveVolumeGuid);
                if (fveVolumeGuid != null)
                    ret += (ulong)fveVolumeGuid.Length;
                this.FVEVolumeGuid = fveVolumeGuid;
            }

            // FVERecoveryGuid:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.FVERecoveryGuid))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryGuid, out byte[] fveRecoveryGuid);
                if (fveRecoveryGuid != null)
                    ret += (ulong)fveRecoveryGuid.Length;
                this.FVERecoveryGuid = fveRecoveryGuid;
            }

            // FVERecoveryPassword:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.FVERecoveryPassword))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryPassword, out string fveRecoveryPassword);
                Console.WriteLine("FVE: {0}", fveRecoveryPassword);
                if (!String.IsNullOrEmpty(fveRecoveryPassword))
                    ret += (ulong)fveRecoveryPassword.Length;
                this.FVERecoveryPassword = fveRecoveryPassword;
            }

            return ret;
        }
        */
    }
}
