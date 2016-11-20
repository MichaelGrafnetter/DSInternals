using DSInternals.Common.Interop;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace DSInternals.SAM.Interop
{
    /// <summary>
    /// Represents a wrapper class for enumeration buffers allocated by SAM RPC.
    /// </summary>
    [SecurityCritical]
    internal class SafeSamEnumerationBufferPointer : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeSamEnumerationBufferPointer() : base(true)
        {
        }

        internal SafeSamEnumerationBufferPointer(IntPtr preexistingPointer, bool ownsPointer)
            : base(ownsPointer)
        {
            this.SetHandle(preexistingPointer);
        }

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return NativeMethods.SamIFree_SAMPR_ENUMERATION_BUFFER(this.handle) == NtStatus.Success;
        }

        internal SamRidEnumeration[] ToArray(uint count)
        {
            // TODO: Test that count < int.Max
            var result = new SamRidEnumeration[count];

            for(int i = 0; i < count; i++)
            {
                var currentOffset = i * Marshal.SizeOf<SamRidEnumeration>();
                result[i] = Marshal.PtrToStructure<SamRidEnumeration>(this.handle + currentOffset);
            }
            
            return result;
        }
    }
}
