using System.Security.Principal;
using DSInternals.Common.Data;
using DSInternals.DataStore;

namespace DSInternals.PowerShell;
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

    public bool IsReadOnly
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

    public Version? OSVersion
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

    /// <summary>
    /// Creates a data transport object from a native DC.
    /// </summary>
    public static DomainController Create(IDomainController nativeDC)
    {
        return new DomainController()
        {
            Name = nativeDC.Name,
            DNSHostName = nativeDC.DNSHostName,
            ServerReference = nativeDC.ServerReference,
            DsaGuid = nativeDC.DsaGuid,
            Guid = nativeDC.Guid,
            InvocationId = nativeDC.InvocationId,
            Sid = nativeDC.Sid,
            OSName = nativeDC.OSName,
            OSVersion = nativeDC.OSVersion,
            DomainName = nativeDC.DomainName,
            ForestName = nativeDC.ForestName,
            NetBIOSDomainName = nativeDC.NetBIOSDomainName,
            DomainSid = nativeDC.DomainSid,
            DomainGuid = nativeDC.DomainGuid,
            DomainMode = nativeDC.DomainMode,
            ForestMode = nativeDC.ForestMode,
            SiteName = nativeDC.SiteName,
            IsADAM = nativeDC.IsADAM,
            IsGlobalCatalog = nativeDC.IsGlobalCatalog,
            IsReadOnly = nativeDC.IsReadOnly,
            Options = nativeDC.Options,
            DomainNamingContext = nativeDC.DomainNamingContext,
            ConfigurationNamingContext = nativeDC.ConfigurationNamingContext,
            SchemaNamingContext = nativeDC.SchemaNamingContext,
            WritablePartitions = nativeDC.WritablePartitions,
            Epoch = nativeDC.Epoch,
            BackupExpiration = nativeDC.BackupExpiration,
            BackupUsn = nativeDC.BackupUsn,
            UsnAtIfm = nativeDC.UsnAtIfm,
            HighestCommittedUsn = nativeDC.HighestCommittedUsn,
            State = nativeDC.State
        };
    }
}
