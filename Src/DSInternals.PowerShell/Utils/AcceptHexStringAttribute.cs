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
        public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
        {
            // Try to interpret the value as a HEX string
            string hexString = inputData as string;
            if (hexString != null)
            {
                // Parse the hex data
                return hexString.HexToBinary();
            }
            else
            {
                // We cannot parse the data as hex string so let system try to do the conversion to byte[].
                return inputData;
            }
        }
    }
}