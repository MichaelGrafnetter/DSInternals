//-----------------------------------------------------------------------
// <copyright file="jet_errorinfo.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    /// <summary>
    /// The valid values of InfoLevel for JetGetErrorInfo.
    /// </summary>
    public enum JET_ErrorInfo
    {
        /// <summary>
        /// Retrieve information about the specific error passed in pvContext.
        /// </summary>
        SpecificErr = 1,
    }
}
