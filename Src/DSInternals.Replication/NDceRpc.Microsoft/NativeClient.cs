using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDceRpc.Native
{
    /// <summary>
    /// Represents a NativeClient.
    /// </summary>
    public class NativeClient:Client
    {
        /// <summary>
        /// base implementation.
        /// </summary>
        public NativeClient(EndpointBindingInfo info) : base(info)
        {
            //string szStringBinding = Client.StringBindingCompose(clientInfo, null);

            //IntPtr bindingHandle;
            //status = NativeMethods.RpcBindingFromStringBinding(
            //   szStringBinding, // The string binding to validate.
            //   out bindingHandle); // Put the result in the explicit binding handle.


            //_handle = GCHandle.Alloc(bindingHandle, GCHandleType.Pinned);
            //Guard.Assert(status);
        }

        public IntPtr Binding
        {
          get { return _handle.Handle; }
        }
    }
}
