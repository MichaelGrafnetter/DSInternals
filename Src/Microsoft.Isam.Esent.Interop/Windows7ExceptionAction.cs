//-----------------------------------------------------------------------
// <copyright file="Windows7ExceptionAction.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows7
{
    /// <summary>
    /// Exception action that have been added to the Windows 7 version of ESENT.
    /// </summary>
    public static class Windows7ExceptionAction
    {
        /// <summary>
        /// Introduced in Windows 7. Use the Windows RaiseFailFastException API to force a crash.
        /// </summary>
        internal const JET_ExceptionAction FailFast = (JET_ExceptionAction)0x00000004;
    }
}
