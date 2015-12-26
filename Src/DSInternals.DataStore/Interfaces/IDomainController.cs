namespace DSInternals.DataStore
{
    using System;

    public interface IDomainController
    {
        DateTime? BackupExpiration { get; }
        long? BackupUsn { get; }
        string Domain { get; }
        Guid DsaGuid { get; }
        int? Epoch { get; }
        long HighestCommittedUsn { get; }
        Guid InvocationId { get; }
        bool IsGlobalCatalog { get; }
        string Name { get; }
        int NTDSSettingsDNT { get; }
        DomainControllerOptions Options { get; }
        string OSVersion { get; }
        uint? OSVersionMajor { get; }
        uint? OSVersionMinor { get; }
        string SiteName { get; }
        DatabaseState State { get; }
        long? UsnAtIfm { get; }
        string[] WritablePartitions { get; }
    }
}
