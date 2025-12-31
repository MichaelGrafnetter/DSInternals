using System.Security.Principal;
using DSInternals.Common.Data;

namespace DSInternals.DataStore;
public interface IDomainController
{
    DateTime? BackupExpiration { get; }
    long? BackupUsn { get; }
    DistinguishedName ConfigurationNamingContext { get; }
    string DNSHostName { get; }
    Guid? DomainGuid { get; }
    FunctionalLevel DomainMode { get; }
    string DomainName { get; }
    DistinguishedName DomainNamingContext { get; }
    SecurityIdentifier DomainSid { get; }
    Guid DsaGuid { get; }
    int? Epoch { get; }
    FunctionalLevel ForestMode { get; }
    string ForestName { get; }
    Guid? Guid { get; }
    long HighestCommittedUsn { get; }
    Guid InvocationId { get; }
    bool IsADAM { get; }
    bool IsGlobalCatalog { get; }
    bool IsReadOnly { get; }
    string Name { get; }
    string NetBIOSDomainName { get; }
    DomainControllerOptions Options { get; }
    string OSName { get; }
    Version? OSVersion { get; }
    DistinguishedName SchemaNamingContext { get; }
    DistinguishedName ServerReference { get; }
    SecurityIdentifier Sid { get; }
    string SiteName { get; }
    DatabaseState State { get; }
    long? UsnAtIfm { get; }
    string[] WritablePartitions { get; }
}
