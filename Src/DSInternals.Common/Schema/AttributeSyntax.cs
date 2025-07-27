
namespace DSInternals.Common.Schema
{
    /// <summary>
    /// Any OID-valued quantity is stored as a 32-bit unsigned integer. 
    /// </summary>
    using ATTRTYP = uint;

    /// <summary>
    /// Specifies the data representation (syntax) type of an Attribute object.
    /// </summary>
    /// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-drsr/284c8a5a-6ede-4d34-88ba-bda0b8bb59e0</remarks>
    public enum AttributeSyntax : ATTRTYP
    {
        /// <summary>
        /// Not a legal syntax.
        /// </summary>
        Undefined = 0x80000,

        /// <summary>
        /// Object(DS-DN) - A distinguished name of a directory service object.
        /// </summary>
        /// <remarks>OID 2.5.5.1</remarks>
        DN = Undefined + 1,

        /// <summary>
        /// String(Object-Identifier) - An OID value type.
        /// </summary>
        /// <remarks>OID 2.5.5.2</remarks>
        Oid = Undefined + 2,

        /// <summary>
        /// String(Case) - A case-sensitive string type.
        /// </summary>
        /// <remarks>OID 2.5.5.3</remarks>
        CaseExactString = Undefined + 3,

        /// <summary>
        /// String(Teletex) - A case-insensitive string type.
        /// </summary>
        /// <remarks>OID 2.5.5.4</remarks>
        CaseIgnoreString = Undefined + 4,

        /// <summary>
        /// String(Printable) - Printable character set string or IA5 character set string. 
        /// </summary>
        /// <remarks>OID 2.5.5.5</remarks>
        String = Undefined + 5,

        /// <summary>
        /// String(Numeric) - A numeric value represented as a string.
        /// </summary>
        /// <remarks>OID 2.5.5.6</remarks>
        NumericString = Undefined + 6,

        /// <summary>
        /// Object(DN-Binary) OR Object(OR-Name) - Structure used for mapping a distinguished name to a non-varying GUID.
        /// </summary>
        /// <remarks>OID 2.5.5.7</remarks>
        DNWithBinary = Undefined + 7,

        /// <summary>
        /// Boolean - A Boolean value type.
        /// </summary>
        /// <remarks>OID 2.5.5.8</remarks>
        Bool = Undefined + 8,

        /// <summary>
        /// Enumeration - A 32-bit number or enumeration.
        /// </summary>
        /// <remarks>OID 2.5.5.9</remarks>
        Int = Undefined + 9,

        /// <summary>
        /// String(Octet) - A byte array represented as a string
        /// </summary>
        /// <remarks>OID 2.5.5.10</remarks>
        OctetString = Undefined + 10,

        /// <summary>
        /// String(UTC-Time) OR String(Generalized-Time) - UTC Time or Generalized-Time.
        /// </summary>
        /// <remarks>OID 2.5.5.11</remarks>
        Time = Undefined + 11,

        /// <summary>
        /// String(Unicode) - Unicode string.
        /// </summary>
        /// <remarks>OID 2.5.5.12</remarks>
        UnicodeString = Undefined + 12,

        /// <summary>
        /// Object(Presentation-Address) - Presentation-Address object type.
        /// </summary>
        /// <remarks>OID 2.5.5.13</remarks>
        PresentationAddress = Undefined + 13,

        /// <summary>
        /// Object(Access-Point) OR Object(DN-String) - An structure used for mapping a distinguished name to a non-varying string value.
        /// </summary>
        /// <remarks>OID 2.5.5.14</remarks>
        DNWithString = Undefined + 14,

        /// <summary>
        /// String(NT-Sec-Desc) - A security descriptor value type.
        /// </summary>
        /// <remarks>OID 2.5.5.15</remarks>
        SecurityDescriptor = Undefined + 15,

        /// <summary>
        /// LargeInteger - A 64 bit (large) integer value type.
        /// </summary>
        /// <remarks>OID 2.5.5.16</remarks>
        Int64 = Undefined + 16,

        /// <summary>
        /// String(Sid) - An SID value type.
        /// </summary>
        /// <remarks>OID 2.5.5.17</remarks>
        Sid = Undefined + 17,
    }
}
