namespace DSInternals.Common.Data
{
    using System;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using DSInternals.Common.Kerberos;
    using DSInternals.Common.Schema;

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
            this.ReadAttribute(name, out byte[] binaryValue);
            value = (binaryValue != null) ? new Guid(binaryValue) : (Guid?)null;
        }
        public void ReadAttribute(string name, out bool value)
        {
            this.ReadAttribute(name, out int? numericValue);
            value = numericValue.HasValue ? (numericValue.Value != 0) : false;
        }
        public abstract void ReadAttribute(string name, out int? value);
        public abstract void ReadAttribute(string name, out long? value);
        public abstract void ReadAttribute(string name, out string value, bool unicode = true);
        public abstract void ReadAttribute(string name, out string[] values, bool unicode = true);

        public virtual void ReadAttribute(string name, out RawSecurityDescriptor value)
        {
            this.ReadAttribute(name, out byte[] binarySecurityDescriptor);
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
            this.ReadAttribute(name, out byte[] binarySid);
            value = binarySid.ToSecurityIdentifier(this.HasBigEndianRid);
        }

        public abstract void ReadAttribute(string name, out DistinguishedName value);

        public void ReadAttribute(string name, out SecurityIdentifier[] value)
        {
            value = null;
            // TODO: Always big endian?
            this.ReadAttribute(name, out byte[][] binarySids);
            if (binarySids != null)
            {
                value = binarySids.Select(binarySid => binarySid.ToSecurityIdentifier(this.HasBigEndianRid)).ToArray();
            }
        }

        public void ReadAttribute(string name, out SamAccountType? value)
        {
            this.ReadAttribute(name, out int? numericValue);
            value = (SamAccountType?)numericValue;
        }

        public void ReadAttribute(string name, out TrustDirection? value)
        {
            this.ReadAttribute(name, out int? numericValue);
            value = (TrustDirection?)numericValue;
        }

        public void ReadAttribute(string name, out TrustAttributes? value)
        {
            this.ReadAttribute(name, out int? numericValue);
            value = (TrustAttributes?)numericValue;
        }

        public void ReadAttribute(string name, out TrustType? value)
        {
            this.ReadAttribute(name, out int? numericValue);
            value = (TrustType?)numericValue;
        }

        public void ReadAttribute(string name, out DateTime? value, bool asGeneralizedTime)
        {
            value = null;
            this.ReadAttribute(name, out long? timestamp);
            if(timestamp.HasValue && timestamp.Value > 0)
            {
                value = asGeneralizedTime ? timestamp.Value.FromGeneralizedTime() : DateTime.FromFileTime(timestamp.Value); 
            }
        }

        /// <summary>
        /// Indicates whether this object has been marked for deletion. After the tombstone period has expired, it will be removed from the system.
        /// </summary>
        public virtual bool IsDeleted
        {
            get
            {
                this.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out bool result);
                return result;
            }
        }

        public InstanceType? InstanceType
        {
            get
            {
                this.ReadAttribute(CommonDirectoryAttributes.InstanceType, out int? result);
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
