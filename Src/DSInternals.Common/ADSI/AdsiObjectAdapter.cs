namespace DSInternals.Common.ADSI
{
    using DSInternals.Common.Data;
    using System;
    using System.DirectoryServices;
    using System.Linq;
    using System.Security.Principal;

    public class AdsiObjectAdapter : DirectoryObject
    {
        protected SearchResult directoryEntry;

        public AdsiObjectAdapter(SearchResult directoryEntry)
        {
            Validator.AssertNotNull(directoryEntry, "directoryEntry");
            this.directoryEntry = directoryEntry;
        }

        public override string DistinguishedName
        {
            get
            {
                return this.ReadAttributeSingle<string>(CommonDirectoryAttributes.DN);
            }
        }

        public override Guid Guid
        {
            get
            {
                Guid? guid;
                this.ReadAttribute(CommonDirectoryAttributes.ObjectGUID, out guid);
                return guid.Value;
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
                return false;
            }
        }

        public override bool HasAttribute(string name)
        {
            return this.directoryEntry.Properties.Contains(name);
        }

        public override void ReadAttribute(string name, out byte[] value)
        {
            value = this.ReadAttributeSingle<byte[]>(name);
        }

        public override void ReadAttribute(string name, out byte[][] value)
        {
            value = this.ReadAttributeMulti<byte[]>(name);
        }

        public override void ReadAttribute(string name, out int? value)
        {
            value = this.ReadAttributeSingle<int?>(name);
        }

        public override void ReadAttribute(string name, out long? value)
        {
            value = this.ReadAttributeSingle<long?>(name);
        }

        public override void ReadAttribute(string name, out string value, bool unicode = true)
        {
            // Unicode vs. IA5 strings are handled by ADSI itself.
            value = this.ReadAttributeSingle<string>(name);
        }

        public override void ReadAttribute(string name, out string[] values, bool unicode = true)
        {
            // Unicode vs. IA5 strings are handled by ADSI itself.
            values = this.ReadAttributeMulti<string>(name);
        }

        public override void ReadAttribute(string name, out DistinguishedName value)
        {
            string dnString = this.ReadAttributeSingle<string>(name);
            value = new DistinguishedName(dnString);
        }

        public override void ReadLinkedValues(string attributeName, out byte[][] values)
        {
            // Parse the DN with binary value
            string[] textValues = this.ReadAttributeMulti<string>(attributeName);
            values = textValues?.Select(textValue => DNWithBinary.Parse(textValue).Binary).ToArray();
        }

        protected TResult ReadAttributeSingle<TResult>(string name)
        {
            return this.directoryEntry.Properties[name].Cast<TResult>().FirstOrDefault();
        }

        protected TResult[] ReadAttributeMulti<TResult>(string name)
        {
            var result = this.directoryEntry.Properties[name].Cast<TResult>().ToArray();
            if(result != null && result.Length == 0)
            {
                // We do not want to return an empty array.
                result = null;
            }
            return result;
        }
    }
}
