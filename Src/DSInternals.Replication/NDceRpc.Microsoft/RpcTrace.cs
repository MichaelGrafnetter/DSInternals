using System;

namespace NDceRpc
{
    internal static class RpcTrace
    {
        public static void Verbose(string message)
        {
        }

        public static void Verbose(string message, params object[] arguments)
        {
        }

        public static void Warning(string message)
        {
        }

        public static void Warning(string message, params object[] arguments)
        {
        }

        public static void Error(string message)
        {
        }

        public static void Error(Exception error)
        {
        }

        public static void Error(string message, params object[] arguments)
        {
        }
    }
}