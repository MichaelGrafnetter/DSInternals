using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DSInternals.Common.Interop
{
    /// <summary>
    /// Represents a wrapper class for...
    /// </summary>

    public sealed class SafeUnicodeSecureStringPointer : SafeHandleZeroOrMinusOneIsInvalid
    {
        private int numChars = 0;

        public SafeUnicodeSecureStringPointer(SecureString password)
            : base(true)
        {
            if (password != null)
            {
                IntPtr pointer = Marshal.SecureStringToGlobalAllocUnicode(password);
                this.SetHandle(pointer);
                this.numChars = password.Length;
            }
        }

        public SafeUnicodeSecureStringPointer(byte[] password)
            : base(true)
        {
            if (password != null)
            {
                if(password.Length % sizeof(char) == 1)
                {
                    // Unicode strings must have even number of bytes
                    new ArgumentOutOfRangeException(nameof(password));
                }

                IntPtr buffer = Marshal.AllocHGlobal(password.Length + sizeof(char));
                Marshal.Copy(password, 0, buffer, password.Length);

                // Add the trailing zero
                Marshal.WriteInt16(buffer, password.Length, 0);

                this.SetHandle(buffer);
                this.numChars = password.Length / sizeof(char);
            }
        }

        public int NumChars
        {
            get
            {
                if (!this.IsInvalid)
                {
                    return numChars;
                }
                else
                {
                    return 0;
                }
            }
        }

        // TODO: Rename Length and NumBytesTotal to something better.
        public int NumBytes
        {
            get
            {
                // Unicode => 2 bytes per character, omitting the terminating zero
                return this.NumBytesTotal - sizeof(char);
            }
        }

        public int NumBytesTotal
        {
            get
            {
                // Unicode => 2 bytes per character, including the terminating zero
                return Encoding.Unicode.GetMaxByteCount(this.NumChars);
            }
        }

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            Marshal.ZeroFreeGlobalAllocUnicode(handle);
            return true;
        }

        public override string ToString()
        {
            return Marshal.PtrToStringUni(this.handle);
        }
    }
}
