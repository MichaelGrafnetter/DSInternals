//-----------------------------------------------------------------------
// <copyright file="JET_eventlogginglevel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    /// <summary>
    /// Options for EventLoggingLevel.
    /// </summary>
    public enum EventLoggingLevels
    {
        /// <summary>
        /// Disable all events.
        /// </summary>
        Disable = 0,

        /// <summary>
        /// Default level. Windows 7 and later.
        /// </summary>
        Min = 1,

        /// <summary>
        /// Low verbosity and lower. Windows 7 and later.
        /// </summary>
        Low = 25,

        /// <summary>
        /// Medium verbosity and lower. Windows 7 and later.
        /// </summary>
        Medium = 50,

        /// <summary>
        /// High verbosity and lower. Windows 7 and later.
        /// </summary>
        High = 75,

        /// <summary>
        /// All events.
        /// </summary>
        Max = 100,
    }
}
