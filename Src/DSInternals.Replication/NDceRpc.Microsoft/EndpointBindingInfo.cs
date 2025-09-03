using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc
{

    ///<seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa378481.aspx"/>
    [StructLayout(LayoutKind.Sequential)]
    [System.Diagnostics.DebuggerDisplay("{Protseq} {NetworkAddr} {EndPoint}")]
    /// <summary>
    /// Represents a EndpointBindingInfo structure.
    /// </summary>
    public struct EndpointBindingInfo:ICloneable
    {
        /// <summary>
        /// The Protseq.
        /// </summary>
        public RpcProtseq Protseq;
        /// <summary>
        /// The NetworkAddr.
        /// </summary>
        public string NetworkAddr;
        /// <summary>
        /// The EndPoint.
        /// </summary>
        public string EndPoint;

        public EndpointBindingInfo(RpcProtseq protseq, string networkAddr, string endPoint)
        {
            Protseq = protseq;
            NetworkAddr = networkAddr;
            EndPoint = endPoint;
        }

        /// <summary>
        /// Clone implementation.
        /// </summary>
        public object Clone()
        {
            return new EndpointBindingInfo(Protseq, NetworkAddr, EndPoint);
        }
    }
}
