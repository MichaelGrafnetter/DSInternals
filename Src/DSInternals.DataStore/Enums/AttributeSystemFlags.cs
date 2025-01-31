using System;

namespace DSInternals.DataStore
{
    /// <summary>
    /// An integer value that contains flags that define additional properties of the attribute. 
    /// </summary>
    [Flags]
    public enum AttributeSystemFlags
    {
        None = 0,
        /// <summary>
        /// The attribute will not be replicated.
        /// </summary>
        NotReplicated = 1,
        /// <summary>
        ///  If set, this attribute is a member of partial attribute set (PAS) regardless of the value of attribute isMemberofPartialAttributeSet.
        /// </summary>
        RequiredInPartialSet = 2,
        /// <summary>
        /// The attribute is constructed.
        /// </summary>
        Constructed = 4,
        /// <summary>
        /// This attribute is an operational attribute, as defined in [RFC2251] section 3.2.1.
        /// </summary>
        Operational = 8,
        /// <summary>
        /// When set, indicates the object is a category 1 object. A category 1 object is a class or attribute that is included in the base schema included with the system.
        /// </summary>
        Base = 16,
        /// <summary>
        ///  This attribute can be used as an RDN attribute of a class.
        /// </summary>
        Rdn = 32,
        /// <summary>
        ///  The attribute cannot be renamed.
        /// </summary>
        DisallowRename = 0x8000000,
    }
}
