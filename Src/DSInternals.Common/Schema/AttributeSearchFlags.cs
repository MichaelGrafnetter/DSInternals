using System;

namespace DSInternals.Common.Schema
{
    /// <summary>
    /// Search flags used on attributes.
    /// </summary>
    /// <remarks>https://msdn.microsoft.com/en-us/library/cc223153.aspx</remarks>
    [Flags]
    public enum AttributeSearchFlags : int
    {
        /// <summary>
        /// No flags set.
        /// </summary>
        None = 0,

        /// <summary>
        /// Index over attribute only.
        /// </summary>
        AttributeIndex = 0x00000001,

        /// <summary>
        /// Index over container and attribute.
        /// </summary>
        ContainerIndex = 0x00000002,

        /// <summary>
        /// Add this attribute to the ambiguous name resolution (ANR) set (should be used in conjunction with 1).
        /// </summary>
        AmbiguousNameResolution = 0x00000004,

        /// <summary>
        /// Preserve this attribute on logical deletion (that is, make this attribute available on tombstones).
        /// </summary>
        PreserveOnDelete = 0x00000008,

        /// <summary>
        /// Include this attribute when copying a user object
        /// </summary>
        Copy = 0x00000010,

        /// <summary>
        /// Create a Tuple index for the attribute to improve medial searches
        /// </summary>
        TupleIndex = 0x00000020,

        /// <summary>
        /// Specifies a hint for the DC to create subtree index for a Virtual List View (VLV) search.
        /// </summary>
        SubtreeIndex = 0x00000040,

        /// <summary>
        /// Specifies that the attribute is confidential. An extended access check is required.
        /// </summary>
        Confidential = 0x00000080,

        /// <summary>
        /// Specifies that auditing of changes to individual values contained in this attribute MUST NOT be performed.
        /// </summary>
        NeverValueAudit = 0x00000100, 

        /// <summary>
        /// Specifies that the attribute is a member of the filtered attribute set.
        /// </summary>
        RODCFilteredAttribute = 0x00000200,

        /// <summary>
        ///  Specifies a hint to the DC to perform additional implementation-specific, nonvisible tracking of link values.
        /// </summary>
        ExtendedLinkTracking = 0x00000400,

        /// <summary>
        /// Specifies that the attribute is not to be returned by search operations that are not scoped to a single object.
        /// </summary>
        BaseOnly = 0x00000800,

        /// <summary>
        /// Specifies that the attribute is a partition secret. An extended access check is required.
        /// </summary>
        PartitionSecret = 0x00001000,
    }
}
