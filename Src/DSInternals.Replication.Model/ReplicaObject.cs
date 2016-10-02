namespace DSInternals.Replication.Model
{
    using DSInternals.Common.Data;
    using System;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Text;

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

        protected bool HasAttribute(int attributeId)
        {
            return this.Attributes.ContainsKey(attributeId);
        }

        protected void ReadAttribute(int attributeId, out byte[][] values)
        {
            values = null;
            ReplicaAttribute attribute;
            bool hasAttribute = this.Attributes.TryGetValue(attributeId, out attribute);
            if (hasAttribute)
            {
                bool hasValue = attribute.Values != null && attribute.Values.Length > 0;
                if (hasValue)
                {
                    values = attribute.Values;
                }
            }
        }

        protected void ReadAttribute(int attributeId, out byte[] value)
        {
            this.ReadAttribute(attributeId, out value, 0);
        }

        protected void ReadAttribute(int attributeId, out byte[] value, int valueIndex)
        {
            byte[][] values;
            this.ReadAttribute(attributeId, out values);
            bool containsValue = values != null && values.Length > valueIndex;
            value = containsValue ? values[valueIndex] : null;
        }

        protected void ReadAttribute(int attributeId, out int? value)
        {
            byte[] binaryValue;
            this.ReadAttribute(attributeId, out binaryValue);
            value = (binaryValue != null) ? BitConverter.ToInt32(binaryValue, 0) : (int?)null;
        }

        protected void ReadAttribute(int attributeId, out long? value)
        {
            byte[] binaryValue;
            this.ReadAttribute(attributeId, out binaryValue);
            value = (binaryValue != null) ? BitConverter.ToInt64(binaryValue, 0) : (long?)null;
        }

        protected void ReadAttribute(int attributeId, out string value)
        {
            byte[] binaryValue;
            this.ReadAttribute(attributeId, out binaryValue);
            value = (binaryValue != null) ? Encoding.Unicode.GetString(binaryValue) : null;
        }

        protected void ReadAttribute(int attributeId, out DistinguishedName value)
        {
            // TODO: Implement
            throw new NotImplementedException();
        }

        protected void ReadAttribute(int attributeId, out SecurityIdentifier value)
        {
            byte[] binaryValue;
            this.ReadAttribute(attributeId, out binaryValue);
            value = (binaryValue != null) ? new SecurityIdentifier(binaryValue, 0) : null;
        }
        protected void ReadAttribute(int attributeId, out SamAccountType? value)
        {
            int? numericValue;
            this.ReadAttribute(attributeId, out numericValue);
            value = numericValue.HasValue ? (SamAccountType)numericValue.Value : (SamAccountType?)null;
        }
        protected void ReadAttribute(int attributeId, out bool value)
        {
            int? numericValue;
            this.ReadAttribute(attributeId, out numericValue);
            value = numericValue.HasValue ? numericValue.Value != 0 : false;
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

        public override void ReadAttribute(string name, out DistinguishedName value)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            this.ReadAttribute(attributeId, out value);
        }

        public override void ReadAttribute(string name, out RawSecurityDescriptor value)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            this.ReadAttribute(attributeId, out value);
        }

        protected void ReadAttribute(int attributeId, out RawSecurityDescriptor value)
        {
            byte[] binarySecurityDescriptor;
            this.ReadAttribute(attributeId, out binarySecurityDescriptor);
            value = (binarySecurityDescriptor != null) ? new RawSecurityDescriptor(binarySecurityDescriptor, 0) : null;
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