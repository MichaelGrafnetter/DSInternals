//-----------------------------------------------------------------------
// <copyright file="Windows8SystemParameters.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// This class provides static properties to set and get
    /// global ESENT system parameters.
    /// </summary>
    public partial class SystemParameters
    {
        /// <summary>
        /// Gets or sets the smallest amount of data that should be compressed with xpress compression.
        /// </summary>
        public static int MinDataForXpress
        {
            get
            {
                return GetIntegerParameter(Windows8Param.MinDataForXpress);
            }

            set
            {
                SetIntegerParameter(Windows8Param.MinDataForXpress, value);
            }
        }

        /// <summary>
        /// Gets or sets the threshold for what is considered a hung IO that should be acted upon.
        /// </summary>
        public static int HungIOThreshold
        {
            get
            {
                return GetIntegerParameter(Windows8Param.HungIOThreshold);
            }

            set
            {
                SetIntegerParameter(Windows8Param.HungIOThreshold, value);
            }
        }

        /// <summary>
        /// Gets or sets the set of actions to be taken on IOs that appear hung.
        /// </summary>
        public static int HungIOActions
        {
            get
            {
                return GetIntegerParameter(Windows8Param.HungIOActions);
            }

            set
            {
                SetIntegerParameter(Windows8Param.HungIOActions, value);
            }
        }

        /// <summary>
        /// Gets or sets the friendly name for this instance of the process.
        /// </summary>
        public static string ProcessFriendlyName
        {
            get
            {
                return GetStringParameter(Windows8Param.ProcessFriendlyName);
            }

            set
            {
                SetStringParameter(Windows8Param.ProcessFriendlyName, value);
            }
        }
    }
}
