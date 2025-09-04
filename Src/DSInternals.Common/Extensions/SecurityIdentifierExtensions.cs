namespace DSInternals.Common
{
    using System;
    using System.Security.Principal;

    /// <summary>
    /// Provides extension methods for SecurityIdentifier objects to extract RID and convert to binary form.
    /// </summary>
    public static class SecurityIdentifierExtensions
    {
        private const int ridLength = 4;

        /// <summary>
        /// Extracts the Relative Identifier (RID) from the security identifier.
        /// </summary>
        /// <param name="sid">The security identifier to extract the RID from.</param>
        /// <returns>The RID as an integer value.</returns>
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

        /// <summary>
        /// Converts the security identifier to its binary representation with optional RID endianness conversion.
        /// </summary>
        /// <param name="sid">The security identifier to convert.</param>
        /// <param name="bigEndianRid">True to convert the RID to big-endian format; otherwise, false for little-endian.</param>
        /// <returns>The binary representation of the security identifier.</returns>
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