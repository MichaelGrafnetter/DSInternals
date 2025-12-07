using System.Text;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Schema;

namespace DSInternals.Common.Data
{
    public class DSComputer : DSAccount
    {
        private List<LapsPasswordInformation>? _lapsPasswords;

        public DSComputer(DirectoryObject dsObject, string netBIOSDomainName, DirectorySecretDecryptor pek, IKdsRootKeyResolver? rootKeyResolver = null, AccountPropertySets propertySets = AccountPropertySets.All) : base(dsObject, netBIOSDomainName, pek, propertySets)
        {
            if (this.SamAccountType != SamAccountType.Computer)
            {
                throw new ArgumentException("The object is not a computer.");
            }

            if (propertySets.HasFlag(AccountPropertySets.GenericComputerInfo))
            {
                this.LoadGenericComputerAccountInfo(dsObject);
            }

            if (propertySets.HasFlag(AccountPropertySets.LegacyLAPS))
            {
                this.LoadLegacyLAPS(dsObject);
            }

            if (propertySets.HasFlag(AccountPropertySets.WindowsLAPS))
            {
                this.LoadWindowsLAPS(dsObject, rootKeyResolver);
            }

            if (propertySets.HasFlag(AccountPropertySets.ManagedBy))
            {
                // This is a linked value, so it takes multiple seeks to load it.
                this.LoadManagedBy(dsObject);
            }
        }

        public IReadOnlyList<LapsPasswordInformation>? LapsPasswords
        {
            get => _lapsPasswords;
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

        public string ComputerName
        {
            get
            {
                // TODO: Also try to fetch DNSHostName and use it as the primary source of computer name.
                // Computer account names always end with $.
                return this.SamAccountName?.TrimEnd('$');
            }
        }

        protected void LoadGenericComputerAccountInfo(DirectoryObject dsObject)
        {
            // dNSHostName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.DnsHostName, out string dnshostname);
            this.DNSHostName = dnshostname;

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

        protected void LoadManagedBy(DirectoryObject dsObject)
        {
            dsObject.ReadAttribute(CommonDirectoryAttributes.ManagedBy, out DistinguishedName managedBy);
            this.ManagedBy = managedBy?.ToString();
        }

        protected void LoadLegacyLAPS(DirectoryObject dsObject)
        {
            LapsPasswordInformation? legacyLapsPassword = null;

            dsObject.ReadAttribute(CommonDirectoryAttributes.LAPSPasswordExpirationTime, out DateTime? legacyExpirationTime, false);

            if (legacyExpirationTime != null)
            {
                // Optimization: Do not try to read the password if no expiration time is set.
                dsObject.ReadAttribute(CommonDirectoryAttributes.LAPSPassword, out byte[] admPwdBinary);

                if(admPwdBinary != null && admPwdBinary.Length > 0)
                {
                    string password = Encoding.UTF8.GetString(admPwdBinary);
                    legacyLapsPassword = new LapsPasswordInformation(this.ComputerName, password, legacyExpirationTime);
                }
            }

            if (legacyLapsPassword != null)
            {
                if(this._lapsPasswords == null)
                {
                    this._lapsPasswords = new List<LapsPasswordInformation>();
                }

                this._lapsPasswords.Add(legacyLapsPassword);
            }
        }

        protected void LoadWindowsLAPS(DirectoryObject dsObject, IKdsRootKeyResolver? rootKeyResolver = null)
        {
            dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsPasswordExpirationTime, out DateTime? expirationTime, false);

            if (expirationTime != null)
            {
                var windowsLapsPasswords = new List<LapsPasswordInformation>();

                // Read msLAPS-Password
                dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsPassword, out byte[] binaryLapsJson);

                if (binaryLapsJson != null && binaryLapsJson.Length > 0)
                {
                    // Parse the binary LAPS password
                    var cleartextPassword = LapsClearTextPassword.Parse(binaryLapsJson);
                    var cleartextPasswordInfo = new LapsPasswordInformation(this.ComputerName, cleartextPassword, expirationTime);
                    windowsLapsPasswords.Add(cleartextPasswordInfo);
                }

                // Read msLAPS-EncryptedPassword
                dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsEncryptedPassword, out byte[] binaryEncryptedPassword);

                if (binaryEncryptedPassword != null && binaryEncryptedPassword.Length > 0)
                {
                    var encryptedPassword = new LapsEncryptedPassword(binaryEncryptedPassword);
                    var encryptedPasswordInfo = new LapsPasswordInformation(this.ComputerName, encryptedPassword, LapsPasswordSource.EncryptedPassword, expirationTime.Value, rootKeyResolver);
                    windowsLapsPasswords.Add(encryptedPasswordInfo);
                }

                // Read msLAPS-EncryptedPasswordHistory
                dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsEncryptedPasswordHistory, out byte[][] encryptedPasswordHistory);

                if (encryptedPasswordHistory != null && encryptedPasswordHistory.Length > 0)
                {
                    foreach (var binaryHistoricalPassword in encryptedPasswordHistory)
                    {
                        var historicalPassword = new LapsEncryptedPassword(binaryHistoricalPassword);
                        var historicalPasswordInfo = new LapsPasswordInformation(this.ComputerName, historicalPassword, LapsPasswordSource.EncryptedPasswordHistory, expiration: null, rootKeyResolver);
                        windowsLapsPasswords.Add(historicalPasswordInfo);
                    }
                }

                // Read msLAPS-EncryptedDSRMPassword
                dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPassword, out byte[] encryptedDsrmPassword);

                if (encryptedDsrmPassword != null && encryptedDsrmPassword.Length > 0)
                {
                    var dsrmPassword = new LapsEncryptedPassword(encryptedDsrmPassword);
                    var dsrmPasswordInfo = new LapsPasswordInformation(this.ComputerName, dsrmPassword, LapsPasswordSource.EncryptedDSRMPassword, expirationTime.Value, rootKeyResolver);
                    windowsLapsPasswords.Add(dsrmPasswordInfo);
                }

                // Read msLAPS-EncryptedDSRMPasswordHistory
                dsObject.ReadAttribute(CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPasswordHistory, out byte[][] encryptedDsrmPasswordHistory);

                if (encryptedDsrmPasswordHistory != null && encryptedDsrmPasswordHistory.Length > 0)
                {
                    foreach (var binarydsrmPassword in encryptedDsrmPasswordHistory)
                    {
                        var historicalDsrmPassword = new LapsEncryptedPassword(binarydsrmPassword);
                        var historicalDsrmPasswordInfo = new LapsPasswordInformation(this.ComputerName, historicalDsrmPassword, LapsPasswordSource.EncryptedDSRMPasswordHistory, expiration: null, rootKeyResolver);
                        windowsLapsPasswords.Add(historicalDsrmPasswordInfo);
                    }
                }

                if(windowsLapsPasswords.Count > 0)
                {
                    if (this._lapsPasswords == null)
                    {
                        this._lapsPasswords = windowsLapsPasswords;
                    }
                    else
                    {
                        this._lapsPasswords.AddRange(windowsLapsPasswords);
                    }
                }
            }
        }
    }
}
