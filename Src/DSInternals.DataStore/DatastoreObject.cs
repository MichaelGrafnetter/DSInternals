namespace DSInternals.DataStore
{
    using DSInternals.Common;
    using DSInternals.Common.Data;
    using Microsoft.Database.Isam;
    using System;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Security.Principal;

    public sealed class DatastoreObject : DirectoryObject
    {
        private DirectoryContext context;
        private Cursor cursor;
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

        public int DNTag
        {
            get
            {
                Columnid columnId = this.context.Schema.FindColumnId(CommonDirectoryAttributes.DNTag);
                int? dnt = cursor.RetrieveColumnAsDNTag(columnId);
                return dnt.Value;
            }
        }

        public override Guid Guid
        {
            get
            {
                Guid? guid;
                this.ReadAttribute(CommonDirectoryAttributes.ObjectGUID, out guid);
                return guid ?? Guid.Empty;
            }
        }

        public override SecurityIdentifier Sid
        {
            get
            {
                SecurityIdentifier sid;
                this.ReadAttribute(CommonDirectoryAttributes.ObjectSid, out sid);
                return sid;
            }
        }

        protected override bool HasBigEndianRid
        {
            get
            {
                return true;
            }
        }

        public bool AddAttribute(string name, SecurityIdentifier[] valuesToAdd)
        {
            Columnid columnId = this.context.Schema.FindColumnId(CommonDirectoryAttributes.SIDHistory);
            bool hasChanged = this.cursor.AddMultiValue(columnId, valuesToAdd);
            return hasChanged;
        }

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

        public override bool HasAttribute(string name)
        {
            if(this.context.Schema.ContainsAttribute(name))
            {
                // Read the appropriate column and check if it has a value
                Columnid columnId = this.context.Schema.FindColumnId(name);
                long columnSize = cursor.Record.SizeOf(columnId);
                return columnSize > 0;
            }
            else
            {
                // The schema does not even contain this attribute, so the object cannot have it.
                return false;
            }
        }

        public override void ReadAttribute(string name, out byte[] value)
        {
            if(this.context.Schema.ContainsAttribute(name))
            {
                Columnid columnId = this.context.Schema.FindColumnId(name);
                value = this.cursor.RetrieveColumnAsByteArray(columnId);
            }
            else
            {
                value = null;
            }
        }

        public override void ReadAttribute(string name, out byte[][] value)
        {
            if (this.context.Schema.ContainsAttribute(name))
            {
                Columnid columnId = this.context.Schema.FindColumnId(name);
                value = this.cursor.RetrieveColumnAsMultiByteArray(columnId);
            }
            else
            {
                value = null;
            }
        }

        public override void ReadAttribute(string name, out int? value)
        {
            if (this.context.Schema.ContainsAttribute(name))
            {
                Columnid columnId = this.context.Schema.FindColumnId(name);
                value = this.cursor.RetrieveColumnAsInt(columnId);
            }
            else
            {
                value = null;
            }
        }

        public override void ReadAttribute(string name, out string value)
        {
            if (this.context.Schema.ContainsAttribute(name))
            {
                Columnid columnId = this.context.Schema.FindColumnId(name);
                value = this.cursor.RetrieveColumnAsString(columnId);
            }
            else
            {
                value = null;
            }
        }

        public override void ReadAttribute(string name, out string[] values)
        {
            if (this.context.Schema.ContainsAttribute(name))
            {
                Columnid columnId = this.context.Schema.FindColumnId(name);
                values = this.cursor.RetrieveColumnAsStringArray(columnId);
            }
            else
            {
                values = null;
            }
        }

        public override void ReadAttribute(string name, out long? value)
        {
            if (this.context.Schema.ContainsAttribute(name))
            {
                Columnid columnId = this.context.Schema.FindColumnId(name);
                value = this.cursor.RetrieveColumnAsLong(columnId);
            }
            else
            {
                value = null;
            }
        }

        public override void ReadAttribute(string name, out DistinguishedName value)
        {
            if (this.context.Schema.ContainsAttribute(name))
            {
                Columnid columnId = this.context.Schema.FindColumnId(name);
                var dnt = this.cursor.RetrieveColumnAsDNTag(columnId);
                value = this.context.DistinguishedNameResolver.Resolve(dnt.Value);
            }
            else
            {
                value = null;
            }
        }

        public override void ReadAttribute(string name, out RawSecurityDescriptor value)
        {
            this.ReadAttribute(name, out byte[] binaryValue);

            if (binaryValue == null)
            {
                value = null;
                return;
            }

            if(binaryValue.Length == sizeof(long))
            {
                // The binary value is a 64-bit foreign key
                long securityDescriptorId = BitConverter.ToInt64(binaryValue, 0);
                value = this.context.SecurityDescriptorRersolver.GetDescriptor(securityDescriptorId);
            }
            else
            {
                // The binary value contains the security descriptor
                value = new RawSecurityDescriptor(binaryValue, 0);
            }
        }

        public void ReadAttribute(string name, out AttributeMetadataCollection value)
        {

            byte[] binaryValue;
            this.ReadAttribute(name, out binaryValue);
            value = new AttributeMetadataCollection(binaryValue);
        }

        public override void ReadLinkedValues(string attributeName, out byte[][] values)
        {
            var rawValues = this.context.LinkResolver.GetLinkedValues(this.DNTag, attributeName);
            // Cut off the first 4 bytes, which is the length of the entire structure.
            values = rawValues.Select( rawValue => rawValue.Cut(sizeof(int)) ).ToArray();
        }

        public bool SetAttribute<T>(string name, T? newValue) where T : struct
        {
            // TODO: This must be called from a transaction
            Columnid columnId  = this.context.Schema.FindColumnId(name);
            bool hasChanged = this.cursor.SetValue(columnId, newValue);
            return hasChanged;
        }

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

        public bool SetAttribute(string name, byte[] newValue)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            bool hasChanged = this.cursor.SetValue (columnId, newValue);
            return hasChanged;
        }

        public void UpdateAttributeMeta(string attributeName, long usn, DateTime time)
        {
            Validator.AssertNotNull(attributeName, "attributeName");
            this.UpdateAttributeMeta(new string[] { attributeName }, usn, time);
        }

        public void UpdateAttributeMeta(string[] attributeNames, long usn, DateTime time)
        {
            Validator.AssertNotNull(attributeNames, nameof(attributeNames));

            ColumnAccessor record = this.cursor.EditRecord;
            
            // Update the uSNChanged attribute
            this.SetAttribute<long>(CommonDirectoryAttributes.USNChanged, usn);
            
            // Update the whenChanged attribute
            this.SetAttribute<long>(CommonDirectoryAttributes.WhenChanged, time.ToGeneralizedTime());

            // Update the replPropertyMetaData attribute (read-modify-write)
            this.ReadAttribute(CommonDirectoryAttributes.PropertyMetaData, out AttributeMetadataCollection meta);

            foreach(var attributeName in attributeNames)
            {
                // We go through all attributes that are changed in this transaction
                int attributeId = this.context.Schema.FindAttribute(attributeName).Id.Value;
                meta.Update(attributeId, this.context.DomainController.InvocationId, time, usn);
            }
            
            this.SetAttribute(CommonDirectoryAttributes.PropertyMetaData, meta.ToByteArray());
        }
    }
}
