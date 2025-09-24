using DSInternals.Common;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;
using System.Security.Principal;
using System.Text;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// Represents a directory object retrieved from a domain controller using the replication protocol.
    /// </summary>
    public class ReplicaObject : DirectoryObject
    {
        private string distinguishedName;
        private Guid guid;
        private SecurityIdentifier sid;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplicaObject"/> class.
        /// </summary>
        /// <param name="distinguishedName">The distinguished name of the object.</param>
        /// <param name="objectGuid">The globally unique identifier (GUID) of the object.</param>
        /// <param name="objectSid">The security identifier (SID) of the object.</param>
        /// <param name="attributes">The attributes of the object.</param>
        /// <param name="schema">The Active Directory schema.</param>
        public ReplicaObject(String distinguishedName, Guid objectGuid, SecurityIdentifier objectSid, ReplicaAttributeCollection attributes, BaseSchema schema)
        {
            this.guid = objectGuid;
            this.distinguishedName = distinguishedName;
            this.sid = objectSid;
            this.Attributes = attributes;
            this.Schema = schema;
        }

        /// <summary>
        /// The Active Directory schema associated with the object.
        /// </summary>
        public BaseSchema Schema
        {
            get;
            private set;
        }

        /// <summary>
        /// The distinguished name of the object.
        /// </summary>
        public override string DistinguishedName => this.distinguishedName;

        /// <summary>
        /// The globally unique identifier (GUID) of the object.
        /// </summary>
        public override Guid Guid => this.guid;

        /// <summary>
        /// The security identifier (SID) of the object.
        /// </summary>
        public override SecurityIdentifier Sid => this.sid;

        // TODO: Read only collection
        /// <summary>
        /// The attributes of the object.
        /// </summary>
        public ReplicaAttributeCollection Attributes
        {
            get;
            private set;
        }

        /// <summary>
        /// Merges linked values from the specified linked value collection into the object's attributes.
        /// </summary>
        /// <param name="linkedValueCollection">The linked value collection.</param>
        public void LoadLinkedValues(ReplicatedLinkedValueCollection linkedValueCollection)
        {
            var objectAttributes = linkedValueCollection.Get(this.Guid);

            // Only continue if the linked values contain attributes of this AD object
            if (objectAttributes != null)
            {
                foreach (var attribute in objectAttributes)
                {
                    this.Attributes.Add(attribute);
                }
            }
        }

        /// <summary>
        /// Determines whether the object has the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to check.</param>
        /// <returns>True if the object has the specified attribute; otherwise, false.</returns>
        protected bool HasAttribute(AttributeType attributeId)
        {
            return this.Attributes.ContainsKey(attributeId);
        }

        /// <summary>
        /// Reads all values of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="values">The values of the attribute.</param>
        protected void ReadAttribute(AttributeType attributeId, out byte[][] values)
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

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The first value of the attribute.</param>
        protected void ReadAttribute(AttributeType attributeId, out byte[] value)
        {
            this.ReadAttribute(attributeId, out value, 0);
        }

        /// <summary>
        /// Reads the specified value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="valueIndex">The index of the value to read.</param>
        protected void ReadAttribute(AttributeType attributeId, out byte[] value, int valueIndex)
        {
            byte[][] values;
            this.ReadAttribute(attributeId, out values);
            bool containsValue = values != null && values.Length > valueIndex;
            value = containsValue ? values[valueIndex] : null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        protected void ReadAttribute(AttributeType attributeId, out int? value)
        {
            byte[] binaryValue;
            this.ReadAttribute(attributeId, out binaryValue);
            value = (binaryValue != null) ? BitConverter.ToInt32(binaryValue, 0) : (int?)null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        protected void ReadAttribute(AttributeType attributeId, out long? value)
        {
            byte[] binaryValue;
            this.ReadAttribute(attributeId, out binaryValue);
            value = (binaryValue != null) ? BitConverter.ToInt64(binaryValue, 0) : (long?)null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="unicode">Indicates whether to use Unicode encoding.</param>
        protected void ReadAttribute(AttributeType attributeId, out string value, bool unicode = true)
        {
            var encoding = unicode ? Encoding.Unicode : Encoding.ASCII;
            byte[] binaryValue;
            this.ReadAttribute(attributeId, out binaryValue);
            value = (binaryValue != null) ? encoding.GetString(binaryValue) : null;
        }

        /// <summary>
        /// Reads all values of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="values">The values of the attribute.</param>
        /// <param name="unicode">Indicates whether to use Unicode encoding.</param>
        protected void ReadAttribute(AttributeType attributeId, out string[] values, bool unicode = true)
        {
            var encoding = unicode ? Encoding.Unicode : Encoding.ASCII;
            values = null;
            byte[][] binaryValues;
            this.ReadAttribute(attributeId, out binaryValues);
            if (binaryValues != null)
            {
                values = binaryValues.Select(item => encoding.GetString(item)).ToArray();
            }
        }

        // TODO: Add support for multi-value and linked value attributes.
        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        protected void ReadAttribute(AttributeType attributeId, out DistinguishedName value)
        {
            value = null;
            this.ReadAttribute(attributeId, out byte[] binaryValue);

            if (binaryValue != null && binaryValue.Length > 0)
            {
                // The attribute uses the DS-DN syntax.
                var dsName = DSName.Parse(binaryValue);

                if (dsName.DistinguishedName != null)
                {
                    value = new DistinguishedName(dsName.DistinguishedName);
                }
            }
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        protected void ReadAttribute(AttributeType attributeId, out SecurityIdentifier value)
        {
            byte[] binaryValue;
            this.ReadAttribute(attributeId, out binaryValue);
            value = (binaryValue != null) ? new SecurityIdentifier(binaryValue, 0) : null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        protected void ReadAttribute(AttributeType attributeId, out SamAccountType? value)
        {
            int? numericValue;
            this.ReadAttribute(attributeId, out numericValue);
            value = numericValue.HasValue ? (SamAccountType)numericValue.Value : (SamAccountType?)null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        protected void ReadAttribute(AttributeType attributeId, out bool value)
        {
            int? numericValue;
            this.ReadAttribute(attributeId, out numericValue);
            value = numericValue.HasValue ? numericValue.Value != 0 : false;
        }

        /// <summary>
        /// Determines whether the object has the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to check.</param>
        /// <returns>True if the attribute exists; otherwise, false.</returns>
        public override bool HasAttribute(string name)
        {
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            return attributeId.HasValue && this.HasAttribute(attributeId.Value);
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public override void ReadAttribute(string name, out byte[] value)
        {
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out value);
            }
            else
            {
                // The schema does not even contain this attribute.
                value = null;
            }
        }

        /// <summary>
        /// Reads all values of the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">The values of the attribute.</param>
        public override void ReadAttribute(string name, out byte[][] value)
        {
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out value);
            }
            else
            {
                // The schema does not even contain this attribute.
                value = null;
            }
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public override void ReadAttribute(string name, out int? value)
        {
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out value);
            }
            else
            {
                // The schema does not even contain this attribute.
                value = null;
            }
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public override void ReadAttribute(string name, out long? value)
        {
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out value);
            }
            else
            {
                // The schema does not even contain this attribute.
                value = null;
            }
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="unicode">Whether to read the value as a Unicode string.</param>
        public override void ReadAttribute(string name, out string? value, bool unicode = true)
        {
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out value, unicode);
            }
            else
            {
                // The schema does not even contain this attribute.
                value = null;
            }
        }

        /// <summary>
        /// Reads all values of the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="values">The values of the attribute.</param>
        /// <param name="unicode">Whether to read the values as Unicode strings.</param>
        public override void ReadAttribute(string name, out string[]? values, bool unicode = true)
        {
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out values, unicode);
            }
            else
            {
                // The schema does not even contain this attribute.
                values = null;
            }
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public override void ReadAttribute(string name, out DistinguishedName? value)
        {
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out value);
            }
            else
            {
                // The schema does not even contain this attribute.
                value = null;
            }
        }

        /// <summary>
        /// Reads all values of the specified attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute to read.</param>
        /// <param name="values">The values of the attribute.</param>
        public override void ReadLinkedValues(string attributeName, out byte[][]? values)
        {
            // The linked values have already been merged with regular attributes using LoadLinkedValues
            // TODO: We currently only support DN-Binary linked values.
            // TODO: Check if the attribute exists
            byte[][] rawValues;
            this.ReadAttribute(attributeName, out rawValues);
            values = (rawValues != null) ? rawValues.Select(rawValue => ParseDNBinary(rawValue)).ToArray() : null;
        }

        /// <summary>
        /// Indicates whether the RID in the object's SID is stored in big-endian format.
        /// </summary>
        protected override bool HasBigEndianRid => false;

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
            while (structLengthWithPadding % sizeof(int) != 0)
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
