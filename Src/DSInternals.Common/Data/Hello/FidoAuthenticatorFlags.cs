#pragma warning disable CA1028 // Enum Storage should be Int32

namespace DSInternals.Common.Data.Fido;

/// <summary>
/// Authenticator data flags 
/// <see cref="https://www.w3.org/TR/webauthn/#flags"/>
/// </summary>
[Flags]
public enum AuthenticatorFlags : byte
{
    /// <summary>
    /// User Present indicates that the user presence test has completed successfully.
    /// <see cref="https://www.w3.org/TR/webauthn/#up"/>
    /// </summary>
    UserPresent = 0x1,

    /// <summary>
    /// Reserved for future use (RFU1)
    /// </summary>
    RFU1 = 0x2,

    /// <summary>
    /// User Verified indicates that the user verification process has completed successfully.
    /// <see cref="https://www.w3.org/TR/webauthn/#uv"/>
    /// </summary>
    UserVerified = 0x4,

    /// <summary>
    /// Reserved for future use (RFU2)
    /// </summary>
    RFU2 = 0x8,

    /// <summary>
    /// Reserved for future use (RFU3)
    /// </summary>
    RFU3 = 0x10,

    /// <summary>
    /// Reserved for future use (RFU4)
    /// </summary>
    RFU4 = 0x20,

    /// <summary>
    /// Attested credential data included indicates that the authenticator added attested credential data to the authenticator data.
    /// <see cref="https://www.w3.org/TR/webauthn/#attested-credential-data"/>
    /// </summary>
    AttestationData = 0x40,

    /// <summary>
    /// Extension data included indicates that the authenticator added extension data to the authenticator data.
    /// <see cref="https://www.w3.org/TR/webauthn/#authdataextensions"/>
    /// </summary>
    ExtensionData = 0x80,
}
