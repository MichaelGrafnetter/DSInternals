using System.Net;
using System.Security.Principal;
using DSInternals.Common;
using DSInternals.Common.Interop;
using DSInternals.SAM.Interop;

namespace DSInternals.SAM;

/// <summary>
/// Represents a connection to a SAM server.
/// </summary>
public sealed class SamServer : SamObject
{
    /// <summary>
    /// The name of the built-in domain.
    /// </summary>
    public const string BuiltinDomainName = "Builtin";
    private const uint PreferedMaximumBufferLength = 1000;
    private const uint InitialEnumerationContext = 0;

    /// <summary>
    /// The name of the server.
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SamServer"/> class and connects to the specified server with the specified credentials and access mask.
    /// </summary>
    /// <param name="serverName">The name of the server.</param>
    /// <param name="credential">The credentials to use for the connection.</param>
    /// <param name="accessMask">The access mask to use for the connection.</param>
    public SamServer(string serverName, SamServerAccessMask accessMask = SamServerAccessMask.MaximumAllowed, NetworkCredential credential = null) : base(null)
    {
        if (string.IsNullOrEmpty(serverName))
        {
            throw new ArgumentNullException(nameof(serverName));
        }

        this.Name = serverName;

        NtStatus result = (credential != null) ?
            NativeMethods.SamConnectWithCreds(serverName, out SafeSamHandle serverHandle, accessMask, credential) :
            NativeMethods.SamConnect(serverName, out serverHandle, accessMask);

        Validator.AssertSuccess(result);
        this.Handle = serverHandle;
    }

    /// <summary>
    /// Enumerates the domains hosted on the connected SAM server.
    /// </summary>
    /// <returns>A list of domain names.</returns>
    public string[] EnumerateDomains()
    {
        uint enumerationContext = InitialEnumerationContext;
        List<string> domains = new();
        NtStatus lastResult;

        do
        {
            lastResult = NativeMethods.SamEnumerateDomainsInSamServer(this.Handle, ref enumerationContext, out SafeSamPointer buffer, PreferedMaximumBufferLength, out uint countReturned);
            Validator.AssertSuccess(lastResult);

            try
            {
                domains.AddRange(buffer.MarshalAs<SamRidEnumeration>(countReturned)?.Select(item => item.Name.Buffer));
            }
            finally
            {
                buffer.Dispose();
            }
        } while (lastResult == NtStatus.MoreEntries);

        return domains.ToArray();
    }

    /// <summary>
    /// Looks up a domain by name and returns its SID.
    /// </summary>
    /// <param name="domainName">The name of the domain to look up.</param>
    /// <param name="throwIfNotFound">If true, an exception is thrown when the domain is not found. If false, null is returned when the domain is not found.</param>
    /// <returns>The SID of the domain.</returns>
    public SecurityIdentifier? LookupDomain(string domainName, bool throwIfNotFound = true)
    {
        if (string.IsNullOrEmpty(domainName))
        {
            throw new ArgumentNullException(nameof(domainName));
        }

        NtStatus result = NativeMethods.SamLookupDomainInSamServer(this.Handle, domainName, out SecurityIdentifier domainSid);

        if (result == NtStatus.NoSuchDomain && throwIfNotFound == false)
        {
            return null;
        }

        Validator.AssertSuccess(result);
        return domainSid;
    }

    /// <summary>
    /// Opens a domain by name.
    /// </summary>
    /// <param name="domainName">The name of the domain to open.</param>
    /// <param name="accessMask">The access mask to use when opening the domain.</param>
    /// <returns>The opened domain object.</returns>
    public SamDomain OpenDomain(string domainName, SamDomainAccessMask accessMask = SamDomainAccessMask.MaximumAllowed)
    {
        SecurityIdentifier domainSid = this.LookupDomain(domainName, throwIfNotFound: true);
        return this.OpenDomain(domainSid, accessMask);
    }

    /// <summary>
    /// Opens a domain by SID.
    /// </summary>
    /// <param name="domainSid">The SID of the domain to open.</param>
    /// <param name="accessMask">The access mask to use when opening the domain.</param>
    /// <returns>The opened domain object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainSid"/> is null.</exception>
    public SamDomain OpenDomain(SecurityIdentifier domainSid, SamDomainAccessMask accessMask = SamDomainAccessMask.MaximumAllowed)
    {
        if (domainSid == null)
        {
            throw new ArgumentNullException(nameof(domainSid));
        }

        NtStatus result = NativeMethods.SamOpenDomain(this.Handle, accessMask, domainSid, out SafeSamHandle domainHandle);
        Validator.AssertSuccess(result);
        return new SamDomain(domainHandle);
    }
}
