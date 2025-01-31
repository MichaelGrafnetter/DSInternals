using System;
using System.Formats.Asn1;
using System.IO;
using System.Security.Cryptography.Pkcs;

namespace DSInternals.Common.Data
{
    public class LapsEncryptedPassword
    {
        private const int StructPrefixLength = sizeof(long) + 2 * sizeof(int);

        public DateTime UpdateTimeStamp
        {
            get;
            private set;
        }

        public uint FlagsReserved
        {
            get;
            private set;
        }

        public byte[] EncryptedBlob
        {
            get;
            private set;
        }

        public LapsEncryptedPassword(byte[] buffer)
        {
            Validator.AssertNotNull(buffer, nameof(buffer));
            Validator.AssertMinLength(buffer, StructPrefixLength, nameof(buffer));

            using (var stream = new MemoryStream(buffer))
            using(var reader = new BinaryReader(stream))
            {
                uint timestampHigh = reader.ReadUInt32();
                uint timestampLow = reader.ReadUInt32();
                long timestamp = (long)timestampHigh << 32 | (long)timestampLow;
                this.UpdateTimeStamp = DateTime.FromFileTimeUtc(timestamp);

                int encryptedBufferSize = reader.ReadInt32();
                this.FlagsReserved = reader.ReadUInt32();

                Validator.AssertLength(buffer, StructPrefixLength + encryptedBufferSize, nameof(buffer));

                if (encryptedBufferSize > 0)
                {
                    this.EncryptedBlob = reader.ReadBytes(encryptedBufferSize);

//                    //var z = new EnvelopedCms();
//                    //var x = AsnDecoder.ReadObjectIdentifier(this.EncryptedBlob, )
                    //var a = new Org.BouncyCastle.Cms.CmsEnvelopedData(this.EncryptedBlob);
//.
                    var ci = new AsnReader(this.EncryptedBlob, AsnEncodingRules.DER);
                    ci.ReadSequence(Asn1Tag.Sequence);
                    ci.ReadObjectIdentifier();

//                    //                    ci.ReadObjectIdentifier();
                }
            }
        }
    }
}
