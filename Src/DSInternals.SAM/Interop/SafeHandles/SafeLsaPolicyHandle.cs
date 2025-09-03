namespace DSInternals.SAM.Interop
{
    using DSInternals.Common.Interop;
    using Microsoft.Win32.SafeHandles;
    using System;

    internal sealed class SafeLsaPolicyHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// base implementation.
        /// </summary>
        public SafeLsaPolicyHandle() : base(true)
        {
        }

        /// <summary>
        /// base implementation.
        /// </summary>
        public SafeLsaPolicyHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
        {
            this.SetHandle(preexistingHandle);
        }

        override protected bool ReleaseHandle()
        {
            return NativeMethods.LsaClose(this.handle) == NtStatus.Success;
        }
    }
}