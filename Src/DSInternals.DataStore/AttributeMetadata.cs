using System;
using DSInternals.Common;

namespace DSInternals.DataStore
{
    /// <summary>
    /// The AttributeMetadata class is used to contain replication metadata for an Active Directory Domain Services attribute.
    /// </summary>
    /// <remarks>Note that AttributeId is held in the parent collection.</remarks>
    /// <see>https://msdn.microsoft.com/en-us/library/system.directoryservices.activedirectory.attributemetadata.aspx</see>
    public class AttributeMetadata
    {
        private const int DefaultVersion = 1;

        public AttributeMetadata(Guid invocationId, DateTime time, long usn)
        {
            this.Version = DefaultVersion;
            this.LastOriginatingChangeTime = time;
            this.LastOriginatingInvocationId = invocationId;
            this.OriginatingChangeUsn = usn;
            this.LocalChangeUsn = usn;
        }

        public AttributeMetadata(int version, long timestamp, Guid originatingDSA, long originatingUSN, long localUSN)
        {
            this.Version = version;
            this.LastOriginatingChangeTimestamp = timestamp;
            this.LastOriginatingInvocationId = originatingDSA;
            this.OriginatingChangeUsn = originatingUSN;
            this.LocalChangeUsn = localUSN;
        }

        /// <summary>
        /// Gets or sets the version number of this attribute.
        /// </summary>
        public int Version
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the timestamp at which the last originating change was made to this attribute.
        /// </summary>
        public long LastOriginatingChangeTimestamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the invocation identifier of the server on which the last change was made to this attribute.
        /// </summary>
        public Guid LastOriginatingInvocationId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the update sequence number (USN) on the originating server at which the last change to this attribute was made.
        /// </summary>
        public long OriginatingChangeUsn
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the update sequence number (USN) on the destination server at which the last change to this attribute was applied.
        /// </summary>
        public long LocalChangeUsn
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the time at which the last originating change was made to this attribute.
        /// </summary>
        public DateTime LastOriginatingChangeTime
        {
            get
            {
                DateTime result = this.LastOriginatingChangeTimestamp.FromGeneralizedTime();
                return result;
            }
            private set
            {
                this.LastOriginatingChangeTimestamp = value.ToGeneralizedTime();
            }
        }

        public void Update(Guid invocationId, DateTime time, long usn)
        {
            this.LastOriginatingInvocationId = invocationId;
            this.LocalChangeUsn = usn;
            this.OriginatingChangeUsn = usn;
            this.LastOriginatingChangeTime = time;
            this.Version++;
        }

        public override string ToString()
        {
            return String.Format("Ver: {0}, USN: {1}, Time: {2}, DSA: {3}", this.Version, this.OriginatingChangeUsn, this.LastOriginatingChangeTime, this.LastOriginatingInvocationId);
        }
    }
}
