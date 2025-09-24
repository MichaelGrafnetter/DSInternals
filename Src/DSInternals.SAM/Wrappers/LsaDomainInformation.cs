using System.Security.Principal;
using DSInternals.SAM.Interop;

namespace DSInternals.SAM;

/// <summary>
/// Represents information about the account domain of a LSA server.
/// </summary>
public struct LsaDomainInformation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LsaDomainInformation"/> struct.
    /// </summary>
    public LsaDomainInformation() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="LsaDomainInformation"/> struct from a native structure.
    /// </summary>
    /// <param name="nativeInfo">The native structure containing domain information.</param>
    internal LsaDomainInformation(LsaDomainInformationNative nativeInfo)
    {

        this.Name = nativeInfo.DomainName.Buffer;
        this.Sid = nativeInfo.GetDomainSid();
    }

    /// <summary>
    /// Name of the account domain.
    /// </summary>
    public string Name;

    /// <summary>
    /// SID of the account domain.
    /// </summary>
    public SecurityIdentifier Sid;
}
