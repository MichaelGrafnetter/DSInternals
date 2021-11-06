using DSInternals.Common;
using DSInternals.Common.Data;
using System;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace DSInternals.Replication.Model
{
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

        public void LoadLinkedValues(ReplicatedLinkedValueCollection linkedValueCollection)
        {
            var objectAttributes = linkedValueCollection.Get(this.Guid);

            // Only continue if the linked values contain attributes of this AD object
            if(objectAttributes != null)
            {
                foreach (var attribute in objectAttributes)
                {
                    this.Attributes.Add(attribute);
                }
            }
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

        protected void ReadAttribute(int attributeId, out string[] values)
        {
            values = null;
            byte[][] binaryValues;
            this.ReadAttribute(attributeId, out binaryValues);
            if(binaryValues != null)
            {
                values = binaryValues.Select(item => Encoding.Unicode.GetString(item)).ToArray();
            }
        }

        protected void ReadAttribute(int attributeId, out DistinguishedName value)
        {
            // TODO: Implement support for DS-DN syntax.
            // Hint: https://github.com/MichaelGrafnetter/DSInternals/issues/49
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

        public override void ReadAttribute(string name, out string[] values)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            this.ReadAttribute(attributeId, out values);
        }

        public override void ReadAttribute(string name, out DistinguishedName value)
        {
            int attributeId = this.Schema.FindAttributeId(name);
            this.ReadAttribute(attributeId, out value);
        }

        public override void ReadLinkedValues(string attributeName, out byte[][] values)
        {
            // The linked values have already been merged with regular attributes using LoadLinkedValues
            // TODO: We currently only support DN-Binary linked values.
            // TODO: Check if the attribute exists
            byte[][] rawValues;
            this.ReadAttribute(attributeName, out rawValues);
            values = (rawValues != null) ? rawValues.Select(rawValue => ParseDNBinary(rawValue)).ToArray() : null;
        }

        protected override bool HasBigEndianRid
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Parses the binary data as SYNTAX_DISTNAME_BINARY.
        /// </summary>
        /// <param name="blob">SYNTAX_DISTNAME_BINARY structure</param>
        /// <returns>Binary value</returns>
        /// <see>https://msdn.microsoft.com/en-us/library/cc228431.aspx</see>
        protected static byte[] ParseDNBinary(byte[] blob)
        {
            // Read structLen (4 bytes): The length of the structure, in bytes, up to and including the field StringName.
            int structLen = BitConverter.ToInt32(blob, 0);

            // Skip Padding (variable): The padding (bytes with value zero) to align the field dataLen at a double word boundary.
            int structLengthWithPadding = structLen;
            while(structLengthWithPadding % sizeof(int) != 0)
            {
                structLengthWithPadding++;
            }

            // Read dataLen (4 bytes): The length of the remaining structure, including this field, in bytes.
            int dataLen = BitConverter.ToInt32(blob, structLengthWithPadding);
            int dataOffset = structLengthWithPadding + sizeof(int);

            // Read byteVal(variable): An array of bytes.
            byte[] value = blob.Cut(dataOffset);

            // TODO: Validate the data length
            // Return only the binary data, without the DN.
            return value;
        }
    }
}
