using System;

namespace NDceRpc
{
    internal static class RpcTrace
    {
        /// <summary>
        /// Verbose implementation.
        /// </summary>
        public static void Verbose(string message)
        {
        }

        /// <summary>
        /// Verbose implementation.
        /// </summary>
        public static void Verbose(string message, params object[] arguments)
        {
        }

        /// <summary>
        /// Warning implementation.
        /// </summary>
        public static void Warning(string message)
        {
        }

        /// <summary>
        /// Warning implementation.
        /// </summary>
        public static void Warning(string message, params object[] arguments)
        {
        }

        /// <summary>
        /// Error implementation.
        /// </summary>
        public static void Error(string message)
        {
        }

        /// <summary>
        /// Error implementation.
        /// </summary>
        public static void Error(Exception error)
        {
        }

        /// <summary>
        /// Error implementation.
        /// </summary>
        public static void Error(string message, params object[] arguments)
        {
        }
    }
}