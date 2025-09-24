
using DSInternals.Common;
using System;
namespace DSInternals.DataStore
{
    /// <summary>
    /// Database setting flags stored in the hiddentable.
    /// </summary>
    public class DatabaseFlags
    {
        private const int AuxClassOffset = 0;
        private const int SDConversionRequiredOffset = 1;
        private const int RootGUIDUpdatedOffset = 2;
        private const int ADAMDatabaseOffset = 3;
        private const int ASCIIIndicesRebuiltOffset = 4;
        private const int ShowInABArrayRebuildOffset = 5;
        private const int UpdateNCTypeRequiredOffset = 6;
        private const int LinkQuotaUSNOffset = 7;
        private const int OldStructureSize = 200;
        private const int NewStructureSize = 192;
        private const int DatabaseGUIDOffset = NewStructureSize - 16;

        private byte[] binaryFlags;

        public DatabaseFlags(byte[] binaryFlags)
        {
            Validator.AssertNotNull(binaryFlags, "binaryFlags");
            this.binaryFlags = binaryFlags;
        }

        private bool HasFlag(int offset)
        {
            byte value = this.binaryFlags[offset];
            // The value is curiously stored as char.
            switch((char)value)
            {
                case '1':
                    return true;
                case '0':
                case '\0':
                    return false;
                default:
                    // TODO: Exception type
                    throw new Exception("Unknown flag value");
            }
        }

        /// <summary>
        ///  Indexes used into this array.
        /// </summary>
        public bool AuxClass
        {
            get
            {
                return this.HasFlag(AuxClassOffset);
            }
        }

        /// <summary>
        /// Set if SDs need to be updated (when an old DIT is detected without SD table)
        /// </summary>
        public bool SDConversionRequired
        {
            get
            {
                return this.HasFlag(SDConversionRequiredOffset);
            }
        }

        /// <summary>
        /// Root DNT GUID was updated.
        /// </summary>
        public bool RootGUIDUpdated
        {
            get
            {
                return this.HasFlag(RootGUIDUpdatedOffset);
            }
        }

        /// <summary>
        /// ADAM database.
        /// </summary>
        public bool ADAMDatabase
        {
            get
            {
                return this.HasFlag(ADAMDatabaseOffset);
            }
        }

        /// <summary>
        /// Win2k3 SP1 required rebuilt ASCII indices, this indicates that has been done.
        /// </summary>
        public bool ASCIIIndicesRebuilt
        {
            get
            {
                return this.HasFlag(ASCIIIndicesRebuiltOffset);
            }
        }

        /// <summary>
        /// Win2k3 SP1 to LH upgrade requires rebuild of showInAddressBookArray column.
        /// </summary>
        public bool ShowInABArrayRebuild
        {
            get
            {
                return this.HasFlag(ShowInABArrayRebuildOffset);
            }
        }

        /// <summary>
        /// Pre-LH to LH upgrade requires addition of msds-NCType.
        /// </summary>
        public bool UpdateNCTypeRequired
        {
            get
            {
                return this.HasFlag(UpdateNCTypeRequiredOffset);
            }
        }

        /// <summary>
        /// Link quota feature start USN in DBFLAGS.
        /// </summary>
        public long LinkQuotaUSN
        {
            get
            {
                return BitConverter.ToInt64(this.binaryFlags, LinkQuotaUSNOffset);
            }
        }

        /// <summary>
        /// The database GUID
        /// </summary>
        public Guid? DatabaseGUID
        {
            get
            {
                // Contains GUID in the last 16B in newer database versions.
                // Old size is 200 bytes, new size is 192 bytes.
                // TODO: Implement database GUID retrieval.
                throw new NotImplementedException();
            }
        }
    }
}
