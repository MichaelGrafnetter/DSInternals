using DSInternals.Common.Schema;

namespace DSInternals.Common.Data;

public class BitLockerRecoveryInformation
{
    public BitLockerRecoveryInformation(DirectoryObject dsObject)
    {
        // Parameter validation
        ArgumentNullException.ThrowIfNull(dsObject);

        // Load generic attribute values
        this.DistinguishedName = dsObject.DistinguishedName;
        this.ObjectGuid = dsObject.Guid;

        // whenCreated
        dsObject.ReadAttribute(CommonDirectoryAttributes.WhenCreated, out DateTime? whenCreated, true);
        this.WhenCreated = whenCreated.Value;

        // Load BitLocker-specific mandatory attribute values

        // ms-FVE-RecoveryGuid
        dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryGuid, out Guid? recoveryGuid);
        this.RecoveryGuid = recoveryGuid.Value;

        // ms-FVE-RecoveryPassword
        dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryPassword, out string recoveryPassword);
        this.RecoveryPassword = recoveryPassword;

        // Load BitLocker-specific optional attribute values

        // ms-FVE-VolumeGuid
        dsObject.ReadAttribute(CommonDirectoryAttributes.FVEVolumeGuid, out Guid? volumeGuid);
        this.VolumeGuid = volumeGuid;

        // ms-FVE-KeyPackage
        dsObject.ReadAttribute(CommonDirectoryAttributes.FVEKeyPackage, out byte[] keyPackage);
        this.KeyPackage = keyPackage;
    }

    public string DistinguishedName
    {
        get;
        private set;
    }

    public string ComputerName
    {
        get
        {
            // Computer name should be the parent RDN
            var parsedDN = new DistinguishedName(this.DistinguishedName);
            return parsedDN.Components.Count >= 2 ? parsedDN.Components[1].Value : null;
        }
    }

    public Guid ObjectGuid
    {
        get;
        private set;
    }

    public Guid? VolumeGuid
    {
        get;
        private set;
    }

    public Guid RecoveryGuid
    {
        get;
        private set;
    }

    public string RecoveryPassword
    {
        get;
        private set;
    }

    public byte[] KeyPackage
    {
        get;
        private set;
    }

    public DateTime WhenCreated
    {
        get;
        private set;
    }

    public override string ToString()
    {
        return $"Recovery ID: {this.RecoveryGuid}, Key: {this.RecoveryPassword}, Date: {this.WhenCreated}";
    }
}
