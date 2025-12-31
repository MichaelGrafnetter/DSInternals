using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Set, "AzureADUserEx", DefaultParameterSetName = ParamSetSingleUserUPN)]
[OutputType("None")]
public class SetAzureADUserExCommand : AzureADCommandBase
{
    [Parameter(Mandatory = true)]
    [AllowEmptyCollection()]
    [Alias("SearchableDeviceKey", "KeyCredentialLink")]
    public KeyCredential[] KeyCredential
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        switch (ParameterSetName)
        {
            case ParamSetSingleUserId:
                Client.SetUserAsync(ObjectId.Value, KeyCredential).GetAwaiter().GetResult();
                break;
            case ParamSetSingleUserUPN:
                Client.SetUserAsync(UserPrincipalName, KeyCredential).GetAwaiter().GetResult();
                break;
        }
    }
}
