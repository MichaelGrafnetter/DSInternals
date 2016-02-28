namespace DSInternals.DataStore
{
    using DSInternals.Common;
    using DSInternals.Common.Exceptions;
    using Microsoft.Database.Isam;
    using System;
    using System.Collections.Generic;
    using System.Security.AccessControl;
    using System.Security.Cryptography;

    public class SecurityDescriptorRersolver : IDisposable
    {
        private const string SdIdCol = "sd_id";
        private const string SdValueCol = "sd_value";
        private const string SdHashCol = "sd_hash";
        private const string SdRefCountCol = "sd_refcount";
        private const string SdIndex = "sd_id_index";
        private const string SdHashIndex = "sd_hash_index";
        private const int RootSecurityDescriptorOffset = sizeof(int);

        private Cursor cursor;

        public SecurityDescriptorRersolver(IsamDatabase database)
        {
            this.cursor = database.OpenCursor(ADConstants.SecurityDescriptorTableName);
        }

        public RawSecurityDescriptor GetDescriptor(long id)
        {
            this.cursor.CurrentIndex = SdIndex;
            bool found = this.cursor.GotoKey(Key.Compose(id));
            if (!found)
            {
                throw new DirectoryObjectNotFoundException(id);
            }
            var binaryForm = this.cursor.RetrieveColumnAsByteArray(SdValueCol);
            // Strip the root SD prefix, which is 0x0F000000
            int sdOffset = (id == ADConstants.RootSecurityDescriptorId) ? RootSecurityDescriptorOffset : 0;
            return new RawSecurityDescriptor(binaryForm, sdOffset);
        }

        public IEnumerable<long> FindDescriptor(GenericSecurityDescriptor securityDescriptor)
        {
            byte[] sdHash = ComputeHash(securityDescriptor);
            return this.FindDescriptorHash(sdHash);
        }

        public IEnumerable<long> FindDescriptor(string securityDescriptor)
        {
            byte[] sdHash = ComputeHash(securityDescriptor);
            return this.FindDescriptorHash(sdHash);
        }

        public IEnumerable<long> FindDescriptorHash(byte[] sdHash)
        {
            Validator.AssertNotNull(sdHash, "sdHash");
            this.cursor.CurrentIndex = SdHashIndex;
            this.cursor.FindRecords(MatchCriteria.EqualTo, Key.Compose(sdHash));
            while(cursor.MoveNext())
            {
                long id = this.cursor.RetrieveColumnAsLong(SdIdCol).Value;
                yield return id;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static byte[] ComputeHash(GenericSecurityDescriptor securityDescriptor)
        {
            Validator.AssertNotNull(securityDescriptor, "securityDescriptor");
            
            // Convert to binary form. We have to use double conversion, because .NET returns the SD in different order than Win32 API used by AD.
            string stringSecurityDescriptor = securityDescriptor.GetSddlForm(AccessControlSections.All);
            return ComputeHash(stringSecurityDescriptor);
        }

        public static byte[] ComputeHash(string securityDescriptor)
        {
            Validator.AssertNotNullOrWhiteSpace(securityDescriptor, "securityDescriptor");

            return ComputeHash(securityDescriptor.SddlToBinary());
        }

        public static byte[] ComputeHash(byte[] securityDescriptor)
        {
            Validator.AssertNotNull(securityDescriptor, "securityDescriptor");

            using (var hashFunction = MD5.Create())
            {
                // TODO: Cache the hash function for performance reasons
                return hashFunction.ComputeHash(securityDescriptor);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.cursor != null)
            {
                this.cursor.Dispose();
                this.cursor = null;
            }
        }
    }
}
