namespace DSInternals.Common.ADSI
{
    using DSInternals.Common.Data;
    using DSInternals.Common.Schema;
    using System;
    using System.DirectoryServices;
    using System.Linq;
    using System.Security.Principal;

    /// <summary>
    /// Provides an adapter for accessing Active Directory objects through ADSI SearchResult objects.
    /// </summary>
    public class AdsiObjectAdapter : DirectoryObject
    {
        /// <summary>
        /// The underlying SearchResult object from ADSI.
        /// </summary>
        protected SearchResult directoryEntry;

        /// <summary>
        /// Initializes a new instance of the AdsiObjectAdapter class.
        /// </summary>
        /// <param name="directoryEntry">The SearchResult object to wrap.</param>
        public AdsiObjectAdapter(SearchResult directoryEntry)
        {
            Validator.AssertNotNull(directoryEntry, "directoryEntry");
            this.directoryEntry = directoryEntry;
        }

        /// <summary>
        /// Gets the distinguished name of the directory object.
        /// </summary>
        public override string DistinguishedName
        {
            get
            {
                return this.ReadAttributeSingle<string>(CommonDirectoryAttributes.DistinguishedName);
            }
        }

        /// <summary>
        /// Gets the globally unique identifier (GUID) of the directory object.
        /// </summary>
        public override Guid Guid
        {
            get
            {
                Guid? guid;
                this.ReadAttribute(CommonDirectoryAttributes.ObjectGuid, out guid);
                return guid.Value;
            }
        }

        /// <summary>
        /// Gets the security identifier (SID) of the directory object.
        /// </summary>
        public override SecurityIdentifier Sid
        {
            get
            {
                SecurityIdentifier sid;
                this.ReadAttribute(CommonDirectoryAttributes.ObjectSid, out sid);
                return sid;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the directory object uses big-endian byte order for RID values.
        /// </summary>
        protected override bool HasBigEndianRid
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether the directory object has the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to check for.</param>
        /// <returns>True if the attribute exists; otherwise, false.</returns>
        public override bool HasAttribute(string name)
        {
            return this.directoryEntry.Properties.Contains(name);
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out byte[] value)
        {
            value = this.ReadAttributeSingle<byte[]>(name);
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out byte[][] value)
        {
            value = this.ReadAttributeMulti<byte[]>(name);
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out int? value)
        {
            value = this.ReadAttributeSingle<int?>(name);
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out long? value)
        {
            value = this.ReadAttributeSingle<long?>(name);
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out string value, bool unicode = true)
        {
            // Unicode vs. IA5 strings are handled by ADSI itself.
            value = this.ReadAttributeSingle<string>(name);
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out string[] values, bool unicode = true)
        {
            // Unicode vs. IA5 strings are handled by ADSI itself.
            values = this.ReadAttributeMulti<string>(name);
        }

        /// <summary>
        /// Reads the value of the specified attribute.
        /// </summary>
        public override void ReadAttribute(string name, out DistinguishedName value)
        {
            string dnString = this.ReadAttributeSingle<string>(name);
            value = new DistinguishedName(dnString);
        }

        /// <summary>
        /// Reads linked attribute values that contain DN with binary data.
        /// </summary>
        /// <param name="attributeName">The name of the linked attribute to read.</param>
        /// <param name="values">When this method returns, contains the binary values from the linked attribute, or null if the attribute is not present.</param>
        public override void ReadLinkedValues(string attributeName, out byte[][] values)
        {
            // Parse the DN with binary value
            string[] textValues = this.ReadAttributeMulti<string>(attributeName);
            values = textValues?.Select(textValue => DNWithBinary.Parse(textValue).Binary).ToArray();
        }

        /// <summary>
        /// Reads a single-valued attribute from the directory object.
        /// </summary>
        /// <typeparam name="TResult">The type of the attribute value.</typeparam>
        /// <param name="name">The name of the attribute to read.</param>
        /// <returns>The attribute value, or the default value if not present.</returns>
        protected TResult ReadAttributeSingle<TResult>(string name)
        {
            return this.directoryEntry.Properties[name].Cast<TResult>().FirstOrDefault();
        }

        /// <summary>
        /// Reads a multi-valued attribute from the directory object.
        /// </summary>
        /// <typeparam name="TResult">The type of the attribute values.</typeparam>
        /// <param name="name">The name of the attribute to read.</param>
        /// <returns>An array of attribute values, or null if not present or empty.</returns>
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
