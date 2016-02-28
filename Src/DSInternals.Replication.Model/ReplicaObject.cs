namespace DSInternals.Replication.Model
{
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Data;
    using System;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Text;

    // TODO: IDisposable?
    public class ReplicaObject : DirectoryObject
    {
        private string distinguishedName;
        private Guid guid;
        private SecurityIdentifier sid;

        public ReplicaObject(String distinguishedName, Guid objectGuid, SecurityIdentifier objectSid, ReplicaAttributeCollection attributes)
        {
            this.guid = objectGuid;
            this.distinguishedName = distinguishedName;
            this.sid = objectSid;
            this.Attributes = attributes;
        }
        // TODO: ISchema
        public BasicSchema Schema
        {
            get;
            set;
        }
        public override string DistinguishedName
        {
            get
            {
                return this.distinguishedName;
            }
        }
        public override Guid Guid
        {
            get
            {
                return this.guid;
            }
        }

        public override SecurityIdentifier Sid
        {
            get
            {
                return this.sid;
            }
        }

        // TODO: Read only collection
        public ReplicaAttributeCollection Attributes
        {
            get;
            private set;
        }
        // TODO: Remove hasValue returns
        protected bool HasAttribute(int attributeId)
        {
            return this.Attributes.ContainsKey(attributeId);
        }
        // TODO: Read multiple values
        protected bool ReadAttribute(int attributeId, out byte[][] values)
        {
            ReplicaAttribute attribute;
            bool hasAttribute = this.Attributes.TryGetValue(attributeId, out attribute);
            if (hasAttribute)
            {
                bool hasValue = attribute.Values != null && attribute.Values.Length > 0;
                if (hasValue)
                {
                    values = attribute.Values;
                    return true;
                }
            }
            values = null;
            return false;
        }

        protected bool ReadAttribute(int attributeId, out byte[] value)
        {
            return this.ReadAttribute(attributeId, out value, 0);
        }

        protected bool ReadAttribute(int attributeId, out byte[] value, int valueIndex)
        {
            byte[][] values;
            bool hasValue = this.ReadAttribute(attributeId, out values);
            if(hasValue && values.Length > valueIndex)
            {
                value = values[valueIndex];
                return true;
            }
            value = null;
            return false;
        }
        protected bool ReadAttribute(int attributeId, out int? value)
        {
            byte[] binaryValue;
            bool hasValue = this.ReadAttribute(attributeId, out binaryValue);
            value = hasValue ? BitConverter.ToInt32(binaryValue, 0) : (int?)null;
            return hasValue;
        }

        protected bool ReadAttribute(int attributeId, out long? value)
        {
            byte[] binaryValue;
            bool hasValue = this.ReadAttribute(attributeId, out binaryValue);
            value = hasValue ? BitConverter.ToInt64(binaryValue, 0) : (long?)null;
            return hasValue;
        }

        protected bool ReadAttribute(int attributeId, out string value)
        {
            byte[] binaryValue;
            bool hasValue = this.ReadAttribute(attributeId, out binaryValue);
            value = hasValue ? Encoding.Unicode.GetString(binaryValue) : null;
            return hasValue;
        }
        protected bool ReadAttribute(int attributeId, out SecurityIdentifier value)
        {
            byte[] binaryValue;
            bool hasValue = this.ReadAttribute(attributeId, out binaryValue);
            value = hasValue ? new SecurityIdentifier(binaryValue, 0) : null;
            return hasValue;
        }
        protected bool ReadAttribute(int attributeId, out SamAccountType? value)
        {
            int? numericValue;
            bool hasValue = this.ReadAttribute(attributeId, out numericValue);
            value = hasValue ? (SamAccountType)numericValue.Value : (SamAccountType?)null;
            return hasValue;
        }
        protected bool ReadAttribute(int attributeId, out bool value)
        {
            int? numericValue;
            bool hasValue = this.ReadAttribute(attributeId, out numericValue);
            value = hasValue ? numericValue.Value != 0 : false;
            return hasValue;
        }

        public override bool HasAttribute(string name)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            return this.HasAttribute(attributeId);
        }

        public override void ReadAttribute(string name, out byte[] value)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            this.ReadAttribute(attributeId, out value);
        }

        public override void ReadAttribute(string name, out byte[][] value)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            this.ReadAttribute(attributeId, out value);
        }

        public override void ReadAttribute(string name, out int? value)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            this.ReadAttribute(attributeId, out value);
        }

        public override void ReadAttribute(string name, out long? value)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            this.ReadAttribute(attributeId, out value);
        }

        public override void ReadAttribute(string name, out string value)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            this.ReadAttribute(attributeId, out value);
        }

        public override void ReadAttribute(string name, out RawSecurityDescriptor value)
        {
            // TODO: Implement SD retrieval
            value = null;
        }

        protected override bool HasBigEndianRid
        {
            get
            {
                return false;
            }
        }
    }
}