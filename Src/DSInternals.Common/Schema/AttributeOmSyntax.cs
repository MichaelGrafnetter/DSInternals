namespace DSInternals.Common.Schema;

public enum AttributeOmSyntax : int
{
    Undefined = 0,
    Boolean = 1,
    Integer = 2,
    OctetString = 4,
    ObjectIdentifierString = 6,
    Enumeration = 10,
    NumericString = 18,
    PrintableString = 19,
    TeletexString = 20,
    IA5String = 22,
    UtcTimeString = 23,
    GeneralisedTimeString = 24,
    UnicodeString = 64,
    LargeInteger = 65,
    ObjectSecurityDescriptor = 66,
    Object = 127,
}
