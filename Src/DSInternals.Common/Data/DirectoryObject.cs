namespace DSInternals.Common.Data
{
    using DSInternals.Common.Cryptography;
    using System;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Text;

    public abstract class DirectoryObject
    {
        protected abstract bool HasBigEndianRid
        {
            get;
        }

        public abstract bool HasAttribute(string name);
        public abstract void ReadAttribute(string name, out byte[] value);
        public abstract void ReadAttribute(string name, out byte[][] value);

        public void ReadAttribute(string name, out Guid? value)
        {
            byte[] binaryValue;
            this.ReadAttribute(name, out binaryValue);
            value = (binaryValue != null) ? new Guid(binaryValue) : (Guid?)null;
        }
        public void ReadAttribute(string name, out bool value)
        {
            int? numericValue;
            this.ReadAttribute(name, out numericValue);
            value = numericValue.HasValue ? (numericValue.Value != 0) : false;
        }
        public abstract void ReadAttribute(string name, out int? value);
        public abstract void ReadAttribute(string name, out long? value);
        public abstract void ReadAttribute(string name, out string value);
        public abstract void ReadAttribute(string name, out string[] values);

        public virtual void ReadAttribute(string name, out RawSecurityDescriptor value)
        {
            byte[] binarySecurityDescriptor;
            this.ReadAttribute(name, out binarySecurityDescriptor);
            value = (binarySecurityDescriptor != null) ? new RawSecurityDescriptor(binarySecurityDescriptor, 0) : null;
        }

        public abstract void ReadLinkedValues(string attributeName, out byte[][] values);

        public abstract string DistinguishedName
        {
            get;
        }

        public abstract Guid Guid
        {
            get;
        }

        public abstract SecurityIdentifier Sid
        {
            get;
        }

        public void ReadAttribute(string name, out SecurityIdentifier value)
        {
            byte[] binarySid;
            this.ReadAttribute(name, out binarySid);
            value = binarySid.ToSecurityIdentifier(this.HasBigEndianRid);
        }

        public abstract void ReadAttribute(string name, out DistinguishedName value);

        public void ReadAttribute(string name, out SecurityIdentifier[] value)
        {
            value = null;
            byte[][] binarySids;
            // TODO: Always big endian?
            this.ReadAttribute(name, out binarySids);
            if(binarySids != null)
            {
                value = binarySids.Select(binarySid => binarySid.ToSecurityIdentifier(this.HasBigEndianRid)).ToArray();
            }
        }

        public void ReadAttribute(string name, out SamAccountType? value)
        {
            int? numericValue;
            this.ReadAttribute(name, out numericValue);
            value = (SamAccountType?)numericValue;
        }

        public void ReadAttribute(string name, out DateTime? value)
        {
            value = null;
            long? timestamp;
            this.ReadAttribute(name, out timestamp);
            if(timestamp.HasValue && timestamp.Value > 0)
            {
                value = DateTime.FromFileTime(timestamp.Value); 
            }
        }

        // based on https://github.com/MichaelGrafnetter/DSInternals/issues/49
        public string ParseDSDN(byte[] binaryVal)
        {
            if (binaryVal != null && binaryVal.Length > 0)
            {
                int curOff = 4 + 16 + 28;
                uint curNameLen = BitConverter.ToUInt32(binaryVal.Skip(curOff).Take(4).ToArray(), 0);
                curOff += 4;
                return Encoding.Unicode.GetString(binaryVal.Skip(curOff).Take((int)(curNameLen * 2)).ToArray());
            }

            return null;
        }

        public bool IsDeleted
        {
            get
            {
                bool result;
                this.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out result);
                return result;
            }
        }

        public bool HasOtherCreds
        {
            get
            {
                byte[] encryptedSupplementalCredentials, roamingTimeStamp;
                byte[][] keyCredentialBlobs;
                this.ReadAttribute(CommonDirectoryAttributes.SupplementalCredentials, out encryptedSupplementalCredentials);
                this.ReadAttribute(CommonDirectoryAttributes.PKIRoamingTimeStamp, out roamingTimeStamp);
                this.ReadLinkedValues(CommonDirectoryAttributes.KeyCredentialLink, out keyCredentialBlobs);
                return (encryptedSupplementalCredentials != null || roamingTimeStamp != null || keyCredentialBlobs != null);
            }
        }

        public bool HasNTHash
        {
            get
            {
                byte[] nt, ntHistory;
                this.ReadAttribute(CommonDirectoryAttributes.NTHash, out nt);
                this.ReadAttribute(CommonDirectoryAttributes.NTHashHistory, out ntHistory);
                return (nt != null || ntHistory != null);
            }
        }

        public bool HasLMHash
        {
            get
            {
                byte[] lm, lmHistory;
                this.ReadAttribute(CommonDirectoryAttributes.LMHash, out lm);
                this.ReadAttribute(CommonDirectoryAttributes.LMHashHistory, out lmHistory);
                return (lm != null || lmHistory != null);
            }
        }

        public bool HasLAPS
        {
            get
            {
                byte[] admPwd;
                this.ReadAttribute(CommonDirectoryAttributes.LAPSPassword, out admPwd);
                return (admPwd != null);
            }
        }

        public bool IsBitlocker
        {
            get
            {
                // make better
                return this.HasAttribute(CommonDirectoryAttributes.FVERecoveryGuid);
            }
        }
        /*
        public bool IsTPM
        {
            get
            {
                // make better
                return this.HasAttribute(CommonDirectoryAttributes.TPMOwnerInfo);
            }
        }
        */
        public bool IsAccount
        {
            get
            {
                SamAccountType? accountType;
                this.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out accountType);
                switch (accountType)
                {
                    case SamAccountType.User:
                    case SamAccountType.Computer:
                    case SamAccountType.Trust:
                        return true;
                    default:
                        return false;
                }
            }
        }
        // TODO: No schema exception?
        public bool IsSecurityPrincipal
        {
            get
            {
                SamAccountType? accountType;
                this.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out accountType);
                switch (accountType)
                {
                    case SamAccountType.User:
                    case SamAccountType.Computer:
                    case SamAccountType.Trust:
                    case SamAccountType.SecurityGroup:
                    case SamAccountType.Alias:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsOtherAccount
        {
            get
            {
                SamAccountType? accountType;
                this.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out accountType);
                switch (accountType)
                {
                    case SamAccountType.Domain:
                    case SamAccountType.SecurityGroup:
                    case SamAccountType.DistributuionGroup:
                    case SamAccountType.Alias:
                    case SamAccountType.NonSecurityAlias:
                    case SamAccountType.ApplicationBasicGroup:
                    case SamAccountType.ApplicationQueryGroup:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsUserAccount
        {
            get
            {
                SamAccountType? accountType;
                this.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out accountType);
                switch (accountType)
                {
                    case SamAccountType.User:
                    case SamAccountType.Trust:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsComputerAccount
        {
            get
            {
                SamAccountType? accountType;
                this.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out accountType);
                switch (accountType)
                {
                    case SamAccountType.Computer:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public InstanceType? InstanceType
        {
            get
            {
                int? result;
                this.ReadAttribute(CommonDirectoryAttributes.InstanceType, out result);
                return (InstanceType?)result;
            }
        }

        public bool IsWritable
        {
            get
            {
                var instanceType = this.InstanceType;
                return instanceType.HasValue && instanceType.Value.HasFlag(DSInternals.Common.Data.InstanceType.Writable);
            }
        }
    }
}
