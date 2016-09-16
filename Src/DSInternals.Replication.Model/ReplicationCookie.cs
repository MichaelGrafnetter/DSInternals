using DSInternals.Common;
using System;
using System.Runtime.Serialization;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// USN Vector
    /// </summary>
    [DataContract]
    public sealed class ReplicationCookie
    {
        public ReplicationCookie(string namingContext)
        {
            Validator.AssertNotNullOrWhiteSpace(namingContext, "namingContext");
            this.NamingContext = namingContext;
        }
        public ReplicationCookie(string namingContext, Guid invocationId, Int64 highObjectUpdate, Int64 highPropUpdate, Int64 reserved)
        {
            Validator.AssertNotNullOrWhiteSpace(namingContext, "namingContext");
            this.NamingContext = namingContext;
            this.InvocationId = invocationId;
            this.HighObjUpdate = highObjectUpdate;
            this.HighPropUpdate = highPropUpdate;
            this.Reserved = reserved;
        }

        [DataMember]
        public string NamingContext
        {
            get;
            private set;
        }

        [DataMember]
        public Guid InvocationId
        {
            get;
            private set;
        }

        [DataMember]
        public Int64 HighObjUpdate
        {
            get;
            private set;
        }

        [DataMember]
        public Int64 Reserved
        {
            get;
            private set;
        }

        [DataMember]
        public Int64 HighPropUpdate
        {
            get;
            private set;
        }

        public override int GetHashCode()
        {
            // We simply XOR the hash codes of all members
            return this.HighObjUpdate.GetHashCode() ^
                   this.HighPropUpdate.GetHashCode() ^
                   this.InvocationId.GetHashCode() ^
                   this.NamingContext.GetHashCode() ^
                   this.Reserved.GetHashCode();
        }

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

        public static bool operator !=(ReplicationCookie a, ReplicationCookie b)
        {
            return !(a == b);
        }

        private static bool MemberwiseEquals(ReplicationCookie a, ReplicationCookie b)
        {
            return a.HighObjUpdate  == b.HighObjUpdate  &&
                   a.HighPropUpdate == b.HighPropUpdate &&
                   a.InvocationId   == b.InvocationId   &&
                   a.NamingContext  == b.NamingContext  &&
                   a.Reserved       == b.Reserved;
        }
    }
}
