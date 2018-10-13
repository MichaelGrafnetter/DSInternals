namespace DSInternals.SAM.Interop
{
    using DSInternals.Common.Interop;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct LsaDnsDomainInformationNative
    {
        /// <summary>
        /// Name of the primary domain.
        /// </summary>
        internal UnicodeString Name;

        /// <summary>
        /// DNS name of the primary domain.
        /// </summary>
        internal UnicodeString DnsDomainName;

        /// <summary>
        /// DNS forest name of the primary domain. This is the DNS name of the domain at the root of the enterprise.
        /// </summary>
        internal UnicodeString DnsForestName;

        /// <summary>
        /// The GUID of the primary domain.
        /// </summary>
        internal Guid DomainGuid;

        /// <summary>
        /// The SID of the primary domain.
        /// </summary>
        internal IntPtr Sid;
    }
}