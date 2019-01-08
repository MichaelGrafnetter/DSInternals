namespace DSInternals.Common.Data
{
    using System;
    using System.IO;
    using System.Security;

    /// <summary>
    /// Represents a group-managed service account's password information.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/hh881234.aspx</see>
    public class ManagedPassword
    {
        private const int MinimumBlobLength = 6 * sizeof(short) + sizeof(int);

        /// <summary>
        /// Gets the version of the msDS-ManagedPassword binary large object (BLOB).
        /// </summary>
        public short Version
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current password.
        /// </summary>
        public string CurrentPassword
        {
            get
            {
                return this.SecureCurrentPassword.ToUnicodeString();
            }
        }

        /// <summary>
        /// Gets the current password.
        /// </summary>
        public SecureString SecureCurrentPassword
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the previous password.
        /// </summary>
        public string PreviousPassword
        {
            get
            {
                return this.SecurePreviousPassword.ToUnicodeString();
            }
        }

        /// <summary>
        /// Gets the previous password.
        /// </summary>
        public SecureString SecurePreviousPassword
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the length of time after which the receiver should requery the password.
        /// </summary>
        public TimeSpan QueryPasswordInterval
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the length of time before which password queries will always return this password value.
        /// </summary>
        public TimeSpan UnchangedPasswordInterval
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedPassword"/> class.
        /// </summary>
        /// <param name="blob">
        /// The MSDS-MANAGEDPASSWORD_BLOB, which is a representation
        /// of a group-managed service account's password information.
        /// This structure is returned as the msDS-ManagedPassword constructed attribute.
        /// </param>
        public ManagedPassword(byte[] blob)
        {
            Validator.AssertMinLength(blob, MinimumBlobLength, "blob");
            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // A 16-bit unsigned integer that defines the version of the msDS-ManagedPassword binary large object (BLOB). The Version field MUST be set to 0x0001.
                    this.Version = reader.ReadInt16();
                    // TODO: Test that version == 1

                    // A 16-bit unsigned integer that MUST be set to 0x0000.
                    short reserved = reader.ReadInt16();
                    // TODO: Test that reserved == 0

                    // A 32-bit unsigned integer that specifies the length, in bytes, of the msDS-ManagedPassword BLOB.
                    int length = reader.ReadInt32();
                    Validator.AssertLength(blob, length, "blob");

                    // A 16-bit offset, in bytes, from the beginning of this structure to the CurrentPassword field. The CurrentPasswordOffset field MUST NOT be set to 0x0000.
                    short currentPasswordOffset = reader.ReadInt16();
                    this.SecureCurrentPassword = blob.ReadSecureWString(currentPasswordOffset);

                    // A 16-bit offset, in bytes, from the beginning of this structure to the PreviousPassword field. If this field is set to 0x0000, then the account has no previous password.
                    short previousPasswordOffset = reader.ReadInt16();
                    if(previousPasswordOffset > 0)
                    {
                        this.SecurePreviousPassword = blob.ReadSecureWString(previousPasswordOffset);
                    }

                    // A 16-bit offset, in bytes, from the beginning of this structure to the QueryPasswordInterval field.
                    short queryPasswordIntervalOffset = reader.ReadInt16();
                    long queryPasswordIntervalBinary = BitConverter.ToInt64(blob, queryPasswordIntervalOffset);
                    this.QueryPasswordInterval = TimeSpan.FromTicks(queryPasswordIntervalBinary);

                    // A 16-bit offset, in bytes, from the beginning of this structure to the UnchangedPasswordInterval field.
                    short unchangedPasswordIntervalOffset = reader.ReadInt16();
                    long unchangedPasswordIntervalBinary = BitConverter.ToInt64(blob, unchangedPasswordIntervalOffset);
                    this.UnchangedPasswordInterval = TimeSpan.FromTicks(unchangedPasswordIntervalBinary);
                }
            }
        }
    }
}
