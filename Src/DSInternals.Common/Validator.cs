using System.ComponentModel;
using System.Net.Sockets;
using System.Security;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Interop;
using Windows.Win32.Foundation;

namespace DSInternals.Common
{
    /// <summary>
    /// Provides validation methods for parameters and system status codes.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validates that an NT status code indicates success and throws an exception if it doesn't.
        /// </summary>
        /// <param name="status">The NT status code to validate.</param>
        /// <exception cref="ArgumentException">Thrown when the status indicates an invalid parameter.</exception>
        /// <exception cref="Win32Exception">Thrown when the status indicates a system error.</exception>
        public static void AssertSuccess(NtStatus status)
        {
            Win32ErrorCode code = NativeMethods.RtlNtStatusToDosError(status);
            AssertSuccess(code);
        }

        internal static void AssertSuccess(NTSTATUS status)
        {
            if (status.SeverityCode == NTSTATUS.Severity.Success)
            {
                // No error has occured
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

        /// <summary>
        /// Validates that a Win32 error code indicates success and throws an appropriate exception if it doesn't.
        /// </summary>
        /// <param name="code">The Win32 error code to validate.</param>
        /// <exception cref="ArgumentException">Thrown when the error code indicates an invalid parameter or DN syntax.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the error code indicates a file was not found.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the error code indicates access is denied or password issues.</exception>
        /// <exception cref="OutOfMemoryException">Thrown when the error code indicates insufficient memory.</exception>
        /// <exception cref="SocketException">Thrown when the error code indicates network connectivity issues.</exception>
        /// <exception cref="DirectoryObjectNotFoundException">Thrown when the error code indicates a directory object was not found.</exception>
        /// <exception cref="Win32Exception">Thrown for other Win32 error codes.</exception>
        public static void AssertSuccess(Win32ErrorCode code)
        {
            switch (code)
            {
                case Win32ErrorCode.Success:
                case Win32ErrorCode.MORE_DATA:
                    // No error occured, so exit gracefully.
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
                case Win32ErrorCode.NO_SUCH_DOMAIN:
                case Win32ErrorCode.RPC_S_SERVER_UNAVAILABLE:
                case Win32ErrorCode.RPC_S_CALL_FAILED:
                    exceptionToThrow = new SocketException((int)code);
                    break;
                case Win32ErrorCode.DS_OBJ_NOT_FOUND:
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

        /// <summary>
        /// Validates that two string values are equal using invariant culture comparison.
        /// </summary>
        /// <param name="expectedValue">The expected string value.</param>
        /// <param name="actualValue">The actual string value to compare.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentException">Thrown when the values are not equal.</exception>
        public static void AssertEquals(string expectedValue, string actualValue, string paramName)
        {
            if (!String.Equals(expectedValue, actualValue, StringComparison.InvariantCulture))
            {
                string message = String.Format("The input contains an unexpected value '{0}', while the expected value is '{1}'.", actualValue, expectedValue);
                throw new ArgumentException(message, paramName);
            }
        }

        /// <summary>
        /// Validates that two integer values are equal.
        /// </summary>
        /// <param name="expectedValue">The expected integer value.</param>
        /// <param name="actualValue">The actual integer value to compare.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentException">Thrown when the values are not equal.</exception>
        public static void AssertEquals(int expectedValue, int actualValue, string paramName)
        {
            if (expectedValue != actualValue)
            {
                string message = String.Format("The input contains an unexpected value '{0}', while the expected value is '{1}'.", actualValue, expectedValue);
                throw new ArgumentException(message, paramName);
            }
        }

        /// <summary>
        /// Validates that two character values are equal.
        /// </summary>
        /// <param name="expectedValue">The expected character value.</param>
        /// <param name="actualValue">The actual character value to compare.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentException">Thrown when the values are not equal.</exception>
        public static void AssertEquals(char expectedValue, char actualValue, string paramName)
        {
            if (expectedValue.CompareTo(actualValue) != 0)
            {
                string message = String.Format("The input contains an unexpected value '{0}', while the expected value is '{1}'.", actualValue, expectedValue);
                throw new ArgumentException(message, paramName);
            }
        }

        /// <summary>
        /// Validates that an object is not null.
        /// </summary>
        /// <param name="value">The object to validate.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        public static void AssertNotNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Validates that a string is not null or empty.
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null or empty.</exception>
        public static void AssertNotNullOrEmpty(string value, string paramName)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Validates that a string is not null, empty, or consists only of whitespace characters.
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null, empty, or whitespace.</exception>
        public static void AssertNotNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Validates that a string has the exact specified length.
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <param name="length">The expected length of the string.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the string length does not match the expected length.</exception>
        public static void AssertLength(string value, int length, string paramName)
        {
            AssertNotNull(value, paramName);
            if (value.Length != length)
            {
                throw new ArgumentOutOfRangeException(paramName, value.Length, "The length of the input is unexpected.");
            }
        }

        /// <summary>
        /// Validates that a SecureString does not exceed the maximum allowed length.
        /// </summary>
        /// <param name="password">The SecureString to validate.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the password is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the password length exceeds the maximum.</exception>
        public static void AssertMaxLength(SecureString password, int maxLength, string paramName)
        {
            AssertNotNull(password, paramName);
            if (password.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(paramName, password.Length, "The input is longer than the maximum length.");
            }
        }

        /// <summary>
        /// Validates that a string does not exceed the maximum allowed length.
        /// </summary>
        /// <param name="input">The string to validate.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the input is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the input length exceeds the maximum.</exception>
        public static void AssertMaxLength(string input, int maxLength, string paramName)
        {
            AssertNotNull(input, paramName);
            if (input.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(paramName, input.Length, "The input is longer than the maximum length.");
            }
        }

        /// <summary>
        /// Validates that a byte array does not exceed the maximum allowed length.
        /// </summary>
        /// <param name="input">The byte array to validate.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the input is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the input length exceeds the maximum.</exception>
        public static void AssertMaxLength(byte[] input, int maxLength, string paramName)
        {
            AssertNotNull(input, paramName);
            if (input.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(paramName, input.Length, "The input is longer than the maximum length.");
            }
        }

        /// <summary>
        /// Validates that a byte array meets the minimum required length.
        /// </summary>
        /// <param name="data">The byte array to validate.</param>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the data is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the data length is less than the minimum.</exception>
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

        /// <summary>
        /// Validates that a read-only byte span meets the minimum required length.
        /// </summary>
        /// <param name="data">The read-only byte span to validate.</param>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the data length is less than the minimum.</exception>
        public static void AssertMinLength(ReadOnlySpan<byte> data, int minLength, string paramName)
        {
            if (data.Length < minLength)
            {
                var exception = new ArgumentOutOfRangeException(paramName, data.Length, "The input is shorter than the minimum length.");
                throw exception;
            }
        }

        /// <summary>
        /// Validates that a read-only byte memory meets the minimum required length.
        /// </summary>
        /// <param name="data">The read-only byte memory to validate.</param>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the data length is less than the minimum.</exception>
        public static void AssertMinLength(ReadOnlyMemory<byte> data, int minLength, string paramName)
        {
            if (data.Length < minLength)
            {
                var exception = new ArgumentOutOfRangeException(paramName, data.Length, "The input is shorter than the minimum length.");
                throw exception;
            }
        }

        /// <summary>
        /// Validates that a byte array has the exact specified length.
        /// </summary>
        /// <param name="value">The byte array to validate.</param>
        /// <param name="length">The expected length of the array.</param>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the array length does not match the expected length.</exception>
        public static void AssertLength(byte[] value, long length, string paramName)
        {
            AssertNotNull(value, paramName);
            if (value.Length != length)
            {
                throw new ArgumentOutOfRangeException(paramName, value.Length, "The length of the input is unexpected.");
            }
        }

        /// <summary>
        /// Validates that a file exists at the specified path.
        /// </summary>
        /// <param name="filePath">The path to the file to validate.</param>
        /// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
        public static void AssertFileExists(string filePath)
        {
            bool exists = File.Exists(filePath);
            if (!exists)
            {
                throw new FileNotFoundException("Path not found.", filePath);
            }
        }

        /// <summary>
        /// Validates that a directory exists at the specified path.
        /// </summary>
        /// <param name="directoryPath">The path to the directory to validate.</param>
        /// <exception cref="DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        public static void AssertDirectoryExists(string directoryPath)
        {
            bool exists = Directory.Exists(directoryPath);
            if (!exists)
            {
                throw new DirectoryNotFoundException("Path not found.");
            }
        }

        /// <summary>
        /// Validates that a buffer's CRC32 checksum matches the expected value.
        /// </summary>
        /// <param name="buffer">The byte buffer to validate.</param>
        /// <param name="expectedCrc">The expected CRC32 checksum value.</param>
        /// <exception cref="FormatException">Thrown when the CRC check fails.</exception>
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
