namespace DSInternals.SAM
{
    using DSInternals.SAM.Interop;
    using System;
    using System.Security.Principal;

    public class LsaDnsDomainInformation
    {
        public LsaDnsDomainInformation() { }

        internal LsaDnsDomainInformation(LsaDnsDomainInformationNative nativeInfo)
        {
            
            this.Name = nativeInfo.Name.Buffer;
            this.DnsDomainName = nativeInfo.DnsDomainName.Buffer;
            this.DnsForestName = nativeInfo.DnsForestName.Buffer;
            this.Guid = nativeInfo.DomainGuid != System.Guid.Empty ? nativeInfo.DomainGuid : (Guid?)null;
            this.Sid = nativeInfo.Sid != IntPtr.Zero ? new SecurityIdentifier(nativeInfo.Sid) : null;
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