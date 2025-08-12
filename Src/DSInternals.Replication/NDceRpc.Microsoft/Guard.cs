using System;
using System.Reflection;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc
{
    /// <summary>
    /// provides a set of runtime validations for inputs
    /// </summary>
    static  class Guard
    {
        /// <summary>
        /// Verifies that the condition is true and if it fails constructs the specified type of
        /// exception and throws.
        /// </summary>
        public static void Assert<TException>(bool condition)
            where TException : Exception, new()
        {
            if (!condition)
                throw new TException();
        }

        /// <summary>
        /// Verifies that the condition is true and if it fails constructs the specified type of
        /// exception with any arguments provided and throws.
        /// </summary>
        public static void Assert<TException>(bool condition, string message)
            where TException : Exception, new()
        {
            if (!condition)
            {
                ConstructorInfo ci = typeof (TException).GetConstructor(new Type[] {typeof (string)});
                if (ci != null)
                {
                    TException e = (TException) ci.Invoke(new object[] {message});
                    throw e;
                }
                throw new TException();
            }
        }

        /// <summary>
        /// Verifies that value is not null and returns the value or throws ArgumentNullException
        /// </summary>
        public static T NotNull<T>(T value)
        {
            if (value == null) throw new ArgumentNullException();
            return value;
        }

        [System.Diagnostics.DebuggerNonUserCode]
        internal static void Assert(long rawError)
        {
            Assert((RPC_STATUS)rawError);
        }
   
        [System.Diagnostics.DebuggerNonUserCode]
        internal static void Assert(int rawError)
        {
            Assert((RPC_STATUS)rawError);
        }

        /// <summary>
        /// Asserts that the argument is set to RPC_STATUS.RPC_S_OK or throws a new exception.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        public static void Assert(RPC_STATUS errorsCode)
        {
            if (errorsCode != RPC_STATUS.RPC_S_OK)
            {
                RpcException ex = new RpcException(errorsCode);
                RpcTrace.Error("RPC_STATUS.{0} - {1}", errorsCode, ex.Message);
                throw ex;
            }
        }
    }
}