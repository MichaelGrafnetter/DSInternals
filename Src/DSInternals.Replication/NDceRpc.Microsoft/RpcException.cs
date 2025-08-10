using System;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc
{
    /// <summary>
    /// Exception class: RpcException : System.ComponentModel.Win32Exception
    /// Unspecified rpc error
    /// </summary>
    [Serializable()]
    public class RpcException : System.ComponentModel.Win32Exception
    {
        private const string DefaultError = "Unspecified RPC error";

        /// <summary>
        /// Serialization constructor
        /// </summary>
        protected RpcException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Unspecified rpc error
        /// </summary>
        public RpcException()
            : base(DefaultError)
        {
        }

        /// <summary>
        /// Unspecified rpc error
        /// </summary>
        public RpcException(Exception innerException)
            : base(DefaultError, innerException)
        {
        }

        /// <summary>
        /// if(condition == false) throws Unspecified rpc error
        /// </summary>
        public static void Assert(bool condition)
        {
            if (!condition) throw new RpcException();
        }

        /// <summary>
        /// </summary>
        public RpcException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public RpcException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Exception class: RpcException : System.ComponentModel.Win32Exception
        /// Unspecified rpc error
        /// </summary>
        public RpcException(RPC_STATUS errorsCode)
            : base(unchecked((int)errorsCode))
        {
        }

        /// <summary>
        /// Returns the RPC Error as an enumeration
        /// </summary>
        public RPC_STATUS RpcStatus
        {
            get { return (RPC_STATUS)NativeErrorCode; }
        }
    }
}