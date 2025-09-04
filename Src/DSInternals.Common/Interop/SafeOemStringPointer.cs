using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DSInternals.Common.Interop
{
    /// <summary>
    /// Represents a safe wrapper for OEM string pointers with automatic memory management.
    /// </summary>
    [SecurityCritical]
    public class SafeOemStringPointer : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Gets the length of the allocated OEM string buffer in bytes.
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Allocates a new OEM string pointer with the specified length.
        /// </summary>
        /// <param name="length">The length of the buffer to allocate in bytes.</param>
        /// <returns>A new SafeOemStringPointer instance wrapping the allocated memory.</returns>
        public static SafeOemStringPointer Allocate(int length)
        {
            IntPtr nativeMemory = Marshal.AllocHGlobal(length);
            return new SafeOemStringPointer(nativeMemory, true, length);
        }

        private SafeOemStringPointer()
            : base(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SafeOemStringPointer class with an existing handle, ownership flag, and length.
        /// </summary>
        /// <param name="preexistingHandle">An existing handle to wrap.</param>
        /// <param name="ownsHandle">True if the handle should be released when the wrapper is disposed.</param>
        /// <param name="length">The length of the buffer in bytes.</param>
        public SafeOemStringPointer(IntPtr preexistingHandle, bool ownsHandle, int length)
            : this(preexistingHandle, ownsHandle)
        {
            this.Length = length;
        }

        /// <summary>
        /// Initializes a new instance of the SafeOemStringPointer class with an existing handle and ownership flag.
        /// </summary>
        /// <param name="preexistingHandle">An existing handle to wrap.</param>
        /// <param name="ownsHandle">True if the handle should be released when the wrapper is disposed.</param>
        public SafeOemStringPointer(IntPtr preexistingHandle, bool ownsHandle)
            : base(ownsHandle)
        {
            this.SetHandle(preexistingHandle);
        }

        /// <summary>
        /// Releases the handle by zeroing the buffer and freeing the allocated memory.
        /// </summary>
        /// <returns>True if the handle was released successfully; otherwise, false.</returns>
        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            this.ZeroFill();
            Marshal.FreeHGlobal(this.handle);
            return true;
        }

        /// <summary>
        /// Fills the allocated buffer with zeros for security purposes.
        /// </summary>
        [SecurityCritical]
        protected void ZeroFill()
        {
            // We know the size of the buffer so we can fill it with zeros for security reasons.
            for (int i = 0; i < this.Length; i++)
            {
                Marshal.WriteByte(this.handle, i, byte.MinValue);
            }
        }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        public override string ToString()
        {
            return Marshal.PtrToStringAnsi(this.handle);
        }
    }
}