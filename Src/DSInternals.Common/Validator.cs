using DSInternals.Common.Cryptography;
using DSInternals.Common.Interop;
using DSInternals.Common.Properties;
using System;
using System.ComponentModel;
using System.IO;
using System.Security;

namespace DSInternals.Common
{
    public static class Validator
    {
        public static void AssertSuccess(NtStatus status)
        {
            if(status != NtStatus.Success)
            {
                Win32ErrorCode code = NativeMethods.RtlNtStatusToDosError(status);
                throw new Win32Exception((int) code);
            }
        }
        public static void AssertSuccess(Win32ErrorCode code)
        {
            if (code != Win32ErrorCode.Success)
            {
                throw new Win32Exception((int) code);
            }
        }

        public static void AssertNotNull(object value, string paramName)
        {
            if(value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
        public static void AssertNotNullOrWhiteSpace(string value, string paramName)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }
        public static void AssertLength(string value, int length, string paramName)
        {
            AssertNotNull(value, paramName);
            if(value.Length != length)
            {
                throw new ArgumentOutOfRangeException(paramName, value.Length, Resources.UnexpectedLengthMessage);
            }
        }
        public static void AssertMaxLength(SecureString password, int maxLength, string paramName)
        {
            AssertNotNull(password, paramName);
            if (password.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(paramName, password.Length, Resources.InputLongerThanMaxMessage);
            }
        }
        public static void AssertMinLength(byte[] data, int minLength, string paramName)
        {
            AssertNotNull(data, paramName);
            if (data.Length < minLength)
            {
                var exception = new ArgumentOutOfRangeException(paramName, data.Length, Resources.InputShorterThanMinMessage);
                // DEBUG: exception.Data.Add("BinaryBlob", data.ToHex());
                throw exception;
            }
        }

        public static void AssertLength(byte[] value, long length, string paramName)
        {
            AssertNotNull(value, paramName);
            if (value.Length != length)
            {
                throw new ArgumentOutOfRangeException(paramName, value.Length, Resources.UnexpectedLengthMessage);
            }
        }

        public static void AssertFileExists(string filePath)
        {
            bool exists = File.Exists(filePath);
            if(!exists)
            {
                throw new FileNotFoundException(Resources.PathNotFoundMessage, filePath);
            }
        }

        public static void AssertDirectoryExists(string directoryPath)
        {
            bool exists = Directory.Exists(directoryPath);
            if (!exists)
            {
                throw new DirectoryNotFoundException(Resources.PathNotFoundMessage);
            }
        }

        public static void AssertCrcMatches(byte[] buffer, uint expectedCrc)
        {
            uint actualCrc = Crc32.Calculate(buffer);
            if(actualCrc != expectedCrc)
            {
                throw new Exception(Resources.InvalidCRCMessage);
            }
        }
    }
}
