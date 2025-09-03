﻿namespace DSInternals.DataStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using DSInternals.Common.Data;
    using DSInternals.Common.Schema;
    using Microsoft.Database.Isam;

    /// <summary>
    /// The DomainController class represents a domain controller in an Active Directory domain.
    /// </summary>
    public class DomainController : IDisposable, IDomainController
    {
        // TODO: Refactor properties and add more of them to the IDomainController interface.
        /// <summary>
        /// The 1.
        /// </summary>
        public const long UsnMinValue = 1;
        /// <summary>
        /// The UsnMaxValue.
        /// </summary>
        public const long UsnMaxValue = long.MaxValue;
        /// <summary>
        /// The 1.
        /// </summary>
        public const long EpochMinValue = 1;
        /// <summary>
        /// The EpochMaxValue.
        /// </summary>
        public const long EpochMaxValue = int.MaxValue;
        private const string CrossRefContainerRDN = "CN=Partitions";
        private const char DnsNameSeparator = '.';

        // List of columns in the hiddentable:
        private const string ntdsSettingsCol = "dsa_col";
        private const string osVersionMinorCol = "osminorversion_col";
        private const string osVersionMajorCol = "osmajorversion_col";
        private const string highestCommitedUsnCol = "usn_col";
        private const string backupExpirationCol = "backupexpiration_col";
        private const string backupUsnCol = "backupusn_col";
        private const string usnAtIfmCol = "usnatrifm_col";
        private const string stateCol = "state_col";
        private const string epochCol = "epoch_col";
        private const string flagsCol = "flags_col";

        private Cursor systemTableCursor;

        // Cache for writable attributes
        private long highestUSNCache;
        private int? epochCache;
        private DateTime? backupExpirationCache;

        public DomainController(DirectoryContext context)
        {
            // TODO: Split to different methods.

            // Open the hiddentable
            this.systemTableCursor = context.OpenSystemTable();

            // Go to the first and only record in the hiddentable:
            this.systemTableCursor.MoveToFirst();

            // Load attributes from the hiddentable:
            this.NTDSSettingsDNT = this.systemTableCursor.RetrieveColumnAsDNTag(ntdsSettingsCol).Value;

            // Some databases like the initial adamntds.dit or ntds.dit on Windows Server 2003 do not contain the OS Version
            if (this.systemTableCursor.TableDefinition.Columns.Contains(osVersionMajorCol))
            {
                uint? osVersionMinor = this.systemTableCursor.RetrieveColumnAsUInt(osVersionMinorCol);
                uint? osVersionMajor = this.systemTableCursor.RetrieveColumnAsUInt(osVersionMajorCol);

                if (osVersionMajor.HasValue && osVersionMinor.HasValue)
                {
                    this.OSVersion = new Version(
                        (int)osVersionMajor.Value,
                        (int)osVersionMinor.Value
                    );
                }
            }

            if (this.systemTableCursor.TableDefinition.Columns.Contains(epochCol))
            {
                // This is a new feature since Windows Server 2008
                this.epochCache = this.systemTableCursor.RetrieveColumnAsInt(epochCol);
            }

            if (this.systemTableCursor.TableDefinition.Columns.Contains(usnAtIfmCol))
            {
                this.UsnAtIfm = this.systemTableCursor.RetrieveColumnAsLong(usnAtIfmCol);
            }

            // Load and cache the backup expiration time
            this.backupExpirationCache = this.systemTableCursor.RetrieveColumnAsTimestamp(backupExpirationCol, true);

            this.BackupUsn = this.systemTableCursor.RetrieveColumnAsLong(backupUsnCol);
            this.State = (DatabaseState) this.systemTableCursor.RetrieveColumnAsInt(stateCol).Value;
            byte[] binaryFlags = this.systemTableCursor.RetrieveColumnAsByteArray(flagsCol);
            var databaseFlags = new DatabaseFlags(binaryFlags);
            this.IsADAM = databaseFlags.ADAMDatabase;
            // TODO: Export other database flags, not just IsADAM.
            // TODO: Load database health
            this.highestUSNCache = this.systemTableCursor.RetrieveColumnAsLong(highestCommitedUsnCol).Value;

            // Now we can load the Invocation ID and other information from the datatable:
            using (var dataTableCursor = context.OpenDataTable())
            {
                // Goto NTDS Settings object:
                DirectorySchema schema = context.Schema;
                dataTableCursor.CurrentIndex = DirectorySchema.DistinguishedNameTagIndex;
                bool ntdsFound = dataTableCursor.GotoKey(Key.Compose(this.NTDSSettingsDNT));

                // Load data from the NTDS Settings object
                this.NTDSSettingsObjectDN = context.DistinguishedNameResolver.Resolve(this.NTDSSettingsDNT);
                this.InvocationId = dataTableCursor.RetrieveColumnAsGuid(schema.FindColumnId(CommonDirectoryAttributes.InvocationId)).Value;
                this.DsaGuid = dataTableCursor.RetrieveColumnAsGuid(schema.FindColumnId(CommonDirectoryAttributes.ObjectGuid)).Value;
                this.Options = dataTableCursor.RetrieveColumnAsDomainControllerOptions(schema.FindColumnId(CommonDirectoryAttributes.Options));
                string ntdsName = dataTableCursor.RetrieveColumnAsString(schema.FindColumnId(CommonDirectoryAttributes.CommonName));

                // The RODC feature is supported since Windows Server 2008+ (version 6.0)
                if (this.OSVersion >= new Version(6,0))
                {
                    // Check if this is a RODC
                    DNTag? ntdsSettingsObjectCategory = dataTableCursor.RetrieveColumnAsDNTag(schema.FindColumnId(CommonDirectoryAttributes.ObjectCategory));
                    DNTag? rodcObjectCategory = schema.FindObjectCategory(ClassType.NtdsSettingsRO);

                    // The objectCategory should be either nTDSDSA or nTDSDSARO
                    if (ntdsSettingsObjectCategory == rodcObjectCategory)
                    {
                        this.IsReadOnly = true;
                    }
                }

                // Retrieve Configuration Naming Context
                this.ConfigurationNamingContextDNT = dataTableCursor.RetrieveColumnAsDNTag(schema.NamingContextDistinguishedNameTagColumnId).Value;
                this.ConfigurationNamingContext = context.DistinguishedNameResolver.Resolve(this.ConfigurationNamingContextDNT);

                // Forest Root Domain NC should be the parent of the Configuration NC
                this.ForestRootNamingContext = this.ConfigurationNamingContext.Parent;

                // Retrieve Schema Naming Context
                this.SchemaNamingContextDNT = dataTableCursor.RetrieveColumnAsDNTag(schema.FindColumnId(CommonDirectoryAttributes.SchemaLocation)).Value;
                this.SchemaNamingContext = context.DistinguishedNameResolver.Resolve(this.SchemaNamingContextDNT);

                // Goto DC object (parent of NTDS):
                bool dcFound = dataTableCursor.GotoParentObject(schema);

                // Load data from the DC object

                // Load DC name:
                string dcName = dataTableCursor.RetrieveColumnAsString(schema.FindColumnId(CommonDirectoryAttributes.CommonName));

                // DC name is null in the initial database, so use NTDS Settings object's CN instead
                this.Name = dcName ?? ntdsName;

                // Load DNS Host Name
                this.DNSHostName = dataTableCursor.RetrieveColumnAsString(schema.FindColumnId(CommonDirectoryAttributes.DnsHostName));

                // Load server reference to domain partition:
                DNTag dcDNTag = dataTableCursor.RetrieveColumnAsDNTag(schema.DistinguishedNameTagColumnId).Value;
                this.ServerReferenceDNT = context.LinkResolver.GetLinkedDNTag(dcDNTag, CommonDirectoryAttributes.ServerReference);

                // Load the DSA object DN
                this.ServerObjectDN = context.DistinguishedNameResolver.Resolve(dcDNTag);

                // Goto Servers object (parent of DC):
                bool serversFound = dataTableCursor.GotoParentObject(schema);

                // Goto Site object (parent of servers):
                bool siteFound = dataTableCursor.GotoParentObject(schema);

                // Load data from the Site object
                if(siteFound)
                {
                    this.SiteName = dataTableCursor.RetrieveColumnAsString(schema.FindColumnId(CommonDirectoryAttributes.CommonName));
                }

                // Load partitions (linked multivalue attribute)
                // TODO: Does not return PAS partitions on RODCs
                IEnumerable<DNTag> partitionDNTags = context.LinkResolver.GetLinkedDNTags(this.NTDSSettingsDNT, CommonDirectoryAttributes.MasterNamingContexts);
                this.WritablePartitions = [.. partitionDNTags.Select(dnTag => context.DistinguishedNameResolver.Resolve(dnTag).ToString())];

                // Load domain (linked multivalue attribute)
                // TODO: Test this against a GC and RODC:
                this.DomainNamingContextDNT = context.LinkResolver.GetLinkedDNTag(this.NTDSSettingsDNT, CommonDirectoryAttributes.DomainNamingContexts);
                if (this.DomainNamingContextDNT.HasValue)
                {
                    // Move cursor to domain:
                    bool domainObjectFound = dataTableCursor.GotoKey(Key.Compose(this.DomainNamingContextDNT.Value));

                    // Load domain SID
                    this.DomainSid = dataTableCursor.RetrieveColumnAsSid(schema.FindColumnId(CommonDirectoryAttributes.ObjectSid));

                    // Load domain GUID
                    this.DomainGuid = dataTableCursor.RetrieveColumnAsGuid(schema.FindColumnId(CommonDirectoryAttributes.ObjectGuid));

                    // Load domain naming context:
                    this.DomainNamingContext = context.DistinguishedNameResolver.Resolve(this.DomainNamingContextDNT.Value);

                    // Load the domain functional level
                    this.DomainMode = dataTableCursor.RetrieveColumnAsFunctionalLevel(schema.FindColumnId(CommonDirectoryAttributes.FunctionalLevel));
                }

                // Goto server object in domain partition
                if (this.ServerReferenceDNT.HasValue)
                {
                    bool serverFound = dataTableCursor.GotoKey(Key.Compose(this.ServerReferenceDNT.Value));

                    // Load DC OS
                    this.OSName = dataTableCursor.RetrieveColumnAsString(schema.FindColumnId(CommonDirectoryAttributes.OperatingSystemName));

                    // Load DC GUID
                    this.Guid = dataTableCursor.RetrieveColumnAsGuid(schema.FindColumnId(CommonDirectoryAttributes.ObjectGuid));

                    // Load DC SID
                    this.Sid = dataTableCursor.RetrieveColumnAsSid(schema.FindColumnId(CommonDirectoryAttributes.ObjectSid));

                    // Load DC DN
                    this.ServerReference = context.DistinguishedNameResolver.Resolve(this.ServerReferenceDNT.Value);
                }

                // The crossRefContainer does not exist in initial DBs, as it is created during dcpromo.
                if (this.State != DatabaseState.Boot)
                {
                    // Construct crossRefContainer DN (CN=Partitions,CN=Configuration,...)
                    var crossRefContainer = new DistinguishedName(CrossRefContainerRDN);
                    crossRefContainer.AddParent(this.ConfigurationNamingContext);

                    // Goto crossRefContainer
                    var crossRefContainerDNT = context.DistinguishedNameResolver.Resolve(crossRefContainer);
                    bool crossRefContainerFound = dataTableCursor.GotoKey(Key.Compose(crossRefContainerDNT));

                    // Read the forest functional level from the crossRefContainer object we just located
                    this.ForestMode = dataTableCursor.RetrieveColumnAsFunctionalLevel(schema.FindColumnId(CommonDirectoryAttributes.FunctionalLevel));

                    // Go through all crossRef objects (children of the crossRefContainer)
                    dataTableCursor.FindChildren(schema);
                    while (dataTableCursor.MoveNext())
                    {
                        // Find the directory partition that is associated with the current crossRef object
                        var partitionNCNameDNT = dataTableCursor.RetrieveColumnAsDNTag(schema.FindColumnId(CommonDirectoryAttributes.NamingContextName));

                        // Note that foreign crossRef objects do not have nCName set

                        if (partitionNCNameDNT == this.DomainNamingContextDNT)
                        {
                            // This must be the DC's domain crossRef object, so we can retrieve its NetBIOS name.
                            this.NetBIOSDomainName = dataTableCursor.RetrieveColumnAsString(schema.FindColumnId(CommonDirectoryAttributes.NetBIOSName));
                        }

                        if (partitionNCNameDNT == this.ConfigurationNamingContextDNT)
                        {
                            // This must be the configuration partition's crossRef object, so we can retrieve the forest DNS name.
                            this.ForestName = dataTableCursor.RetrieveColumnAsString(schema.FindColumnId(CommonDirectoryAttributes.DnsRoot));
                        }
                    }
                }
            }
        }

        public DNTag NTDSSettingsDNT
        {
            get;
            private set;
        }

        public DNTag SchemaNamingContextDNT
        {
            get;
            private set;
        }

        public DNTag ConfigurationNamingContextDNT
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the GUID associated with the DC account.
        /// </summary>
        public Guid? Guid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Security ID (SID) of the DC account.
        /// </summary>
        public SecurityIdentifier Sid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the operating system version of this domain controller.
        /// </summary>
        public Version? OSVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the domain that this domain controller is a member of.
        /// </summary>
        public string DomainName
        {
            get
            {
                // DomainNamingContext in ADAM DB might be null.
                return (this.DomainNamingContext != null) ? this.DomainNamingContext.GetDnsName() : null;
            }
        }

        /// <summary>
        /// Gets the name of the forest that this domain controller is a member of.
        /// </summary>
        public string ForestName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the SID of the domain.
        /// </summary>
        public SecurityIdentifier DomainSid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the GUID of the domain.
        /// </summary>
        public Guid? DomainGuid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the mode that this domain is operating in.
        /// </summary>
        public FunctionalLevel DomainMode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the mode that this forest is operating in.
        /// </summary>
        public FunctionalLevel ForestMode
        {
            get;
            private set;
        }

        /// <summary>
        /// DSA Database Epoch
        /// </summary>
        public int? Epoch
        {
            get
            {
                return this.epochCache;
            }
            set
            {
                if (this.epochCache == null)
                {
                    // This is a legacy DB without the epoch_col, so we cannot change it.
                    throw new InvalidOperationException("Current database does not support epoch information.");
                }

                // Update table
                this.systemTableCursor.BeginEditForUpdate();
                this.systemTableCursor.EditRecord[epochCol] = value;
                this.systemTableCursor.AcceptChanges();
                // Cache the value
                this.epochCache = value;
            }
        }

        // TODO: Nullable InvocationId?
        public Guid InvocationId
        {
            get;
            private set;
        }

        public Guid DsaGuid
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the name of the directory server.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        public string DNSHostName
        {
            get;
            private set;
        }

        public string HostName
        {
            get
            {
                return !String.IsNullOrEmpty(this.DNSHostName) ? this.DNSHostName.Split(DnsNameSeparator)[0] : this.Name;
            }
        }

        // TODO: Rename to ComputerObjectDN
        public DistinguishedName ServerReference
        {
            get;
            private set;
        }

        public DistinguishedName NTDSSettingsObjectDN
        {
            get;
            private set;
        }

        public DistinguishedName ServerObjectDN
        {
            get;
            private set;
        }

        /// <summary>
        /// Backup expiration time.
        /// </summary>
        public DateTime? BackupExpiration
        {
            get
            {
                return this.backupExpirationCache;
            }

            set
            {
                // Update table
                this.systemTableCursor.BeginEditForUpdate();
                this.systemTableCursor.SetValue(backupExpirationCol, value, true);
                this.systemTableCursor.AcceptChanges();

                // Cache the value
                this.backupExpirationCache = value;
            }
        }

        /// <summary>
        /// Gets the wtitable partitions on this directory server.
        /// </summary>
        public string[] WritablePartitions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the site that this domain controller belongs to.
        /// </summary>
        public string SiteName
        {
            get;
            private set;
        }

        public DatabaseState State
        {
            get;
            private set;
        }

        public long? BackupUsn
        {
            get;
            private set;
        }

        public long? UsnAtIfm
        {
            get;
            private set;
        }

        public DomainControllerOptions Options
        {
            get;
            private set;
        }
        /// <summary>
        /// Determines if this domain controller is a global catalog server.
        /// </summary>
        public bool IsGlobalCatalog
        {
            get
            {
                return this.Options.HasFlag(DomainControllerOptions.GlobalCatalog);
            }
        }

        /// <summary>
        /// Indicates wheteher this is a RODC.
        /// </summary>
        public bool IsReadOnly
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the highest update sequence number that has been committed to this domain controller.
        /// </summary>
        public long HighestCommittedUsn
        {
            get
            {
                return this.highestUSNCache;
            }
            set
            {
                // Update table
                this.systemTableCursor.BeginEditForUpdate();
                this.systemTableCursor.EditRecord[highestCommitedUsnCol] = value;
                this.systemTableCursor.AcceptChanges();

                // Cache the value
                this.highestUSNCache = value;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.systemTableCursor != null)
                {
                    this.systemTableCursor.Dispose();
                    this.systemTableCursor = null;
                }
            }
        }

        /// <summary>
        /// Dispose implementation.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DNTag? DomainNamingContextDNT
        {
            get;
            private set;
        }

        public DistinguishedName DomainNamingContext
        {
            get;
            private set;
        }

        public DistinguishedName ForestRootNamingContext
        {
            get;
            private set;
        }

        public DistinguishedName ConfigurationNamingContext
        {
            get;
            private set;
        }

        public DistinguishedName SchemaNamingContext
        {
            get;
            private set;
        }

        public string NetBIOSDomainName
        {
            get;
            private set;
        }

        public DNTag? ServerReferenceDNT
        {
            get;
            private set;
        }

        public string OSName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the IsADAM.
        /// </summary>
        public bool IsADAM {
            get;
            private set;
        }
    }
}
