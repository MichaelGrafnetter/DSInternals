using System.ComponentModel;
using System.Net.Sockets;
using System.Security;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Interop;
using Windows.Win32.Foundation;

namespace DSInternals.Common
{
    public static class Validator
    {
        public static void AssertSuccess(NtStatus status)
        {
            Win32ErrorCode code = NativeMethods.RtlNtStatusToDosError(status);
            AssertSuccess(code);
        }

        internal static void AssertSuccess(NTSTATUS status)
        {
            if (status.SeverityCode == NTSTATUS.Severity.Success)
            {
                // No error has occurred
                return;
            }

            if (status == NTSTATUS.STATUS_INVALID_PARAMETER)
            {
                // TODO: Translate NTSTATUS to .NET exceptions
                throw new ArgumentException();
            }

            // TODO: Translate NTSTATUS to .NET exceptions
            throw new Win32Exception();
        }

        public static void AssertSuccess(Win32ErrorCode code)
        {
            switch (code)
            {
                case Win32ErrorCode.Success:
                case Win32ErrorCode.MORE_DATA:
                    // No error occurred, so exit gracefully.
                    return;
            }

            var genericException = new Win32Exception((int)code);
            Exception exceptionToThrow;
            // We will try to translate the generic Win32 exception to a more specific built-in exception.
            switch (code)
            {
                case Win32ErrorCode.DS_INVALID_DN_SYNTAX:
                case Win32ErrorCode.INVALID_PARAMETER:
                    exceptionToThrow = new ArgumentException(genericException.Message, genericException);
                    break;
                case Win32ErrorCode.FILE_NOT_FOUND:
                    exceptionToThrow = new FileNotFoundException(genericException.Message, genericException);
                    break;
                case Win32ErrorCode.ACCESS_DENIED:
                case Win32ErrorCode.DS_DRA_ACCESS_DENIED:
                case Win32ErrorCode.WRONG_PASSWORD:
                case Win32ErrorCode.PASSWORD_EXPIRED:
                    exceptionToThrow = new UnauthorizedAccessException(genericException.Message, genericException);
                    break;
                case Win32ErrorCode.NOT_ENOUGH_MEMORY:
                case Win32ErrorCode.OUTOFMEMORY:
                case Win32ErrorCode.DS_DRA_OUT_OF_MEM:
                case Win32ErrorCode.RPC_S_OUT_OF_RESOURCES:
                    exceptionToThrow = new OutOfMemoryException(genericException.Message, genericException);
                    break;
                case Win32ErrorCode.NO_LOGON_SERVERS:
                case Win32ErrorCode.RPC_S_SERVER_UNAVAILABLE:
                case Win32ErrorCode.RPC_S_CALL_FAILED:
                    exceptionToThrow = new SocketException((int)code);
                    break;
                case Win32ErrorCode.NO_SUCH_DOMAIN:
                case Win32ErrorCode.DS_OBJ_NOT_FOUND:
                // TODO: Add NoneMapped and SomeNotMapped errors
                // This error code means either a non-existing DN or Access Denied.
                case Win32ErrorCode.DS_DRA_BAD_DN:
                    exceptionToThrow = new DirectoryObjectNotFoundException(null, genericException);
                    break;
                // TODO: Add translation for ActiveDirectoryOperationException and for other exception types.
                default:
                    // We were not able to translate the Win32Exception to a more specific type.
                    exceptionToThrow = genericException;
                    break;
            }
            throw exceptionToThrow;
        }

        public static void AssertEquals(string expectedValue, string actualValue, string paramName)
        {
            if (!String.Equals(expectedValue, actualValue, StringComparison.InvariantCulture))
            {
                string message = String.Format("The input contains an unexpected value '{0}', while the expected value is '{1}'.", actualValue, expectedValue);
                throw new ArgumentException(message, paramName);
            }
        }

        public static void AssertEquals(int expectedValue, int actualValue, string paramName)
        {
            if (expectedValue != actualValue)
            {
                string message = String.Format("The input contains an unexpected value '{0}', while the expected value is '{1}'.", actualValue, expectedValue);
                throw new ArgumentException(message, paramName);
            }
        }

        public static void AssertEquals(char expectedValue, char actualValue, string paramName)
        {
            if (expectedValue.CompareTo(actualValue) != 0)
            {
                string message = String.Format("The input contains an unexpected value '{0}', while the expected value is '{1}'.", actualValue, expectedValue);
                throw new ArgumentException(message, paramName);
            }
        }

        public static void AssertNotNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void AssertNotNullOrEmpty(string value, string paramName)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void AssertNotNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void AssertLength(string value, int length, string paramName)
        {
            AssertNotNull(value, paramName);
            if (value.Length != length)
            {
                throw new ArgumentOutOfRangeException(paramName, value.Length, "The length of the input is unexpected.");
            }
        }

        public static void AssertMaxLength(SecureString password, int maxLength, string paramName)
        {
            AssertNotNull(password, paramName);
            if (password.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(paramName, password.Length, "The input is longer than the maximum length.");
            }
        }

        public static void AssertMaxLength(string input, int maxLength, string paramName)
        {
            AssertNotNull(input, paramName);
            if (input.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(paramName, input.Length, "The input is longer than the maximum length.");
            }
        }

        public static void AssertMaxLength(byte[] input, int maxLength, string paramName)
        {
            AssertNotNull(input, paramName);
            if (input.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(paramName, input.Length, "The input is longer than the maximum length.");
            }
        }

        public static void AssertMinLength(byte[] data, int minLength, string paramName)
        {
            AssertNotNull(data, paramName);
            if (data.Length < minLength)
            {
                var exception = new ArgumentOutOfRangeException(paramName, data.Length, "The input is shorter than the minimum length.");
                // DEBUG: exception.Data.Add("BinaryBlob", data.ToHex());
                throw exception;
            }
        }

        public static void AssertMinLength(ReadOnlySpan<byte> data, int minLength, string paramName)
        {
            if (data.Length < minLength)
            {
                var exception = new ArgumentOutOfRangeException(paramName, data.Length, "The input is shorter than the minimum length.");
                throw exception;
            }
        }

        public static void AssertMinLength(ReadOnlyMemory<byte> data, int minLength, string paramName)
        {
            if (data.Length < minLength)
            {
                var exception = new ArgumentOutOfRangeException(paramName, data.Length, "The input is shorter than the minimum length.");
                throw exception;
            }
        }

        public static void AssertLength(byte[] value, long length, string paramName)
        {
            AssertNotNull(value, paramName);
            if (value.Length != length)
            {
                throw new ArgumentOutOfRangeException(paramName, value.Length, "The length of the input is unexpected.");
            }
        }

        public static void AssertFileExists(string filePath)
        {
            bool exists = File.Exists(filePath);
            if (!exists)
            {
                throw new FileNotFoundException("Path not found.", filePath);
            }
        }

        public static void AssertDirectoryExists(string directoryPath)
        {
            bool exists = Directory.Exists(directoryPath);
            if (!exists)
            {
                throw new DirectoryNotFoundException("Path not found.");
            }
        }

        public static void AssertCrcMatches(byte[] buffer, uint expectedCrc)
        {
            uint actualCrc = Crc32.Calculate(buffer);
            if (actualCrc != expectedCrc)
            {
                throw new FormatException("CRC check failed.");
            }
        }
    }
}
