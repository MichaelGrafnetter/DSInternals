using DSInternals.SAM.Interop;
using System;
using System.Security;

namespace DSInternals.SAM
{
    public abstract class SamObject : IDisposable
    {
        protected SafeSamHandle Handle
        {
            get;
            set;
        }

        protected SamObject(SafeSamHandle handle)
        {
            this.Handle = handle;
        }

        /// <summary>
        /// ReleaseHandle implementation.
        /// </summary>
        public void ReleaseHandle()
        {
            if (this.Handle != null)
            {
                this.Handle.Dispose();
                this.Handle = null;
            }
        }
        /// <summary>
        /// Dispose implementation.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                this.ReleaseHandle();
            }
        }
    }
}
