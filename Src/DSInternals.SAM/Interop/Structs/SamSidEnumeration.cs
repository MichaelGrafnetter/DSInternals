using DSInternals.Common.Interop;
using System;
using System.Runtime.InteropServices;

namespace DSInternals.SAM.Interop
{
    // TODO: Is this struct really needed?
    [StructLayout(LayoutKind.Sequential)]
    internal struct SamSidEnumeration
    {
        internal IntPtr Sid;
        // TODO: Sanitize
        internal IntPtr a;
        internal IntPtr Name;
    }
}
