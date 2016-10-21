namespace DSInternals.DataStore
{
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Data;
    using Microsoft.Database.Isam;
    using System;
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
                Columnid columnId = this.context.Schema.FindColumnId(CommonDirectoryAttributes.DNTag);
                int? dnt = cursor.RetrieveColumnAsDNTag(columnId);
                var dn = this.context.DistinguishedNameResolver.Resolve(dnt.Value);
                return dn.ToString();
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
            Columnid columnId = this.context.Schema.FindColumnId(name);
            long columnSize = cursor.Record.SizeOf(columnId);
            return columnSize > 0;
        }

        public override void ReadAttribute(string name, out byte[] value)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            value = this.cursor.RetrieveColumnAsByteArray(columnId);
        }

        public override void ReadAttribute(string name, out byte[][] value)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            value = this.cursor.RetrieveColumnAsMultiByteArray(columnId);
        }

        public override void ReadAttribute(string name, out int? value)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            value = this.cursor.RetrieveColumnAsInt(columnId);
        }

        public override void ReadAttribute(string name, out string value)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            value = this.cursor.RetrieveColumnAsString(columnId);
        }

        public override void ReadAttribute(string name, out string[] values)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            values = this.cursor.RetrieveColumnAsStringArray(columnId);
        }

        public override void ReadAttribute(string name, out long? value)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            value = this.cursor.RetrieveColumnAsLong(columnId);
        }

        public override void ReadAttribute(string name, out DistinguishedName value)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            var dnt = this.cursor.RetrieveColumnAsDNTag(columnId);
            value = this.context.DistinguishedNameResolver.Resolve(dnt.Value);
        }

        public override void ReadAttribute(string name, out RawSecurityDescriptor value)
        {
            byte[] binarySecurityDescriptorId;
            // The value is stored as 8 bytes instead of 64-bit integer
            this.ReadAttribute(name, out binarySecurityDescriptorId);

            if (binarySecurityDescriptorId == null)
            {
                value = null;
                return;
            }
            long securityDescriptorId = BitConverter.ToInt64(binarySecurityDescriptorId, 0);
            value = this.context.SecurityDescriptorRersolver.GetDescriptor(securityDescriptorId);
        }

        public void ReadAttribute(string name, out AttributeMetadataCollection value)
        {
            byte[] binaryValue;
            this.ReadAttribute(name, out binaryValue);
            value = new AttributeMetadataCollection(binaryValue);
        }

        public bool SetAttribute<T>(string name, Nullable<T> newValue) where T : struct
        {
            // TODO: This must be called from a transaction
            Columnid columnId  = this.context.Schema.FindColumnId(name);
            bool hasChanged = this.cursor.SetValue(columnId, newValue);
            return hasChanged;
        }

        public bool SetAttribute(string name, byte[] newValue)
        {
            Columnid columnId = this.context.Schema.FindColumnId(name);
            bool hasChanged = this.cursor.SetValue (columnId, newValue);
            return hasChanged;
        }
        public void UpdateAttributeMeta(string attributeName, long usn, DateTime time)
        {

            ColumnAccessor record = this.cursor.EditRecord;
            // Update the uSNChanged attribute
            this.SetAttribute<long>(CommonDirectoryAttributes.USNChanged, usn);
            // Update the whenChanged attribute
            // TODO: Add time as parameter?
            DateTime now = DateTime.Now;
            this.SetAttribute<long>(CommonDirectoryAttributes.WhenChanged, now.ToGeneralizedTime());
            // Update the replPropertyMetaData attribute
            AttributeMetadataCollection meta;
            this.ReadAttribute(CommonDirectoryAttributes.PropertyMetaData, out meta);
            int attributeId = this.context.Schema.FindAttribute(attributeName).Id.Value;
            meta.Update(attributeId, this.context.DomainController.InvocationId, now, usn);
            this.SetAttribute(CommonDirectoryAttributes.PropertyMetaData, meta.ToByteArray());
        }
    }
}
