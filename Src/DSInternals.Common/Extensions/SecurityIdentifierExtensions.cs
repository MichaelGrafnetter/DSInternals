namespace DSInternals.Common
{
    using System;
    using System.Security.Principal;

    public static class SecurityIdentifierExtensions
    {
        private const int ridLength = 4;

        public static int GetRid(this SecurityIdentifier sid)
        {
            Validator.AssertNotNull(sid, "sid");
            byte[] binaryForm = sid.GetBinaryForm();
            int domainSidLength = binaryForm.Length - ridLength;
            if (domainSidLength < 0)
            {
                throw new ArgumentOutOfRangeException("sid", binaryForm.Length, "The SID is too short.");
            }
            return BitConverter.ToInt32(binaryForm, domainSidLength);
        }

        public static byte[] GetBinaryForm(this SecurityIdentifier sid, bool bigEndianRid = false)
        {
            Validator.AssertNotNull(sid, "sid");
            int sidLength = sid.BinaryLength;
            byte[] binarySid = new byte[sidLength];
            sid.GetBinaryForm(binarySid, 0);
            if (bigEndianRid)
            {
                int lastByteIndex = sidLength - 1;
                // Convert RID from big endian to little endian (Reverse the order of the last 4 bytes)
                binarySid.SwapBytes(lastByteIndex - 3, lastByteIndex);
                binarySid.SwapBytes(lastByteIndex - 2, lastByteIndex - 1);
            }
            return binarySid;
        }
    }
}