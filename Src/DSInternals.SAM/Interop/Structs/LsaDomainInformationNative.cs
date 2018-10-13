namespace DSInternals.SAM.Interop
{
    using DSInternals.Common.Interop;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct LsaDomainInformationNative
    {
        /// <summary>
        /// Name of the account domain.
        /// </summary>
        internal UnicodeString DomainName;

        /// <summary>
        /// SID of the account domain.
        /// </summary>
        internal IntPtr DomainSid;
    }
}