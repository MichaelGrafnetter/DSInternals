using DSInternals.Common.Schema;

namespace DSInternals.Replication.Model;

/// <summary>
/// Represents an attribute of a directory object in a domain controller.
/// </summary>
public sealed class ReplicaAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReplicaAttribute"/> class.
    /// </summary>
    /// <param name="id">The attribute type.</param>
    /// <param name="values">The attribute values in binary format.</param>
    public ReplicaAttribute(AttributeType id, byte[][] values)
    {
        this.Id = id;
        this.Values = values;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReplicaAttribute"/> class.
    /// </summary>
    /// <param name="id">The attribute type.</param>
    /// <param name="value">The attribute value in binary format.</param>
    public ReplicaAttribute(AttributeType id, byte[] value)
    {
        this.Id = id;
        this.Values = new byte[1][];
        this.Values[0] = value;
    }

    /// <summary>
    /// The attribute type.
    /// </summary>
    public AttributeType Id
    {
        get;
        private set;
    }

    /// <summary>
    /// The attribute values in binary format.
    /// </summary>
    public byte[][] Values
    {
        get;
        private set;
    }
}
