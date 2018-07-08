namespace DSInternals.Common.Data
{
    public class CustomKeyInformation
    {
        private const byte CurrentVersion = 1;
        private const int MinLength = sizeof(byte) + sizeof(byte); // Version + KeyFlags
        private const int VersionOffset = 0;
        private const int FlagsOffset = 1;
        private const int VolumeTypeOffset = 2;
        private const int SupportsNotificationOffset = 3;
        private const int FekKeyVersionOffset = 4;
        private const int KeyStrengthOffset = 5;
        private const int ReservedOffset = 6;
        private const int EncodedExtendedCKIOffset = 16;


        public byte Version
        {
            get;
            private set;
        }

        public KeyFlags Flags
        {
            get;
            private set;
        }

        public VolumeType VolumeType
        {
            get;
            private set;
        }

        public bool SupportsNotification
        {
            get;
            private set;
        }

        public byte FekKeyVersion
        {
            get;
            private set;
        }

        public KeyStrength Strength
        {
            get;
            private set;
        }

        public CustomKeyInformation() : this(KeyFlags.None)
        {
        }

        public CustomKeyInformation(KeyFlags flags)
        {
            this.Version = CurrentVersion;
            this.Flags = flags;
        }

        public CustomKeyInformation(byte[] blob)
        {
            // Validate the input
            Validator.AssertNotNull(blob, "blob");
            Validator.AssertMinLength(blob, MinLength, "blob");

            // Parse:
            // An 8 - bit unsigned integer that must be set to 1:
            this.Version = blob[VersionOffset];

            // An 8-bit unsigned integer that specifies zero or more bit-flag values.
            this.Flags = (KeyFlags)blob[FlagsOffset];

            if(blob.Length <= VolumeTypeOffset)
            {
                // This structure has two possible representations. In the first representation, only the Version and Flags fields are present; in this case the structure has a total size of two bytes.In the second representation, all additional fields shown below are also present; in this case, the structure's total size is variable. Differentiating between the two representations must be inferred using only the total size.
                return;
            }

            // An 8-bit unsigned integer that specifies one of the volume types.
            this.VolumeType = (VolumeType)blob[VolumeTypeOffset];

            if(blob.Length <= SupportsNotificationOffset)
            {
                return;
            }

            // An 8 - bit unsigned integer that specifies whether the device associated with this credential supports notification.
            this.SupportsNotification = blob[SupportsNotificationOffset] != 0;

            if (blob.Length <= FekKeyVersionOffset)
            {
                return;
            }

            // An 8-bit unsigned integer that specifies the version of the File Encryption Key (FEK). This field must be set to 1.
            this.FekKeyVersion = blob[FekKeyVersionOffset];

            if (blob.Length <= KeyStrengthOffset)
            {
                return;
            }

            // An 8 - bit unsigned integer that specifies the strength of the NGC key.
            this.Strength = (KeyStrength)blob[KeyStrengthOffset];

            // TODO: Read Reserved: 10 bytes reserved for future use.
            // TODO: Read EncodedExtendedCKI: Extended custom key information.
        }

        public byte[] ToByteArray()
        {
            // We only support the short 2-byte format, which is more than enough for NGC generation.
            var blob = new byte[MinLength];
            blob[VersionOffset] = this.Version;
            blob[FlagsOffset] = (byte)this.Flags;
            return blob;
        }
    }
}