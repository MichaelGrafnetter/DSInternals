using System;
using System.Management.Automation;
using DSInternals.Common;

namespace DSInternals.PowerShell
{
    /// <summary>
    /// Defines the attribute used to designate a cmdlet parameter as one that
    /// should accept hex strings and convert them to byte arrays.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class AcceptHexStringAttribute : ArgumentTransformationAttribute
    {
        /// <summary>
        /// Transforms hex string input data to byte arrays for PowerShell cmdlet parameters.
        /// </summary>
        /// <param name="engineIntrinsics">The engine intrinsics for the current PowerShell session.</param>
        /// <param name="inputData">The input data to transform (string or byte array).</param>
        /// <returns>A byte array converted from the input hex string or the original byte array.</returns>
        public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
        {
            string hexString;

            // Check if inputData is a String encapsulated in an PSObject
            PSObject psObject = inputData as PSObject;
            if(psObject != null)
            {
                hexString = psObject.BaseObject as string;
            }
            else
            {
                // We are not dealing with an PSObject, so try to interpret it as a String
                hexString = inputData as string;
            }

            // Now try to interpret the value as a HEX string
            if (hexString != null)
            {
                // Parse the hex data
                return hexString.HexToBinary();
            }
            else
            {
                // We cannot parse the data as (encapsulated) hex string so we let the system try to do the conversion to byte[].
                return inputData;
            }
        }
    }
}