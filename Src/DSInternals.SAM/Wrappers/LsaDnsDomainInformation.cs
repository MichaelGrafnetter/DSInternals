using System.Security.Principal;
using DSInternals.SAM.Interop;

namespace DSInternals.SAM
{
    /// <summary>
    /// Represents information about the primary domain of a LSA server.
    /// </summary>
    public struct LsaDnsDomainInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LsaDnsDomainInformation"/> struct.
        /// </summary>
        public LsaDnsDomainInformation() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LsaDnsDomainInformation"/> struct from a native structure.
        /// </summary>
        /// <param name="nativeInfo">The native structure containing domain information.</param>
        internal LsaDnsDomainInformation(LsaDnsDomainInformationNative nativeInfo)
        {
            this.Name = nativeInfo.Name.Buffer;
            this.DnsDomainName = nativeInfo.DnsDomainName.Buffer;
            this.DnsForestName = nativeInfo.DnsForestName.Buffer;
            this.Guid = nativeInfo.DomainGuid != System.Guid.Empty ? nativeInfo.DomainGuid : (Guid?)null;
            this.Sid = nativeInfo.GetDomainSid();
        }

        /// <summary>
        /// Name of the primary domain.
        /// </summary>
        public string Name;

        /// <summary>
        /// DNS name of the primary domain.
        /// </summary>
        public string DnsDomainName;

        /// <summary>
        /// DNS forest name of the primary domain. This is the DNS name of the domain at the root of the enterprise.
        /// </summary>
        public string DnsForestName;

        /// <summary>
        /// The GUID of the primary domain.
        /// </summary>
        public Guid? Guid;

        /// <summary>
        /// The SID of the primary domain.
        /// </summary>
        public SecurityIdentifier Sid;
    }
}
