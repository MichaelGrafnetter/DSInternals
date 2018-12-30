namespace DSInternals.SAM
{
    using DSInternals.Common;
    using DSInternals.Common.Data;
    using DSInternals.Common.Interop;
    using DSInternals.SAM.Interop;
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Principal;

    public class LsaPolicy : IDisposable
    {
        private SafeLsaPolicyHandle policyHandle;

        public LsaPolicy(LsaPolicyAccessMask accessMask) : this(null, accessMask) { }

        public LsaPolicy(string systemName, LsaPolicyAccessMask accessMask)
        {
            var status = NativeMethods.LsaOpenPolicy(systemName, accessMask, out this.policyHandle);
            Validator.AssertSuccess(status);
        }

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

        public LsaDomainInformation QueryAccountDomainInformation()
        {
            return this.QueryDomainInformation(LsaPolicyInformationClass.AccountDomainInformation);
        }

        public LsaDomainInformation QueryLocalAccountDomainInformation()
        {
            return this.QueryDomainInformation(LsaPolicyInformationClass.LocalAccountDomainInformation);
        }

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

        public byte[] RetrievePrivateData(string keyName)
        {
            Validator.AssertNotNullOrWhiteSpace(keyName, "keyName");
            byte[] privateData;
            NtStatus status = NativeMethods.LsaRetrievePrivateData(this.policyHandle, keyName, out privateData);
            Validator.AssertSuccess(status);
            return privateData;
        }

        public DPAPIBackupKey[] GetDPAPIBackupKeys()
        {
            Guid rsaKeyId = new Guid(this.RetrievePrivateData(DPAPIBackupKey.PreferredRSAKeyName));
            Guid legacyKeyId = new Guid(this.RetrievePrivateData(DPAPIBackupKey.PreferredLegacyKeyName));

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

        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion
    }
}