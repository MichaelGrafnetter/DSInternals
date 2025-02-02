using System;
using System.Collections.Generic;
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

            if (propertySets.HasFlag(AccountPropertySets.GenericInformation))
            {
                this.LoadGenericComputerAccountInfo(dsObject);
            }

            if (propertySets.HasFlag(AccountPropertySets.LegacyLAPS))
            {
                this.LoadLegacyLAPS(dsObject);
            }

            if (propertySets.HasFlag(AccountPropertySets.WindowsLAPS))
            {
                this.LoadWindowsLAPS(dsObject);
            }
        }

        public IList<LapsPasswordInformation> LapsPasswords
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

        protected void LoadLegacyLAPS(DirectoryObject dsObject)
        {
            LapsPasswordInformation legacyLapsPassword = null;

            dsObject.ReadAttribute(CommonDirectoryAttributes.LAPSPasswordExpirationTime, out DateTime? legacyExpirationTime, false);

            if (legacyExpirationTime != null)
            {
                // Optimization: Do not try to read the password if no expiration time is set.
                dsObject.ReadAttribute(CommonDirectoryAttributes.LAPSPassword, out byte[] admPwdBinary);

                if(admPwdBinary != null && admPwdBinary.Length > 0)
                {
                    string password = Encoding.UTF8.GetString(admPwdBinary);
                    legacyLapsPassword = new LapsPasswordInformation(this.SamAccountName, password, legacyExpirationTime);
                }
            }

            if (legacyLapsPassword != null)
            {
                if(this.LapsPasswords == null)
                {
                    this.LapsPasswords = new List<LapsPasswordInformation>();
                }

                this.LapsPasswords.Add(legacyLapsPassword);
            }
        }

        protected void LoadWindowsLAPS(DirectoryObject dsObject)
        {
            LapsPasswordInformation lapsPassword = null;

            dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsPasswordExpirationTime, out DateTime? expirationTime, false);

            if (expirationTime != null)
            {
                // Read msLAPS-Password
                dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsPassword, out byte[] binaryLapsJson);

                if (binaryLapsJson != null && binaryLapsJson.Length > 0)
                {
                    LapsClearTextPassword passwordInfo = LapsClearTextPassword.Parse(binaryLapsJson);
                    lapsPassword = new LapsPasswordInformation(this.SamAccountName, passwordInfo, expirationTime);
                }

                // Read msLAPS-EncryptedPassword
                // TODO: dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsEncryptedPassword, out byte[] encryptedPassword);

                // Read msLAPS-EncryptedPasswordHistory
                // TODO: dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsEncryptedPasswordHistory, out byte[][] encryptedPasswordHistory);

                // Read msLAPS-EncryptedDSRMPassword
                // TODO: dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPassword, out byte[] encryptedDsrmPassword);

                // Read msLAPS-EncryptedDSRMPasswordHistory
                // TODO: dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPasswordHistory, out byte[][] encryptedDsrmPasswordHistory);
            }

            if (lapsPassword != null)
            {
                if (this.LapsPasswords == null)
                {
                    this.LapsPasswords = new List<LapsPasswordInformation>();
                }

                this.LapsPasswords.Add(lapsPassword);
            }
        }
    }
}
