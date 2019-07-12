namespace DSInternals.Common.Data.Fido
{
    /// <summary>
    /// <see cref="https://www.w3.org/TR/webauthn/#extensions"/>
    /// </summary>
    public class Extensions
    {
        private readonly byte[] _extensionBytes;
        public Extensions(byte[] extensions)
        {
            // Input validation
            Validator.AssertNotNull(extensions, "extensions");

            _extensionBytes = extensions;
        }
        public override string ToString()
        {
            return string.Format("Extensions: {0}",
                PeterO.Cbor.CBORObject.DecodeFromBytes(_extensionBytes));
        }
        public int Length
        {
            get
            {
                return _extensionBytes.Length;
            }
        }
    }
}
