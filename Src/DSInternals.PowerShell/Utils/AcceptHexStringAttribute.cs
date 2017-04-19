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