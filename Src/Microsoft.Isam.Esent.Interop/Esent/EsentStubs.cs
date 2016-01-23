//-----------------------------------------------------------------------
// <copyright file="EsentStubs.cs" company="Microsoft Corporation">
//  Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>
//  Class stubs to allow compiling on CoreClr.
// </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Some useful functionality that was omitted from Core CLR classes.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "These stub classes are compiled only on some platforms that do not contain the entire framework, e.g. new Windows user interface.")]
    public static class ExtensionsToCoreClr
    {
        /// <summary>Converts the value of this instance to the equivalent OLE Automation date.</summary>
        /// <param name="dateTime">A DateTime structure.</param>
        /// <returns>A double-precision floating-point number that contains an OLE Automation date equivalent to the value of this instance.</returns>
        /// <exception cref="T:System.OverflowException">The value of this instance cannot be represented as an OLE Automation Date. </exception>
        /// <filterpriority>2</filterpriority>
        public static double ToOADate(this DateTime dateTime)
        {
            return LibraryHelpers.TicksToOADate(dateTime.Ticks);
        }
    }

    /// <summary>
    /// JetApi code that is specific to ESENT.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "These stub classes are compiled only on some platforms that do not contain the entire framework, e.g. new Windows user interface.")]
    public class SerializationInfo
    {
        /// <summary>
        /// Prints out the object's contents.
        /// </summary>
        /// <returns>A string represenetation or the object.</returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Deserializes an integer.
        /// </summary>
        /// <param name="propName">The name of the field to retrieve.</param>
        /// <returns>An integer.</returns>
        public int GetInt32(string propName)
        {
            return 0;
        }
    }

    /// <summary>
    /// A fake class to allow compilation on platforms that lack this class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "These stub classes are compiled only on some platforms that do not contain the entire framework, e.g. new Windows user interface.")]
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
    /// A fake class to allow compilation on platforms that lack this class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    internal class TraceSwitch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraceSwitch"/> class.
        /// </summary>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        public TraceSwitch(string displayName, string description)
        {
        }

        /// <summary>
        /// Gets a value indicating whether TraceVerbose.
        /// </summary>
        public bool TraceVerbose { get; private set; }

        /// <summary>
        /// Gets a value indicating whether TraceWarning.
        /// </summary>
        public bool TraceWarning { get; private set; }

        /// <summary>
        /// Gets a value indicating whether TraceError.
        /// </summary>
        public bool TraceError { get; private set; }

        /// <summary>
        /// Gets a value indicating whether TraceInfo.
        /// </summary>
        public bool TraceInfo { get; private set; }

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
    internal class Trace
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
        /// <param name="message">
        /// The message.
        /// </param>
        [ConditionalAttribute("TRACE")]
        public static void TraceError(string message)
        {
        }

        /// <summary>
        /// A fake function to allow compilation on platforms that lack this functionality.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The arguments.
        /// </param>
        [ConditionalAttribute("TRACE")]
        public static void TraceWarning(string message, params object[] args)
        {
        }
    }
}

#if MANAGEDESENT_ON_WSA
namespace System.ComponentModel
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Security;

    /// <summary>
    /// An exception from a Win32 call.
    /// </summary>
    //// The original derives from System.Runtime.InteropServices.ExternalException.
    internal class Win32Exception : Exception
    {
        /// <summary>
        /// The error code from a Win32 call.
        /// </summary>
        private readonly int nativeErrorCode;

        /// <summary>
        /// Initializes a new instance of the Win32Exception class.
        /// </summary>
        [SecuritySafeCritical]
        public Win32Exception() : this(Marshal.GetLastWin32Error())
        {
        }

        /// <summary>
        /// Initializes a new instance of the Win32Exception class.
        /// </summary>
        /// <param name="error">A win32 error code.</param>
        [SecuritySafeCritical]
        public Win32Exception(int error) : this(error, Win32Exception.GetErrorMessage(error))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Win32Exception class.
        /// </summary>
        /// <param name="error">A win32 error code.</param>
        /// <param name="message">The string message.</param>
        public Win32Exception(int error, string message)
            : base(message)
        {
            this.nativeErrorCode = error;
        }

        /// <summary>
        /// Initializes a new instance of the Win32Exception class.
        /// </summary>
        /// <param name="message">The string message.</param>
        [SecuritySafeCritical]
        public Win32Exception(string message) : this(Marshal.GetLastWin32Error(), message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Win32Exception class.
        /// </summary>
        /// <param name="message">The string message.</param>
        /// <param name="innerException">The nested exception.</param>
        [SecuritySafeCritical, SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "Retuning error codes.")]
        public Win32Exception(string message, Exception innerException) : base(message, innerException)
        {
            this.nativeErrorCode = Marshal.GetLastWin32Error();
        }

        /// <summary>
        /// Returns a string value of the Win32 error code.
        /// </summary>
        /// <param name="error">The Win32 error code.</param>
        /// <returns>A string representation.</returns>
        [SecuritySafeCritical]
        private static string GetErrorMessage(int error)
        {
            string result = string.Empty;
            result = "Win32 error (0x" + Convert.ToString(error, 16) + ")";
            return result;
        }
    }
}
#endif // MANAGEDESENT_ON_WSA
