//-----------------------------------------------------------------------
// <copyright file="jet_exceptionaction.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    /// <summary>
    /// Constants to be used with JET_paramExceptionAction.
    /// </summary>
    public enum JET_ExceptionAction
    {
        /// <summary>
        /// Display message box on exception.
        /// </summary>
        MsgBox = 0x00000001,
        
        /// <summary>
        /// Do not handle exceptions. Throw them to the caller.
        /// </summary>
        None = 0x00000002,
    }
}
