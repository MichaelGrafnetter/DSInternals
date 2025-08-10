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
    public struct EndpointBindingInfo:ICloneable
    {
        public RpcProtseq Protseq;
        public string NetworkAddr;
        public string EndPoint;

        public EndpointBindingInfo(RpcProtseq protseq, string networkAddr, string endPoint)
        {
            Protseq = protseq;
            NetworkAddr = networkAddr;
            EndPoint = endPoint;
        }

        public object Clone()
        {
            return new EndpointBindingInfo(Protseq, NetworkAddr, EndPoint);
        }
    }
}
