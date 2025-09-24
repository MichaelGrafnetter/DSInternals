using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using DSInternals.Common;
using DSInternals.Common.Data;
using DSInternals.Common.Interop;
using DSInternals.SAM.Interop;
using Windows.Win32.Security.Authentication.Identity;

namespace DSInternals.SAM;

/// <summary>
/// Represents a connection to the Local Security Authority (LSA) on a local or remote system.
/// </summary>
public sealed class LsaPolicy : IDisposable
{
    private SafeLsaPolicyHandle policyHandle;

    /// <summary>
    /// Initializes a new instance of the <see cref="LsaPolicy"/> class with the specified access rights on the local system.
    /// </summary>
    /// <param name="accessMask">The access rights to be granted to the policy handle.</param>
    public LsaPolicy(LsaPolicyAccessMask accessMask) : this(null, accessMask) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="LsaPolicy"/> class with the specified access rights on a remote system.
    /// </summary>
    /// <param name="systemName">The name of the remote system.</param>
    /// <param name="accessMask">The access rights to be granted to the policy handle.</param>
    public LsaPolicy(string systemName, LsaPolicyAccessMask accessMask)
    {
        var status = NativeMethods.LsaOpenPolicy(systemName, accessMask, out this.policyHandle);
        Validator.AssertSuccess(status);
    }

    /// <summary>
    /// Queries DNS domain information from the LSA.
    /// </summary>
    /// <returns>A <see cref="LsaDnsDomainInformation"/> object containing the DNS domain information.</returns>
    public LsaDnsDomainInformation QueryDnsDomainInformation()
    {
        var status = NativeMethods.LsaQueryInformationPolicy(this.policyHandle, POLICY_INFORMATION_CLASS.PolicyDnsDomainInformation, out SafeLsaMemoryHandle buffer);
        Validator.AssertSuccess(status);

        try
        {
            // If the computer associated with the Policy object is not a member of a domain, all structure members except Name are NULL or zero.
            var domainInfoNative = buffer.MarshalAs<LsaDnsDomainInformationNative>();
            return new LsaDnsDomainInformation(domainInfoNative);
        }
        finally
        {
            buffer.Dispose();
        }
    }

    /// <summary>
    /// Queries machine account information from the LSA.
    /// </summary>
    /// <returns>A <see cref="SecurityIdentifier"/> object containing the machine account SID.</returns>
    public SecurityIdentifier QueryMachineAccountInformation()
    {
        var status = NativeMethods.LsaQueryInformationPolicy(this.policyHandle, POLICY_INFORMATION_CLASS.PolicyMachineAccountInformation, out SafeLsaMemoryHandle buffer);

        if (status == NtStatus.InvalidParameter)
        {
            // This information appears not to be readable on pre-Win10 systems
            return null;
        }

        // Continue with regular result validation
        Validator.AssertSuccess(status);

        try
        {
            var machineInfoNative = buffer.MarshalAs<LsaMachineAccountInformation>();
            return machineInfoNative.GetSid();
        }
        finally
        {
            buffer.Dispose();
        }
    }

    /// <summary>
    /// Queries account domain information from the LSA.
    /// </summary>
    /// <returns>A <see cref="LsaDomainInformation"/> object containing the account domain information.</returns>
    public LsaDomainInformation QueryAccountDomainInformation()
    {
        return this.QueryDomainInformation(POLICY_INFORMATION_CLASS.PolicyAccountDomainInformation);
    }

    /// <summary>
    /// Queries local account domain information from the LSA.
    /// </summary>
    /// <returns>A <see cref="LsaDomainInformation"/> object containing the local account domain information.</returns>
    public LsaDomainInformation QueryLocalAccountDomainInformation()
    {
        return this.QueryDomainInformation(POLICY_INFORMATION_CLASS.PolicyLocalAccountDomainInformation);
    }

