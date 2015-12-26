
namespace DSInternals.Common.Data
{
    /// <summary>
    /// Specifies the data representation (syntax) type of an Attribute object.
    /// </summary>
    public enum AttributeSyntax : int
    {
        /// <summary>
        /// Not a legal syntax.
        /// </summary>
        Undefined = 0x80000,
        /// <summary>
        /// A distinguished name of a directory service object.
        /// </summary>
        DN = Undefined + 1,
        /// <summary>
        /// An OID value type.
        /// </summary>
        Oid = Undefined + 2,
        /// <summary>
        /// A case-sensitive string type.
        /// </summary>
        CaseExactString = Undefined + 3,
        /// <summary>
        /// A case-insensitive string type.
        /// </summary>
        CaseIgnoreString = Undefined + 4,
        /// <summary>
        /// Printable character set string or IA5 character set string. 
        /// </summary>
        String = Undefined + 5,
        /// <summary>
        /// A numeric value represented as a string.
        /// </summary>
        NumericString = Undefined + 6,
        /// <summary>
        /// An ADS_DN_WITH_BINARY structure used for mapping a distinguished name to a non-varying GUID.
        /// </summary>
        DNWithBinary = Undefined + 7,
        /// <summary>
        /// A Boolean value type.
        /// </summary>
        Bool = Undefined + 8,
        /// <summary>
        /// A 32-bit number or enumeration.
        /// </summary>
        Int = Undefined + 9,
        /// <summary>
        /// A byte array represented as a string
        /// </summary>
        OctetString = Undefined + 10,
        /// <summary>
        /// UTC Time or Generalized-Time.
        /// </summary>
        Time = Undefined + 11,
        /// <summary>
        /// Unicode string.
        /// </summary>
        UnicodeString = Undefined + 12,
        /// <summary>
        /// A Presentation-Address object type.
        /// </summary>
        PresentationAddress = Undefined + 13,
        /// <summary>
        /// An ADS_DN_WITH_STRING structure used for mapping a distinguished name to a non-varying string value.
        /// </summary>
        DNWithString = Undefined + 14,
        /// <summary>
        /// A security descriptor value type.
        /// </summary>
        SecurityDescriptor = Undefined + 15,
        /// <summary>
        /// A 64 bit (large) integer value type.
        /// </summary>
        Int64 = Undefined + 16,
        /// <summary>
        /// An SID value type.
        /// </summary>
        Sid = Undefined + 17,
    }
}
