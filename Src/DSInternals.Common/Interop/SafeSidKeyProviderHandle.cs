using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DSInternals.Common.Interop
{
    /// <summary>
    /// Represents a wrapper class for SID Key Provider object handles.
    /// </summary>
    [SecurityCritical]
    internal class SafeSidKeyProviderHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeSidKeyProviderHandle() : base(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SafeSidKeyProviderHandle class with an existing handle and ownership flag.
        /// </summary>
        /// <param name="preexistingHandle">An existing handle to wrap.</param>
        /// <param name="ownsHandle">True if the handle should be released when the wrapper is disposed.</param>
        public SafeSidKeyProviderHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
        {
            this.SetHandle(preexistingHandle);
        }

        /// <summary>
        /// Converts the handle's memory contents to a byte array of the specified size.
        /// </summary>
        /// <param name="size">The number of bytes to read from the handle.</param>
        /// <returns>A byte array containing the data from the handle.</returns>
        public byte[] ToArray(int size)
        {
            if(this.IsInvalid)
            {
                return null;
            }

            byte[] binaryData = new byte[size];
            Marshal.Copy(this.handle, binaryData, 0, size);

            return binaryData;
        }

        /// <summary>
        /// Gets the string value pointed to by the handle.
        /// </summary>
        public string StringValue
        {
            get
            {
                return this.IsInvalid ? null : Marshal.PtrToStringUni(this.handle);
            }
        }

        /// <summary>
        /// Releases the handle by calling the appropriate native method to free the SID Key Provider.
        /// </summary>
        /// <returns>True if the handle was released successfully; otherwise, false.</returns>
        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            NativeMethods.SIDKeyProvFree(this.handle);

            // Presume that the memory release has been successful.
            return true;
        }
    }
}