    /// <summary>
    /// Sets DNS domain information in the LSA.
    /// </summary>
    /// <param name="newDomainInfo">The new DNS domain information to set.</param>
    public void SetDnsDomainInformation(LsaDnsDomainInformation newDomainInfo)
    {
        // Convert values to unmanaged types
        byte[] binarySid = newDomainInfo.Sid?.GetBinaryForm() ?? null;
        var pinnedSid = GCHandle.Alloc(binarySid, GCHandleType.Pinned);
        try
        {
            var nativeInfo = new LsaDnsDomainInformationNative()
            {
                DnsDomainName = new UnicodeString(newDomainInfo.DnsDomainName),
                DnsForestName = new UnicodeString(newDomainInfo.DnsForestName),
                Name = new UnicodeString(newDomainInfo.Name),
                DomainGuid = newDomainInfo.Guid.HasValue ? newDomainInfo.Guid.Value : Guid.Empty,
                DomainSid = pinnedSid.AddrOfPinnedObject()
            };

            var status = NativeMethods.LsaSetInformationPolicy(this.policyHandle, nativeInfo);
            Validator.AssertSuccess(status);
        }
        finally
        {
            pinnedSid.Free();
        }
    }

    /// <summary>
    /// Retrieves private data from the LSA.
    /// </summary>
    /// <param name="keyName">The name of the private data key to retrieve.</param>
    /// <returns>A byte array containing the private data.</returns>
    public byte[] RetrievePrivateData(string keyName)
    {
        if (string.IsNullOrWhiteSpace(keyName))
        {
            throw new ArgumentException("Key name cannot be null or whitespace.", nameof(keyName));
        }

        NtStatus status = NativeMethods.LsaRetrievePrivateData(this.policyHandle, keyName, out byte[] privateData);
        Validator.AssertSuccess(status);
        return privateData;
    }

    /// <summary>
    /// Retrieves the DPAPI backup keys from the LSA.
    /// </summary>
    /// <returns>An array of <see cref="DPAPIBackupKey"/> objects.</returns>
    /// <exception cref="CryptographicException">Thrown when the backup keys cannot be retrieved.</exception>
    public DPAPIBackupKey[] GetDPAPIBackupKeys()
    {
        byte[] rsaKeyIdBinary = this.RetrievePrivateData(DPAPIBackupKey.PreferredRSAKeyName);

        if (rsaKeyIdBinary == null || rsaKeyIdBinary.Length != Marshal.SizeOf<Guid>())
        {
            throw new CryptographicException("Failed reading the preferred RSA key ID. This typically happens on RODCs.");
        }

        byte[] legacyKeyIdBinary = this.RetrievePrivateData(DPAPIBackupKey.PreferredLegacyKeyName);

        if (legacyKeyIdBinary == null || legacyKeyIdBinary.Length != Marshal.SizeOf<Guid>())
        {
            throw new CryptographicException("Failed reading the preferred legacy key ID. This typically happens on RODCs.");
        }

        Guid rsaKeyId = new Guid(rsaKeyIdBinary);
        Guid legacyKeyId = new Guid(legacyKeyIdBinary);

        byte[] rsaKeyData = this.RetrievePrivateData(DPAPIBackupKey.GetKeyName(rsaKeyId));
        byte[] legacyKeyData = this.RetrievePrivateData(DPAPIBackupKey.GetKeyName(legacyKeyId));

        DPAPIBackupKey rsaKey = new DPAPIBackupKey(rsaKeyId, rsaKeyData);
        DPAPIBackupKey legacyKey = new DPAPIBackupKey(legacyKeyId, legacyKeyData);

        return [rsaKey, legacyKey];
    }

    /// <summary>
    /// Queries domain information from the LSA.
    /// </summary>
    private LsaDomainInformation QueryDomainInformation(POLICY_INFORMATION_CLASS informationClass)
    {
        var status = NativeMethods.LsaQueryInformationPolicy(this.policyHandle, informationClass, out SafeLsaMemoryHandle buffer);
        Validator.AssertSuccess(status);

        try
        {
            var domainInfoNative = buffer.MarshalAs<LsaDomainInformationNative>();
            return new LsaDomainInformation(domainInfoNative);
        }
        finally
        {
            buffer.Dispose();
        }
    }

    /// <summary>
    /// Disposes the object.
    /// </summary>
    public void Dispose()
    {
        this.policyHandle?.Dispose();
        this.policyHandle = null;
    }
}
