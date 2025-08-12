using System;

namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// Base class for RPC handles
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Handle}")]
    public abstract class RpcHandle : IDisposable
    {
        internal IntPtr Handle;

        ~RpcHandle()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            try
            {
                RpcTrace.Verbose("RpcHandle.Dispose on {0}", Handle);

                if (Handle != IntPtr.Zero)
                {
                    DisposeHandle(ref Handle);
                }
            }
            finally
            {
                Handle = IntPtr.Zero;
            }
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        protected abstract void DisposeHandle(ref IntPtr handle);

        protected bool Equals(RpcHandle other)
        {
            return Handle.Equals(other.Handle);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            if (Handle == IntPtr.Zero) return false;
            return Equals((RpcHandle) obj);
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
    }
}