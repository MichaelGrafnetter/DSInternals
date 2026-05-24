using System.Management.Automation;

namespace DSInternals.PowerShell;

/// <summary>
/// Defines the attribute used to designate a cmdlet parameter as one that
/// should accept base64 strings and convert them to byte arrays.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public sealed class AcceptBase64StringAttribute : ArgumentTransformationAttribute
{
    public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
    {
        string base64String;

        // Check if inputData is a String encapsulated in an PSObject.
        PSObject psObject = inputData as PSObject;
        if (psObject != null)
        {
            base64String = psObject.BaseObject as string;
        }
        else
        {
            // We are not dealing with an PSObject, so try to interpret it as a String.
            base64String = inputData as string;
        }

        // Now try to interpret the value as a base64 string.
        if (base64String != null)
        {
            return Convert.FromBase64String(base64String);
        }
        else
        {
            // We cannot parse the data as (encapsulated) base64 string, so we let the system try to do the conversion to byte[].
            return inputData;
        }
    }
}
