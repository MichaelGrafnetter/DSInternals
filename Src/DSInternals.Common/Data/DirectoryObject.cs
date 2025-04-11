namespace DSInternals.Common.Data
{
    using System;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Security.Principal;

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
        public abstract void ReadAttribute(string name, out string value, bool unicode = true);
        public abstract void ReadAttribute(string name, out string[] values, bool unicode = true);

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

        public void ReadAttribute(string name, out DateTime? value, bool asGeneralizedTime)
        {
            value = null;
            long? timestamp;
            this.ReadAttribute(name, out timestamp);
            if(timestamp.HasValue && timestamp.Value > 0)
            {
                value = asGeneralizedTime ? timestamp.Value.FromGeneralizedTime() : DateTime.FromFileTime(timestamp.Value); 
            }
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
