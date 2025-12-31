using System.Runtime.Serialization;

namespace DSInternals.Replication.Model;

/// <summary>
/// Represents a replication cookie used to track the state of replication.
/// </summary>
[DataContract]
public sealed class ReplicationCookie : IEquatable<ReplicationCookie>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReplicationCookie"/> class.
    /// </summary>
    /// <param name="namingContext">The naming context.</param>
    public ReplicationCookie(string namingContext)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(namingContext);
        this.NamingContext = namingContext;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReplicationCookie"/> class.
    /// </summary>
    /// <param name="namingContext">The naming context.</param>
    /// <param name="invocationId">The unique identifier of the replication invocation.</param>
    /// <param name="highObjectUpdate">The highest object update sequence number (USN).</param>
    /// <param name="highPropUpdate">The highest property update sequence number (USN).</param>
    /// <param name="reserved">Reserved for future use.</param>
    public ReplicationCookie(string namingContext, Guid invocationId, Int64 highObjectUpdate, Int64 highPropUpdate, Int64 reserved)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(namingContext);
        this.NamingContext = namingContext;
        this.InvocationId = invocationId;
        this.HighObjUpdate = highObjectUpdate;
        this.HighPropUpdate = highPropUpdate;
        this.Reserved = reserved;
    }

    /// <summary>
    /// The naming context.
    /// </summary>
    [DataMember]
    public string NamingContext
    {
        get;
        private set;
    }

    /// <summary>
    /// The unique identifier of the replication invocation.
    /// </summary>
    [DataMember]
    public Guid InvocationId
    {
        get;
        private set;
    }

    /// <summary>
    /// The highest object update sequence number (USN).
    /// </summary>
    [DataMember]
    public Int64 HighObjUpdate
    {
        get;
        private set;
    }

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    [DataMember]
    public Int64 Reserved
    {
        get;
        private set;
    }

    /// <summary>
    /// The highest property update sequence number (USN).
    /// </summary>
    [DataMember]
    public Int64 HighPropUpdate
    {
        get;
        private set;
    }

    /// <summary>
    /// Indicates whether this is an initial cookie (all USNs are zero).
    /// </summary>
    public bool IsInitial
    {
        get
        {
            return this.HighObjUpdate == 0 && this.HighPropUpdate == 0 && this.Reserved == 0;
        }
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        // We simply XOR the hash codes of all members
        return this.HighObjUpdate.GetHashCode() ^
               this.HighPropUpdate.GetHashCode() ^
               this.InvocationId.GetHashCode() ^
               this.NamingContext.GetHashCode() ^
               this.Reserved.GetHashCode();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>Value indicating whether the specified object is equal to the current object.</returns>
    public override bool Equals(object obj)
    {
        // If parameter is null return false.
        if (obj == null)
        {
            return false;
        }

        // If parameter cannot be cast to ReplicationCookie return false.
        ReplicationCookie cookie = obj as ReplicationCookie;
        if ((object)cookie == null)
        {
            return false;
        }

        // Return true if the properties match:
        return MemberwiseEquals(this, cookie);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="cookie">The cookie to compare with.</param>
    /// <returns>Value indicating whether the current object is equal to the specified cookie.</returns>
    public bool Equals(ReplicationCookie cookie)
    {
        // If parameter is null return false:
        if ((object)cookie == null)
        {
            return false;
        }

        // Return true if the properties match:
        return MemberwiseEquals(this, cookie);
    }

    /// <summary>
    /// Returns a value indicating whether two specified instances of <see cref="ReplicationCookie"/> are equal.
    /// </summary>
    /// <param name="a">The first instance to compare.</param>
    /// <param name="b">The second instance to compare.</param>
    /// <returns>Value indicating whether the two specified instances of <see cref="ReplicationCookie"/> are equal.</returns>
    public static bool operator ==(ReplicationCookie a, ReplicationCookie b)
    {
        // If both are null, or both are same instance, return true.
        if (Object.ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        // Return true if the properties match:
        return MemberwiseEquals(a, b);
    }

    /// <summary>
    /// Returns a value indicating whether two specified instances of <see cref="ReplicationCookie"/> are not equal.
    /// </summary>
    /// <param name="a">The first instance to compare.</param>
    /// <param name="b">The second instance to compare.</param>
    /// <returns>Value indicating whether the two specified instances of <see cref="ReplicationCookie"/> are not equal.</returns>
    public static bool operator !=(ReplicationCookie a, ReplicationCookie b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Compares two instances of <see cref="ReplicationCookie"/> for memberwise equality.
    /// </summary>
    /// <param name="a">The first instance to compare.</param>
    /// <param name="b">The second instance to compare.</param>
    /// <returns>Value indicating whether the two specified instances of <see cref="ReplicationCookie"/> are equal.</returns>
    private static bool MemberwiseEquals(ReplicationCookie a, ReplicationCookie b)
    {
        return a.HighObjUpdate == b.HighObjUpdate &&
               a.HighPropUpdate == b.HighPropUpdate &&
               a.InvocationId == b.InvocationId &&
               a.NamingContext == b.NamingContext &&
               a.Reserved == b.Reserved;
    }
}
