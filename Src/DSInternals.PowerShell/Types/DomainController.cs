namespace DSInternals.PowerShell
{
    using System;
    using DSInternals.DataStore;
    using System.Security.Principal;
    using DSInternals.Common.Data;
    // Transport object
    public class DomainController : IDomainController
    {
        public string Name
        {
            get;
            set;
        }

        public string DNSHostName
        {
            get;
            set;
        }

        public DistinguishedName ServerReference
        {
            get;
            set;
        }

        public string DomainName
        {
            get;
            set;
        }

        public string ForestName
        {
            get;
            set;
        }

        public string NetBIOSDomainName
        {
            get;
            set;
        }

        public SecurityIdentifier DomainSid
        {
            get;
            set;
        }

        public Guid? DomainGuid
        {
            get;
            set;
        }

        public Guid? Guid
        {
            get;
            set;
        }

        public SecurityIdentifier Sid
        {
            get;
            set;
        }

        public FunctionalLevel DomainMode
        {
            get;
            set;
        }

        public FunctionalLevel ForestMode
        {
            get;
            set;
        }

        public string SiteName
        {
            get;
            set;
        }

        public System.Guid DsaGuid
        {
            get;
            set;
        }
                
        public Guid InvocationId
        {
            get;
            set;
        }

        public bool IsADAM
        {
            get;
            set;
        }

        public bool IsGlobalCatalog
        {
            get;
            set;
        }

        public DomainControllerOptions Options
        {
            get;
            set;
        }

        public string OSName
        {
            get;
            set;
        }

        public string OSVersion
        {
            get;
            set;
        }

        public uint? OSVersionMajor
        {
            get;
            set;
        }

        public uint? OSVersionMinor
        {
            get;
            set;
        }

        public DistinguishedName DomainNamingContext
        {
            get;
            set;
        }

        public DistinguishedName ConfigurationNamingContext
        {
            get;
            set;
        }

        public DistinguishedName SchemaNamingContext
        {
            get;
            set;
        }

        public string[] WritablePartitions
        {
            get;
            set;
        }

        public DatabaseState State
        {
            get;
            set;
        }

        public long HighestCommittedUsn
        {
            get;
            set;
        }

        public long? UsnAtIfm
        {
            get;
            set;
        }

        public long? BackupUsn
        {
            get;
            set;
        }

        public DateTime? BackupExpiration
        {
            get;
            set;
        }

        public int? Epoch
        {
            get;
            set;
        }
    }
}