//-----------------------------------------------------------------------
// <copyright file="Windows10Param.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows10
{
    /// <summary>
    /// System parameters that were introduced in Windows 10.
    /// </summary>
    public static class Windows10Param
    {
        /// <summary>
        /// This allows the client to specify a registry path preceded by a reg: to optionally configure
        /// loading or overriding parameters from the registry.
        /// </summary>
        public const JET_param ConfigStoreSpec = (JET_param)189;
    }
}
