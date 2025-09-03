namespace DSInternals.SAM
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Security.Principal;
    using DSInternals.Common;
    using DSInternals.Common.Data;
    using DSInternals.Common.Interop;
    using DSInternals.SAM.Interop;

    /// <summary>
    /// Represents a Local Security Authority (LSA) policy handle used for querying system security information.
    /// </summary>
    public class LsaPolicy : IDisposable
    {
        private SafeLsaPolicyHandle policyHandle;

        /// <summary>
        /// Initializes a new instance of the LsaPolicy class on the local system with the specified access rights.
        /// </summary>
        /// <param name="accessMask">The access rights to request on the LSA policy.</param>
        public LsaPolicy(LsaPolicyAccessMask accessMask) : this(null, accessMask) { }

        public LsaPolicy(string systemName, LsaPolicyAccessMask accessMask)
        {
            var status = NativeMethods.LsaOpenPolicy(systemName, accessMask, out this.policyHandle);
            Validator.AssertSuccess(status);
        }

        /// <summary>
        /// Queries DNS domain information from the Local Security Authority.
        /// </summary>
        /// <returns>A LsaDnsDomainInformation object containing the DNS domain information.</returns>
        public LsaDnsDomainInformation QueryDnsDomainInformation()
        {
            IntPtr buffer;
            var status = NativeMethods.LsaQueryInformationPolicy(this.policyHandle, LsaPolicyInformationClass.DnsDomainInformation, out buffer);
            Validator.AssertSuccess(status);

            try
            {
                // If the computer associated with the Policy object is not a member of a domain, all structure members except Name are NULL or zero.
                var domainInfoNative = Marshal.PtrToStructure<LsaDnsDomainInformationNative>(buffer);
                return new LsaDnsDomainInformation(domainInfoNative);
            }
            finally
            {
                // Ignore any errors during memory deallocation.
                status = NativeMethods.LsaFreeMemory(buffer);
            }
        }

        /// <summary>
        /// Queries machine account information from the Local Security Authority.
        /// </summary>
        /// <returns>The security identifier (SID) of the machine account, or null if not available.</returns>
        public SecurityIdentifier QueryMachineAccountInformation()
        {
            IntPtr buffer;
            var status = NativeMethods.LsaQueryInformationPolicy(this.policyHandle, LsaPolicyInformationClass.MachineAccountInformation, out buffer);

            if (status == NtStatus.InvalidParameter)
            {
                // This information appears not to be readable on pre-Win10 systems
                return null;
            }

            // Continue with regular result validation
            Validator.AssertSuccess(status);

            try
            {
                var machineInfoNative = Marshal.PtrToStructure<LsaMachineAccountInformation>(buffer);
                return machineInfoNative.Sid != IntPtr.Zero ? new SecurityIdentifier(machineInfoNative.Sid) : null;
            }
            finally
            {
                // Ignore any errors during memory deallocation.
                status = NativeMethods.LsaFreeMemory(buffer);
            }
        }

        /// <summary>
        /// QueryAccountDomainInformation implementation.
        /// </summary>
        public LsaDomainInformation QueryAccountDomainInformation()
        {
            return this.QueryDomainInformation(LsaPolicyInformationClass.AccountDomainInformation);
        }

        /// <summary>
        /// QueryLocalAccountDomainInformation implementation.
        /// </summary>
        public LsaDomainInformation QueryLocalAccountDomainInformation()
        {
            return this.QueryDomainInformation(LsaPolicyInformationClass.LocalAccountDomainInformation);
        }

        /// <summary>
        /// SetDnsDomainInformation implementation.
        /// </summary>
        public void SetDnsDomainInformation(LsaDnsDomainInformation newDomainInfo)
        {
            // TODO: Validation
            Validator.AssertNotNull(newDomainInfo, "newDomainInfo");

            // Convert values to unmanaged types
            byte[] binarySid = newDomainInfo.Sid != null ? newDomainInfo.Sid.GetBinaryForm() : null;
            var pinnedSid = GCHandle.Alloc(binarySid, GCHandleType.Pinned);
            try
            {
                var nativeInfo = new LsaDnsDomainInformationNative()
                {
                    DnsDomainName = new UnicodeString(newDomainInfo.DnsDomainName),
                    DnsForestName = new UnicodeString(newDomainInfo.DnsForestName),
                    Name = new UnicodeString(newDomainInfo.Name),
                    DomainGuid = newDomainInfo.Guid.HasValue ? newDomainInfo.Guid.Value : Guid.Empty,
                    Sid = pinnedSid.AddrOfPinnedObject()
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
        /// RetrievePrivateData implementation.
        /// </summary>
        public byte[] RetrievePrivateData(string keyName)
        {
            Validator.AssertNotNullOrWhiteSpace(keyName, "keyName");
            byte[] privateData;
            NtStatus status = NativeMethods.LsaRetrievePrivateData(this.policyHandle, keyName, out privateData);
            Validator.AssertSuccess(status);
            return privateData;
        }

        /// <summary>
        /// GetDPAPIBackupKeys implementation.
        /// </summary>
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

            return new DPAPIBackupKey[] { rsaKey, legacyKey };
        }

        private LsaDomainInformation QueryDomainInformation(LsaPolicyInformationClass informationClass)
        {
            IntPtr buffer;
            var status = NativeMethods.LsaQueryInformationPolicy(this.policyHandle, informationClass, out buffer);
            Validator.AssertSuccess(status);

            try
            {
                var domainInfoNative = Marshal.PtrToStructure<LsaDomainInformationNative>(buffer);
                return new LsaDomainInformation(domainInfoNative);
            }
            finally
            {
                // Ignore any errors during memory deallocation.
                status = NativeMethods.LsaFreeMemory(buffer);
            }
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.policyHandle != null)
            {
                // Dispose managed state
                this.policyHandle.Dispose();
                this.policyHandle = null;
            }
        }

        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion
    }
}
