using System;
using System.Text;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Properties;

namespace DSInternals.Common.Data
{
    public class DSComputer : DSAccount
    {
        public DSComputer(DirectoryObject dsObject, string netBIOSDomainName, DirectorySecretDecryptor pek, AccountPropertySets propertySets = AccountPropertySets.Default) : base(dsObject, netBIOSDomainName, pek, propertySets)
        {
            if (this.SamAccountType != SamAccountType.Computer)
            {
                throw new ArgumentException(Resources.ObjectNotAccountMessage);
            }

            if(propertySets.HasFlag(AccountPropertySets.GenericInformation))
            {
                this.LoadGenericComputerAccountInfo(dsObject);
            }

            if(propertySets.HasFlag(AccountPropertySets.LAPS))
            {
                this.LoadLAPS(dsObject);
            }
        }

        public string AdminPassword
        {
            get;
            private set;
        }

        public DateTime? AdminPasswordExpirationTime
        {
            get;
            private set;
        }

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

        protected void LoadGenericComputerAccountInfo(DirectoryObject dsObject)
        {
            // dNSHostName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.DNSHostName, out string dnshostname);
            this.DNSHostName = dnshostname;

            // managedBy:
            dsObject.ReadAttribute(CommonDirectoryAttributes.ManagedBy, out DistinguishedName managedBy);
            this.ManagedBy = managedBy?.ToString();

            // location:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Location, out string location);
            this.Location = location;

            // operatingSystem:
            dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemName, out string operatingSystem);
            this.OperatingSystem = operatingSystem;

            // operatingSystemVersion:
            dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemVersion, out string operatingSystemVersion);
            this.OperatingSystemVersion = operatingSystemVersion;

            // operatingSystemHotfix:
            dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemHotfix, out string operatingSystemHotfix);
            this.OperatingSystemHotfix = operatingSystemHotfix;

            // operatingSystemServicePack:
            dsObject.ReadAttribute(CommonDirectoryAttributes.OperatingSystemServicePack, out string operatingSystemServicePack);
            this.OperatingSystemServicePack = operatingSystemServicePack;
        }

        protected void LoadLAPS(DirectoryObject dsObject)
        {
            dsObject.ReadAttribute(CommonDirectoryAttributes.LAPSPasswordExpirationTime, out DateTime? expirationTime, false);
            this.AdminPasswordExpirationTime = expirationTime;

            if(expirationTime != null)
            {
                // Optimization: Do not try to read the password if no expiration time is set.
                dsObject.ReadAttribute(CommonDirectoryAttributes.LAPSPassword, out byte[] admPwdBinary);
                this.AdminPassword = Encoding.UTF8.GetString(admPwdBinary);
            }
        }
    }
}
