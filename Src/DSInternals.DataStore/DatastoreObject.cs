namespace DSInternals.DataStore
{
    using System;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using DSInternals.Common;
    using DSInternals.Common.Data;
    using DSInternals.Common.Exceptions;
    using DSInternals.Common.Schema;
    using Microsoft.Database.Isam;

    /// <summary>
    /// Represents an Active Directory object stored in the database, providing access to attributes and metadata.
    /// </summary>
    public sealed class DatastoreObject : DirectoryObject
    {
        private DirectoryContext context;
        private Cursor cursor;

        /// <summary>
        /// Initializes a new instance of the DatastoreObject class with the specified database cursor and context.
        /// </summary>
        /// <param name="datatableCursor">The database cursor positioned at the object's record.</param>
        /// <param name="context">The directory context for database operations.</param>
        public DatastoreObject(Cursor datatableCursor, DirectoryContext context)
        {
            this.cursor = datatableCursor;
            this.context = context;
        }
        public override string DistinguishedName
        {
            get
            {
                var dn = this.context.DistinguishedNameResolver.Resolve(this.DNTag);
                return dn.ToString();
            }
        }

        public DNTag DNTag
        {
            get
            {
                Columnid columnId = this.context.Schema.DistinguishedNameTagColumnId;
                DNTag? dnt = cursor.RetrieveColumnAsDNTag(columnId);
                return dnt.Value;
            }
        }

        public override Guid Guid
        {
            get
            {
                this.ReadAttribute(CommonDirectoryAttributes.ObjectGuid, out Guid? guid);
                return guid ?? Guid.Empty;
            }
        }

        public override SecurityIdentifier Sid
        {
            get
            {
                this.ReadAttribute(CommonDirectoryAttributes.ObjectSid, out SecurityIdentifier sid);
                return sid;
            }
        }

        public bool IsPhantomObject
        {
            get
            {
                // Check the value of the OBJ_col
                bool isObject = this.cursor.RetrieveColumnAsBoolean(this.context.Schema.ObjectColumnId);
                return !isObject;
            }
        }

        protected override bool HasBigEndianRid
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// AddAttribute implementation.
        /// </summary>
        public bool AddAttribute(string name, SecurityIdentifier[] valuesToAdd)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(CommonDirectoryAttributes.SidHistory);
            if (columnId != null)
            {
                bool hasChanged = this.cursor.AddMultiValue(columnId, valuesToAdd);
                return hasChanged;
            }

            return false;
        }

        /// <summary>
        /// Delete implementation.
        /// </summary>
        public void Delete()
        {
            // TODO: Check if we are in a transaction
            using (var transaction = this.context.BeginTransaction())
            {
                this.cursor.Delete();
            }
            // Invalidate this object:
            this.cursor = null;
        }

        /// <summary>
        /// HasAttribute implementation.
        /// </summary>
        public override bool HasAttribute(string name)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);

            if (columnId != null)
            {
                // Read the appropriate column and check if it has a value
                long columnSize = cursor.Record.SizeOf(columnId);
                return columnSize > 0;
            }
            else
            {
                // The schema does not even contain this attribute, so the object cannot have it.
                return false;
            }
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out byte[] value)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);
            value = columnId != null ? this.cursor.RetrieveColumnAsByteArray(columnId) : null;
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out byte[][] value)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);
            value = columnId != null ? this.cursor.RetrieveColumnAsMultiByteArray(columnId) : null;
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out int? value)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);
            value = columnId != null ? this.cursor.RetrieveColumnAsInt(columnId) : null;
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public void ReadAttribute(string name, out DNTag? value)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);
            value = columnId != null ? (DNTag?)this.cursor.RetrieveColumnAsInt(columnId) : null;
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out string value, bool unicode = true)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);
            value = columnId != null ? this.cursor.RetrieveColumnAsString(columnId, unicode) : null;
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out string[] values, bool unicode = true)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);
            values = columnId != null ? this.cursor.RetrieveColumnAsStringArray(columnId, unicode) : null;
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out long? value)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);
            value = columnId != null ? this.cursor.RetrieveColumnAsLong(columnId) : null;
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out DistinguishedName? value)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);

            // The value can either be located in the datatable or in the link table
            DNTag? dnt = columnId != null ? this.cursor.RetrieveColumnAsDNTag(columnId) : this.context.LinkResolver.GetLinkedDNTag(this.DNTag, name);

            // Translate the distinguished name tag to DN
            value = dnt.HasValue ? this.context.DistinguishedNameResolver.Resolve(dnt.Value) : null;
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out RawSecurityDescriptor value)
        {
            this.ReadAttribute(name, out byte[] binaryValue);

            if (binaryValue == null)
            {
                value = null;
                return;
            }

            if (binaryValue.Length == sizeof(long))
            {
                // The binary value is a 64-bit foreign key
                long securityDescriptorId = BitConverter.ToInt64(binaryValue, 0);
                value = this.context.SecurityDescriptorResolver.GetDescriptor(securityDescriptorId);
            }
            else
            {
                // The binary value contains the security descriptor
                value = new RawSecurityDescriptor(binaryValue, 0);
            }
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public void ReadAttribute(string name, out ClassType? value)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);
            value = columnId != null ? this.cursor.RetrieveColumnAsObjectCategory(columnId) : null;
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public void ReadAttribute(string name, out AttributeMetadataCollection value)
        {
            Columnid? columnId = this.context.Schema.FindColumnId(name);

            if (columnId != null)
            {
                this.ReadAttribute(name, out byte[] binaryValue);
                value = new AttributeMetadataCollection(binaryValue);
            }
            else
            {
                value = null;
            }
        }

        /// <summary>
        /// ReadLinkedValues implementation.
        /// </summary>
        public override void ReadLinkedValues(string attributeName, out byte[][] values)
        {
            // Cut off the first 4 bytes, which is the length of the entire structure.
            values = this.context.LinkResolver.GetLinkedValues(this.DNTag, attributeName)
                .Select( link => link.LinkData.Cut(sizeof(int)) ).ToArray();
        }

        public bool SetAttribute<T>(string name, T? newValue) where T : struct
        {
            // TODO: This must be called from a transaction
            Columnid? columnId  = this.context.Schema.FindColumnId(name);

            if (columnId == null)
            {
                throw new SchemaAttributeNotFoundException(name);
            }

            bool hasChanged = this.cursor.SetValue(columnId, newValue);
            return hasChanged;
        }

        /// <summary>
        /// SetAttribute implementation.
        /// </summary>
        public bool SetAttribute(string name, DateTime newValue)
        {
            if(newValue != DateTime.MinValue)
            {
                return this.SetAttribute<long>(name, newValue.ToFileTime());
            }
            else
            {
                // The value of Never cannot be converted to FileTime.
                return this.SetAttribute<long>(name, 0);
            }
        }

        /// <summary>
        /// SetAttribute implementation.
        /// </summary>
        public bool SetAttribute(string name, byte[] newValue)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            bool hasChanged = this.cursor.SetValue (columnId, newValue);
            return hasChanged;
        }

        /// <summary>
        /// UpdateAttributeMeta implementation.
        /// </summary>
        public void UpdateAttributeMeta(string attributeName, long usn, DateTime time)
        {
            Validator.AssertNotNull(attributeName, "attributeName");
            this.UpdateAttributeMeta(new string[] { attributeName }, usn, time);
        }

        /// <summary>
        /// UpdateAttributeMeta implementation.
        /// </summary>
        public void UpdateAttributeMeta(string[] attributeNames, long usn, DateTime time)
        {
            Validator.AssertNotNull(attributeNames, nameof(attributeNames));

            ColumnAccessor record = this.cursor.EditRecord;
            
            // Update the uSNChanged attribute
            this.SetAttribute<long>(CommonDirectoryAttributes.USNChanged, usn);
            
            // Update the whenChanged attribute
            this.SetAttribute<long>(CommonDirectoryAttributes.WhenChanged, time.ToGeneralizedTime());

            // Update the replPropertyMetaData attribute (read-modify-write)
            this.ReadAttribute(CommonDirectoryAttributes.ReplicationPropertyMetaData, out AttributeMetadataCollection meta);

            foreach(string attributeName in attributeNames)
            {
                // We go through all attributes that are changed in this transaction
                AttributeType? attributeId = this.context.Schema.FindAttribute(attributeName)?.AttributeId;

                if (!attributeId.HasValue)
                {
                    throw new SchemaAttributeNotFoundException(attributeName);
                }

                meta.Update(attributeId.Value, this.context.DomainController.InvocationId, time, usn);
            }
            
            this.SetAttribute(CommonDirectoryAttributes.ReplicationPropertyMetaData, meta.ToByteArray());
        }
    }
}
