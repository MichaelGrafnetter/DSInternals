using System.Security.Principal;
using System.Text;
using DSInternals.Common;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// Represents a directory object retrieved from a domain controller using the replication protocol.
    /// </summary>
    public class ReplicaObject : DirectoryObject
    {
        private string distinguishedName;
        private Guid guid;
        private SecurityIdentifier? sid;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplicaObject"/> class.
        /// </summary>
        /// <param name="distinguishedName">The distinguished name of the object.</param>
        /// <param name="objectGuid">The globally unique identifier (GUID) of the object.</param>
        /// <param name="objectSid">The security identifier (SID) of the object.</param>
        /// <param name="attributes">The attributes of the object.</param>
        /// <param name="schema">The Active Directory schema.</param>
        public ReplicaObject(string distinguishedName, Guid objectGuid, SecurityIdentifier? objectSid, ReplicaAttributeCollection attributes, BaseSchema schema)
        {
            Validator.AssertNotNull(distinguishedName, nameof(distinguishedName));
            Validator.AssertNotNull(attributes, nameof(attributes));
            Validator.AssertNotNull(schema, nameof(schema));

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
        /// Gets the class of which the object is an instance, as well as all structural or abstract superclasses from which that class is derived.
        /// </summary>
        public ClassType[] ObjectClass
        {
            get
            {
                this.ReadAttribute(AttributeType.ObjectClass, out uint[]? numericObjectClasses);

                if (numericObjectClasses != null)
                {
                    ClassType[] objectClasses = new ClassType[numericObjectClasses.Length];

                    for (int i = 0; i < numericObjectClasses.Length; i++)
                    {
                        objectClasses[i] = (ClassType)numericObjectClasses[i];
                    }

                    return objectClasses;
                }
                else
                {
                    // This should never happen, as objectClass is a mandatory attribute.
                    return [];
                }
            }
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

        /// <summary>
        /// Indicates whether this object has been marked for deletion. After the tombstone period has expired, it will be removed from the system.
        /// </summary>
        /// <remarks>
        /// This is a more efficient implementation than the base class, as it directly reads the isDeleted attribute.
        /// </remarks>
        public override bool IsDeleted
        {
            get
            {
                ReadAttribute(AttributeType.IsDeleted, out bool isDeleted);
                return isDeleted;
            }
        }

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
        public bool HasAttribute(AttributeType attributeId)
        {
            return this.Attributes.ContainsKey(attributeId);
        }

        /// <summary>
        /// Reads all values of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="values">The values of the attribute.</param>
        public void ReadAttribute(AttributeType attributeId, out byte[][]? values)
        {
            values = null;
            bool hasAttribute = this.Attributes.TryGetValue(attributeId, out ReplicaAttribute attribute);
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
        public void ReadAttribute(AttributeType attributeId, out byte[]? value)
        {
            this.ReadAttribute(attributeId, out value, 0);
        }

        /// <summary>
        /// Reads the specified value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="valueIndex">The index of the value to read.</param>
        public void ReadAttribute(AttributeType attributeId, out byte[]? value, int valueIndex)
        {
            this.ReadAttribute(attributeId, out byte[][] values);
            bool containsValue = values != null && values.Length > valueIndex;
            value = containsValue ? values[valueIndex] : null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public void ReadAttribute(AttributeType attributeId, out int? value)
        {
            this.ReadAttribute(attributeId, out byte[]? binaryValue);
            value = (binaryValue != null) ? BitConverter.ToInt32(binaryValue, 0) : null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public void ReadAttribute(AttributeType attributeId, out uint? value)
        {
            this.ReadAttribute(attributeId, out byte[]? binaryValue);
            value = (binaryValue != null) ? BitConverter.ToUInt32(binaryValue, 0) : null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public void ReadAttribute(AttributeType attributeId, out long? value)
        {
            this.ReadAttribute(attributeId, out byte[] binaryValue);
            value = (binaryValue != null) ? BitConverter.ToInt64(binaryValue, 0) : null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public void ReadAttribute(AttributeType attributeId, out Guid? value)
        {
            this.ReadAttribute(attributeId, out byte[] binaryValue);
            value = (binaryValue != null) ? new Guid(binaryValue) : null;
        }

        /// <summary>
        /// Reads the specified attribute and returns its values as an array of unsigned integers.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="values">When this method returns, contains an array of unsigned integers representing the attribute values, or <see
        /// langword="null"/> if no values are available.</param>
        public void ReadAttribute(AttributeType attributeId, out uint[]? values)
        {
            this.ReadAttribute(attributeId, out byte[][]? binaryValues);

            if (binaryValues != null && binaryValues.Length > 0)
            {
                values = new uint[binaryValues.Length];

                for (int i = 0; i < binaryValues.Length; i++)
                {
                    values[i] = BitConverter.ToUInt32(binaryValues[i], 0);
                }
            }
            else
            {
                values = null;
            }
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="unicode">Indicates whether to use Unicode encoding.</param>
        public void ReadAttribute(AttributeType attributeId, out string? value, bool unicode = true)
        {
            var encoding = unicode ? Encoding.Unicode : Encoding.ASCII;
            this.ReadAttribute(attributeId, out byte[] binaryValue);
            value = (binaryValue != null) ? encoding.GetString(binaryValue) : null;
        }

        /// <summary>
        /// Reads all values of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="values">The values of the attribute.</param>
        /// <param name="unicode">Indicates whether to use Unicode encoding.</param>
        public void ReadAttribute(AttributeType attributeId, out string[]? values, bool unicode = true)
        {
            var encoding = unicode ? Encoding.Unicode : Encoding.ASCII;
            values = null;
            this.ReadAttribute(attributeId, out byte[][] binaryValues);
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
        public void ReadAttribute(AttributeType attributeId, out DistinguishedName? value)
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
        public void ReadAttribute(AttributeType attributeId, out SecurityIdentifier? value)
        {
            this.ReadAttribute(attributeId, out byte[] binaryValue);
            value = (binaryValue != null) ? new SecurityIdentifier(binaryValue, 0) : null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public void ReadAttribute(AttributeType attributeId, out SamAccountType? value)
        {
            int? numericValue;
            this.ReadAttribute(attributeId, out numericValue);
            value = numericValue.HasValue ? (SamAccountType)numericValue.Value : null;
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public void ReadAttribute(AttributeType attributeId, out bool value)
        {
            this.ReadAttribute(attributeId, out int? numericValue);
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
            value = null;
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out value);
            }
        }

        /// <summary>
        /// Reads all values of the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">The values of the attribute.</param>
        public override void ReadAttribute(string name, out byte[][] value)
        {
            value = null;
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out value);
            }
        }

        /// <summary>
        /// Reads the first value of the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">The value of the attribute.</param>
        public override void ReadAttribute(string name, out int? value)
        {
            value = null;
            AttributeType? attributeId = this.Schema.FindAttributeId(name);
            if (attributeId != null)
            {
                this.ReadAttribute(attributeId.Value, out value);
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
            AttributeType? attributeId = Schema.FindAttributeId(attributeName);
            if (attributeId.HasValue)
            {
                ReadAttribute(attributeId.Value, out values);
            }
            else
            {
                // The schema does not even contain this attribute.
                values = null;
            }
        }

        /// <summary>
        /// Reads the linked values for the specified attribute and returns them as an array of parsed binary data.
        /// </summary>
        /// <param name="attributeId">The identifier of the attribute whose linked values are to be read.</param>
        /// <param name="values">When this method returns, contains an array of byte arrays representing the parsed linked values, or null if
        /// no values are present.</param>
        public void ReadLinkedValues(AttributeType attributeId, out byte[][]? values)
        {
            // The linked values have already been merged with regular attributes using LoadLinkedValues
            // TODO: We currently only support DN-Binary linked values.
            // TODO: Check if the attribute exists
            this.ReadAttribute(attributeId, out byte[][]? rawValues);

            if (rawValues != null && rawValues.Length > 0)
            {
                // Allocate the array for parsed values
                values = new byte[rawValues.Length][];

                for (int i = 0; i < rawValues.Length; i++)
                {
                    values[i] = ParseDNBinary(rawValues[i]);
                }
            }
            else
            {
                values = null;
            }
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
        public static byte[] ParseDNBinary(byte[] blob)
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
