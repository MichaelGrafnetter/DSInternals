namespace DSInternals.Common.Data
{
    using System;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using DSInternals.Common.Kerberos;
    using DSInternals.Common.Schema;

    /// <summary>
    /// Represents an abstract base class for objects stored in a directory service.
    /// </summary>
    public abstract class DirectoryObject
    {
        protected abstract bool HasBigEndianRid
        {
            get;
        }

        /// <summary>
        /// Determines whether the directory object has the specified attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to check.</param>
        /// <returns>true if the attribute exists; otherwise, false.</returns>
        public abstract bool HasAttribute(string name);
        
        /// <summary>
        /// Reads the value of an attribute as a byte array.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute value or null if the attribute does not exist.</param>
        public abstract void ReadAttribute(string name, out byte[] value);
        
        /// <summary>
        /// Reads the values of a multi-valued attribute as an array of byte arrays.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute values or null if the attribute does not exist.</param>
        public abstract void ReadAttribute(string name, out byte[][] value);

        /// <summary>
        /// Reads the value of an attribute as a GUID.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute value as a GUID or null if the attribute does not exist.</param>
        public void ReadAttribute(string name, out Guid? value)
        {
            byte[] binaryValue;
            this.ReadAttribute(name, out binaryValue);
            value = (binaryValue != null) ? new Guid(binaryValue) : (Guid?)null;
        }
        /// <summary>
        /// Reads the value of an attribute as a boolean.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute value as a boolean.</param>
        public void ReadAttribute(string name, out bool value)
        {
            int? numericValue;
            this.ReadAttribute(name, out numericValue);
            value = numericValue.HasValue ? (numericValue.Value != 0) : false;
        }
        /// <summary>
        /// Reads the value of an attribute as a nullable integer.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute value as an integer or null if the attribute does not exist.</param>
        public abstract void ReadAttribute(string name, out int? value);
        
        /// <summary>
        /// Reads the value of an attribute as a nullable long integer.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute value as a long integer or null if the attribute does not exist.</param>
        public abstract void ReadAttribute(string name, out long? value);
        /// <summary>
        /// Reads the value of an attribute as a string.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute value as a string or null if the attribute does not exist.</param>
        /// <param name="unicode">Indicates whether to use Unicode encoding for the string.</param>
        public abstract void ReadAttribute(string name, out string value, bool unicode = true);
        
        /// <summary>
        /// Reads the values of a multi-valued attribute as an array of strings.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="values">When this method returns, contains the attribute values as an array of strings or null if the attribute does not exist.</param>
        /// <param name="unicode">Indicates whether to use Unicode encoding for the strings.</param>
        public abstract void ReadAttribute(string name, out string[] values, bool unicode = true);

        /// <summary>
        /// Reads the value of an attribute as a security descriptor.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute value as a security descriptor or null if the attribute does not exist.</param>
        public virtual void ReadAttribute(string name, out RawSecurityDescriptor value)
        {
            byte[] binarySecurityDescriptor;
            this.ReadAttribute(name, out binarySecurityDescriptor);
            value = (binarySecurityDescriptor != null) ? new RawSecurityDescriptor(binarySecurityDescriptor, 0) : null;
        }

        /// <summary>
        /// Reads the linked values of an attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute to read.</param>
        /// <param name="values">When this method returns, contains the linked values as an array of byte arrays.</param>
        public abstract void ReadLinkedValues(string attributeName, out byte[][] values);

        /// <summary>
        /// Gets the distinguished name of the directory object.
        /// </summary>
        public abstract string DistinguishedName
        {
            get;
        }

        /// <summary>
        /// Gets the GUID of the directory object.
        /// </summary>
        public abstract Guid Guid
        {
            get;
        }

        /// <summary>
        /// Gets the security identifier (SID) of the directory object.
        /// </summary>
        public abstract SecurityIdentifier Sid
        {
            get;
        }

        /// <summary>
        /// Reads the value of an attribute as a security identifier.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute value as a security identifier.</param>
        public void ReadAttribute(string name, out SecurityIdentifier value)
        {
            byte[] binarySid;
            this.ReadAttribute(name, out binarySid);
            value = binarySid.ToSecurityIdentifier(this.HasBigEndianRid);
        }

        /// <summary>
        /// Reads the value of an attribute as a distinguished name.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute value as a distinguished name.</param>
        public abstract void ReadAttribute(string name, out DistinguishedName value);

        /// <summary>
        /// Reads the values of a multi-valued attribute as an array of security identifiers.
        /// </summary>
        /// <param name="name">The name of the attribute to read.</param>
        /// <param name="value">When this method returns, contains the attribute values as an array of security identifiers.</param>
        public void ReadAttribute(string name, out SecurityIdentifier[] value)
        {
            value = null;
            // TODO: Always big endian?
            this.ReadAttribute(name, out byte[][] binarySids);
            if (binarySids != null)
            {
                value = binarySids.Select(binarySid => binarySid.ToSecurityIdentifier(this.HasBigEndianRid)).ToArray();
            }
        }

        /// <summary>
        /// Reads the value of an attribute as a SecurityIdentifier.
        /// </summary>
        public void ReadAttribute(string name, out SamAccountType? value)
        {
            this.ReadAttribute(name, out int? numericValue);
            value = (SamAccountType?)numericValue;
        }

        /// <summary>
        /// Reads the value of an attribute as a SecurityIdentifier.
        /// </summary>
        public void ReadAttribute(string name, out TrustDirection? value)
        {
            this.ReadAttribute(name, out int? numericValue);
            value = (TrustDirection?)numericValue;
        }

        /// <summary>
        /// Reads the value of an attribute as a SecurityIdentifier.
        /// </summary>
        public void ReadAttribute(string name, out TrustAttributes? value)
        {
            this.ReadAttribute(name, out int? numericValue);
            value = (TrustAttributes?)numericValue;
        }

        /// <summary>
        /// Reads the value of an attribute as a SecurityIdentifier.
        /// </summary>
        public void ReadAttribute(string name, out TrustType? value)
        {
            this.ReadAttribute(name, out int? numericValue);
            value = (TrustType?)numericValue;
        }

        /// <summary>
        /// Reads the value of an attribute as a SecurityIdentifier.
        /// </summary>
        public void ReadAttribute(string name, out DateTime? value, bool asGeneralizedTime)
        {
            value = null;
            long? timestamp;
            this.ReadAttribute(name, out timestamp);
            if(timestamp.HasValue && timestamp.Value > 0)
            {
                value = asGeneralizedTime ? timestamp.Value.FromGeneralizedTime() : DateTime.FromFileTime(timestamp.Value); 
            }
        }

        public bool IsDeleted
        {
            get
            {
                bool result;
                this.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out result);
                return result;
            }
        }

        public InstanceType? InstanceType
        {
            get
            {
                int? result;
                this.ReadAttribute(CommonDirectoryAttributes.InstanceType, out result);
                return (InstanceType?)result;
            }
        }

        public bool IsWritable
        {
            get
            {
                var instanceType = this.InstanceType;
                return instanceType.HasValue && instanceType.Value.HasFlag(DSInternals.Common.Data.InstanceType.Writable);
            }
        }
    }
}
