namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Interop;
    using DSInternals.DataStore;
    using System;
    using System.IO;
    using System.Management.Automation;
    using System.Reflection;
    using System.Security;
    using System.Text;

    [Cmdlet(VerbsCommon.New, "ADDBRestoreFromMediaScript")]
    [OutputType(typeof(string))]
    public class NewADDBRestoreFromMediaScriptCommand : ADDBCommandBase
    {
        private const int DSRMPasswordMinLength = 7;
        private const string ScriptTemplateResourceName = "DSInternals.PowerShell.ADDBRestoreFromMediaScriptTemplate.ps1";
        private const string DefaultSysvolPath = @"..\SYSVOL\";
        private const string DefaultRegistryPath = @"..\registry\SYSTEM";

        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
        [AcceptHexString]
        [Alias("key", "SysKey", "SystemKey")]
        public byte[] BootKey
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        [Alias("SysVol")]
        public string SysvolPath
        {
            get;
            set;
        }

        [Parameter(Mandatory = true)]
        [ValidateNotNull]
        [ValidatePasswordLength(DSRMPasswordMinLength, NTHash.MaxInputLength)]
        [Alias("SafeModeAdminPassword", "AdminPassword", "DSRMPassword")]
        public SecureString SafeModeAdministratorPassword
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        [Alias("DoNotInstallDNS", "DoNotInstallDNSServer", "DontInstallDNS", "SkipDNS")]
        public SwitchParameter SkipDNSServer
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string PostInstallScriptPath
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            DomainController dc = this.DirectoryContext.DomainController;

            if (dc.IsADAM)
            {
                this.ThrowTerminatingError(new ErrorRecord(
                    new NotSupportedException("AD LDS databases are not supported by this operation."),
                    "ADAMRestoreUnsupported",
                    ErrorCategory.InvalidOperation,
                    null));
            }

            if (dc.WritablePartitions.Length <= 0)
            {
                this.ThrowTerminatingError(new ErrorRecord(
                    new NotSupportedException("RODC databases are not supported by this operation."),
                    "RODCRestoreUnsupported",
                    ErrorCategory.InvalidOperation,
                    null));
            }

            if (!dc.IsGlobalCatalog)
            {
                this.WriteWarning("The database does not originate from a Global Catalog. At least one GC must be online for AD to work properly.");
            }

            if (dc.BackupExpiration.HasValue && dc.BackupExpiration.Value < DateTime.Now)
            {
                this.WriteWarning("The database backup seems to be past its shelf life, which could break replication.");
            }

            if (this.BootKey == null)
            {
                // No boot key has been provided so we need to get one from registry.
                // Presume that the database is part of an IFM backup:
                string registryPath = Path.Combine(this.DirectoryContext.DSAWorkingDirectory, DefaultRegistryPath);
                string resolvedRegistryPath = this.ResolveFilePath(registryPath);
                this.BootKey = BootKeyRetriever.GetBootKey(resolvedRegistryPath);
            }

            // Fetch the local server's boot key from the registry:
            byte[] localBootKey = BootKeyRetriever.GetBootKey();

            using (var dsa = new DirectoryAgent(this.DirectoryContext))
            {
                bool bootKeyIsValid = dsa.CheckBootKey(this.BootKey);
                if(!bootKeyIsValid)
                {
                    this.ThrowTerminatingError(new ErrorRecord(
                        new ArgumentException("The boot key provided cannot be used to decrypt the database.", nameof(BootKey)),
                        "BootKeyInvalid",
                        ErrorCategory.InvalidArgument,
                        null));
                }
            }

            if (this.SysvolPath == null)
            {
                // Presume that the database is part of an IFM backup:
                this.SysvolPath = Path.Combine(this.DirectoryContext.DSAWorkingDirectory, DefaultSysvolPath);
            }

            string winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string targetDatabaseDirectory = Path.Combine(winDir, "NTDS");
            string targetDatabasePath = Path.Combine(targetDatabaseDirectory, "ntds.dit");
            string targetSysvolPath = Path.Combine(winDir, "SYSVOL");

            string postInstallScriptAbsolutePath = string.Empty;

            if (!string.IsNullOrEmpty(this.PostInstallScriptPath))
            {
                postInstallScriptAbsolutePath = this.ResolveFilePath(this.PostInstallScriptPath);
            }

            // Load the RFM script template and replace placeholders with values from the DB:
            string template = LoadScriptTemplate();
            StringBuilder script = new StringBuilder(template).
                Replace("{DCName}", dc.Name).
                Replace("{DCHostName}", dc.HostName).
                Replace("{DCGuid}", dc.Guid.ToString()).
                Replace("{DCDistinguishedName}", dc.ServerReference.ToString()).
                Replace("{DomainName}", dc.DomainName).
                Replace("{NetBIOSDomainName}", dc.NetBIOSDomainName).
                Replace("{ForestName}", dc.ForestName).
                Replace("{DomainGuid}", dc.DomainGuid.ToString()).
                Replace("{DomainSid}", dc.DomainSid.ToString()).
                Replace("{ConfigNC}", dc.ConfigurationNamingContext.ToString()).
                Replace("{RootDomainNC}", dc.ForestRootNamingContext.ToString()).
                Replace("{NTDSSettingsObject}", dc.NTDSSettingsObjectDN.ToString()).
                Replace("{DomainMode}", ((int)dc.DomainMode).ToString()).
                Replace("{ForestMode}", ((int)dc.ForestMode).ToString()).
                Replace("{DomainModeString}", (dc.DomainMode).ToString()).
                Replace("{ForestModeString}", (dc.ForestMode).ToString()).
                Replace("{OSName}", dc.OSName).
                Replace("{OldBootKey}", this.BootKey.ToHex()).
                Replace("{CurrentBootKey}", localBootKey.ToHex()).
                Replace("{SourceDBPath}", this.DirectoryContext.DSADatabaseFile).
                Replace("{SourceDBDirPath}", this.DirectoryContext.DSAWorkingDirectory).
                Replace("{SourceLogDirPath}", this.DirectoryContext.DatabaseLogFilesPath).
                Replace("{TargetDBDirPath}", targetDatabaseDirectory).
                Replace("{TargetDBPath}", targetDatabasePath).
                Replace("{TargetLogDirPath}", targetDatabaseDirectory).
                Replace("{SourceSysvolPath}", this.ResolveDirectoryPath(this.SysvolPath)).
                Replace("{TargetSysvolPath}", targetSysvolPath).
                Replace("{DNSOnNetwork}", this.SkipDNSServer.IsPresent ? "Yes" : "No").
                Replace("{InstallDNSComment}", this.SkipDNSServer.IsPresent ? "# " : string.Empty).
                Replace("{InstallDNS}", this.SkipDNSServer.IsPresent ? "No" : "Yes").
                Replace("{PostInstallScriptPath}", postInstallScriptAbsolutePath);

            // We need to inject cleartext version of the password into the script for dcpromo. The SecureString will therefore have to appear in managed memory, which is against best practices.
            using (var dsrmPassword = new SafeUnicodeSecureStringPointer(this.SafeModeAdministratorPassword))
            {
                script.Replace("{DSRMPassword}", dsrmPassword.ToString());
            }

            // The script is now ready so write it to standard output
            this.WriteObject(script.ToString());
            script.Clear();
        }

        private static string LoadScriptTemplate()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ScriptTemplateResourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
