//-----------------------------------------------------------------------
// <copyright file="EsentImplementationStubs.cs" company="Microsoft Corporation">
//  Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>
//  Class stubs to allow compiling on CoreClr.
// </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Implementation
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// A fake class to allow compilation on platforms that lack this functionality.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    public class Trace
    {
        /// <summary>
        /// A fake function to allow compilation on platforms that lack this functionality.
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        [ConditionalAttribute("TRACE")]
        public static void WriteLineIf(bool condition, string message)
        {
        }

        /// <summary>
        /// A fake function to allow compilation on platforms that lack this functionality.
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        [ConditionalAttribute("TRACE")]
        public static void WriteLineIf(bool condition, object message)
        {
        }

        /// <summary>
        /// Prints out the object's contents.
        /// </summary>
        /// <returns>A string represenetation or the object.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    /// A fake class to allow compilation on platforms that lack this class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    internal static class RuntimeHelpers
    {
        /// <summary>
        /// A fake function to allow compilation on platforms that lack this functionality.
        /// </summary>
        public static void PrepareConstrainedRegions()
        {
        }

        /// <summary>
        /// A fake function to allow compilation on platforms that lack this functionality.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        public static void PrepareMethod(RuntimeMethodHandle method)
        {
        }
    }

    /// <summary>
    /// Ascii encoding is not available on Core Clr. But UTF-8 is.
    /// This class will reject any character that results in an
    /// extended value.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    internal class SlowAsciiEncoding : UTF8Encoding
    {
        /// <summary>
        /// The standard encoding object.
        /// </summary>
        private static SlowAsciiEncoding slowAsciiEncoding = new SlowAsciiEncoding();

        /// <summary>
        /// Gets an Encoding object.
        /// </summary>
        public static Encoding Encoding
        {
            get
            {
                return slowAsciiEncoding;
            }
        }

#if !MANAGEDESENT_ON_WSA
        /// <summary>
        /// Converts a string to the byte representation.
        /// </summary>
        /// <param name="chars">
        /// The chars.
        /// </param>
        /// <param name="charCount">
        /// The char count.
        /// </param>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        /// <param name="byteCount">
        /// The byte count.
        /// </param>
        /// <returns>
        /// A count of bytes stored.
        /// </returns>
        public override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
        {
            IntPtr toFree;
            char* charsToTranslate = this.SanitizeString(chars, charCount, out toFree);

            int toReturn = base.GetBytes(charsToTranslate, charCount, bytes, byteCount);
            LibraryHelpers.MarshalFreeHGlobal(toFree);

            return toReturn;
        }
#endif

        /// <summary>
        /// Converts a string to the byte representation.
        /// </summary>
        /// <param name="inputString">
        /// The input string.
        /// </param>
        /// <returns>
        /// The byte representation of the string.
        /// </returns>
        public override byte[] GetBytes(string inputString)
        {
            string stringToTranslate = this.SanitizeString(inputString);
            return base.GetBytes(stringToTranslate);
        }

        /// <summary>
        /// Converts a string to the byte representation.
        /// </summary>
        /// <param name="inputString">
        /// The input string.
        /// </param>
        /// <param name="charIndex">
        /// The char index.
        /// </param>
        /// <param name="charCount">
        /// The char count.
        /// </param>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        /// <param name="byteIndex">
        /// The byte index.
        /// </param>
        /// <returns>
        /// The byte representation of the string.
        /// </returns>
        public override int GetBytes(string inputString, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            string stringToTranslate = this.SanitizeString(inputString);

            return base.GetBytes(stringToTranslate, charIndex, charCount, bytes, byteIndex);
        }

        /// <summary>
        /// Scans the string looking for unmappable characters in the ASCII set, and replaces
        /// them with '?'.
        /// </summary>
        /// <param name="inputString">A unicode string with unknown characters.</param>
        /// <returns>A string that has all legal ASCII characters.</returns>
        private string SanitizeString(string inputString)
        {
            bool needToDuplicate = false;
            string returnString = inputString;

            foreach (char ch in inputString)
            {
                if (ch > 127)
                {
                    needToDuplicate = true;
                    break;
                }
            }

            if (needToDuplicate)
            {
                StringBuilder sb = new StringBuilder(inputString.Length);
                foreach (char ch in inputString)
                {
                    sb.Append(ch > 127 ? '?' : ch);
                }

                returnString = sb.ToString();
            }

            return returnString;
        }

#if !MANAGEDESENT_ON_WSA
        /// <summary>
        /// Scans the string looking for unmappable characters in the ASCII set, and replaces
        /// them with '?'.
        /// </summary>
        /// <param name="inputString">A unicode string with unknown characters.</param>
        /// <param name="charCount">The length of the string to sanitize.</param>
        /// <param name="allocedMemory">On output, a value that needs to be freed. Only used
        /// if there are any untranslaable characters.</param>
        /// <returns>A string that has all legal ASCII characters.</returns>
        private unsafe char* SanitizeString(char* inputString, int charCount, out IntPtr allocedMemory)
        {
            allocedMemory = IntPtr.Zero;

            bool needToDuplicate = false;
            char* returnString = inputString;

            for (int i = 0; i < charCount; ++i)
            {
                if (inputString[i] > 127)
                {
                    needToDuplicate = true;
                    break;
                }
            }

            if (needToDuplicate)
            {
                allocedMemory = LibraryHelpers.MarshalAllocHGlobal(charCount);
                returnString = (char*)allocedMemory;

                char* dest = returnString;

                for (int i = 0; i < charCount; ++i)
                {
                    dest[i] = inputString[i] > 127 ? '?' : inputString[i];
                }
            }

            return returnString;
        }
#endif
    }
}
