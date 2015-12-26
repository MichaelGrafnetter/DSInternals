using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.SAM.Interop
{
    /// <summary>
    /// Represents a wrapper class for a handle that points
    /// to a structure containing the client's authentication and authorization
    /// credentials specified for remote procedure calls.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/windows/desktop/aa378492(v=vs.85).aspx</see>
    [SecurityCritical]
    public class SafeRpcAuthIdentityHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeRpcAuthIdentityHandle()
            : base(true)
        {
        }

        public SafeRpcAuthIdentityHandle(IntPtr preexistingHandle, bool ownsHandle)
            : base(ownsHandle)
        {
            this.SetHandle(preexistingHandle);
        }

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            NativeMethods.DsFreePasswordCredentials(this.handle);
            // Always return success
            return true;
        }
    }
}
