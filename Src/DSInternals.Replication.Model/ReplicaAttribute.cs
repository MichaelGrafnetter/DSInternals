using DSInternals.Common.Schema;

namespace DSInternals.Replication.Model;

/// <summary>
/// Represents an attribute of a directory object in a domain controller.
/// </summary>
public sealed class ReplicaAttribute
{
    /// <summary>
    /// The attribute type.
    /// </summary>
    public readonly AttributeType Id;

    /// <summary>
    /// The attribute values in binary format.
    /// </summary>
    public readonly byte[][] Values;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReplicaAttribute"/> class.
    /// </summary>
    /// <param name="id">The attribute type.</param>
    /// <param name="values">The attribute values in binary format.</param>
    public ReplicaAttribute(AttributeType id, byte[][] values)
    {
        Id = id;
        Values = values;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReplicaAttribute"/> class.
    /// </summary>
    /// <param name="id">The attribute type.</param>
    /// <param name="value">The attribute value in binary format.</param>
    public ReplicaAttribute(AttributeType id, byte[] value)
    {
        Id = id;
        Values = new byte[1][];
        Values[0] = value;
    }
}
