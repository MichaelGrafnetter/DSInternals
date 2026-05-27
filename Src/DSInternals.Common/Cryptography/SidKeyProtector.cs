using System.Security.Cryptography;
using System.Security.Principal;
using DSInternals.Common.Data;

namespace DSInternals.Common.Cryptography;

/// <summary>
/// Represents a single DPAPI-NG protector bound to a SID or SDDL principal.
/// </summary>
public sealed class SidKeyProtector
{
    private Lazy<byte[]> _targetSDDLRaw;

    private SidKeyProtector()
    {
    }

    /// <summary>
    /// Gets the formatted protection descriptor rule string for this protector.
    /// </summary>
    /// <value>The descriptor rule string formatted as <c>SID=...</c> or <c>SDDL=...</c>.</value>
    /// <example>
    /// <code language="text">SID=S-1-5-21-3288850392-3299536932-2614793081-1000</code>
    /// <code language="text">SDDL=O:S-1-5-5-0-290724G:SYD:(A;;CCDC;;;S-1-5-5-0-290724)(A;;DC;;;WD)</code>
    /// </example>
    public string Descriptor { get; private set; }

    /// <summary>
    /// Gets the KDS protection key identifier for this protector.
    /// </summary>
    public ProtectionKeyIdentifier ProtectionKeyIdentifier { get; private set; }

    /// <summary>
    /// Gets the target SID, or <see langword="null" /> when this protector targets an SDDL principal.
    /// </summary>
    public SecurityIdentifier? TargetSid { get; private set; }

    /// <summary>
    /// Indicates whether this protector is based on an SDDL principal (true) or a SID principal (false).
    /// </summary>
    public bool IsSDDL => TargetSid is null;

    /// <summary>
    /// Gets the target security descriptor in SDDL form.
    /// </summary>
    /// <value>The SDDL form derived from the target SID, or the SDDL string supplied by an SDDL protector.</value>
    public string TargetSDDL { get; private set; }

    /// <summary>
    /// Gets the target security descriptor in binary self-relative form.
    /// </summary>
    public byte[] TargetSDDLRaw => this._targetSDDLRaw.Value;

    /// <summary>
    /// Gets the key encryption algorithm used to wrap the content encryption key for this protector.
    /// </summary>
    public Oid KeyEncryptionAlgorithm { get; private set; } = new Oid();

    /// <summary>
    /// Gets the encrypted content encryption key for this protector.
    /// </summary>
    public ReadOnlyMemory<byte> EncryptedKey { get; private set; }

    internal static SidKeyProtector FromSid(
        SecurityIdentifier targetSid,
        ProtectionKeyIdentifier protectionKeyIdentifier,
        Oid keyEncryptionAlgorithm,
        ReadOnlyMemory<byte> encryptedKey)
    {
        ArgumentNullException.ThrowIfNull(targetSid);

        return new SidKeyProtector
        {
            Descriptor = ProtectionDescriptorFormatter.FormatSid(targetSid),
            ProtectionKeyIdentifier = protectionKeyIdentifier,
            TargetSid = targetSid,
            TargetSDDL = $"O:SYG:SYD:(A;;CCDC;;;{targetSid.Value})(A;;DC;;;WD)",
            _targetSDDLRaw = new Lazy<byte[]>(() => GroupKeyEnvelope.ConvertSidToSecurityDescriptor(targetSid)),
            KeyEncryptionAlgorithm = keyEncryptionAlgorithm,
            EncryptedKey = encryptedKey
        };
    }

    internal static SidKeyProtector FromSddl(
        string sddl,
        ProtectionKeyIdentifier protectionKeyIdentifier,
        Oid keyEncryptionAlgorithm,
        ReadOnlyMemory<byte> encryptedKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(sddl);

        return new SidKeyProtector
        {
            Descriptor = ProtectionDescriptorFormatter.FormatSddl(sddl),
            ProtectionKeyIdentifier = protectionKeyIdentifier,
            TargetSid = null,
            TargetSDDL = sddl,
            _targetSDDLRaw = new Lazy<byte[]>(() => sddl.SddlToBinary()),
            KeyEncryptionAlgorithm = keyEncryptionAlgorithm,
            EncryptedKey = encryptedKey
        };
    }
}
