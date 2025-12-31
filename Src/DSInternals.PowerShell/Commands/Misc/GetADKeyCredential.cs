using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;
[Cmdlet(VerbsCommon.Get, "ADKeyCredential", DefaultParameterSetName = ParamSetFromUserCertificate)]
[OutputType(new Type[] { typeof(KeyCredential) })]
public class GetADKeyCredentialCommand : PSCmdlet
{
    #region Parameters
    private const string ParamSetFromUserCertificate = "FromUserCertificate";
    private const string ParamSetFromComputerCertificate = "FromComputerCertificate";
    private const string ParamSetFromBinary = "FromBinary";
    private const string ParamSetFromDNBinary = "FromDNBinary";

    [Parameter(
        Mandatory = true,
        Position = 0,
        ParameterSetName = ParamSetFromDNBinary,
        ValueFromPipeline = true
    )]
    [Alias("DNWithBinary", "DistinguishedNameWithBinary")]
    public string[] DNWithBinaryData
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        ParameterSetName = ParamSetFromBinary
    )]
    [AcceptHexString]
    [Alias("Binary")]
    public byte[] BinaryData
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 0,
        ParameterSetName = ParamSetFromUserCertificate
    )]
    [Parameter(
        Mandatory = true,
        Position = 0,
        ParameterSetName = ParamSetFromComputerCertificate
    )]
    public X509Certificate2 Certificate
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 1,
        ParameterSetName = ParamSetFromUserCertificate
    )]
    [Alias("ComputerId", "ComputerGuid")]
    public Guid? DeviceId
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        ParameterSetName = ParamSetFromBinary
    )]
    [Parameter(
        Mandatory = true,
        Position = 2,
        ParameterSetName = ParamSetFromUserCertificate
    )]
    [Parameter(
        Mandatory = true,
        Position = 1,
        ParameterSetName = ParamSetFromComputerCertificate
    )]
    [Alias("DistinguishedName", "DN", "ObjectDN", "HolderDN", "Holder", "Owner", "UserPrincipalName", "UPN")]
    public string OwnerDN
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = false,
        ParameterSetName = ParamSetFromUserCertificate
    )]
    [Parameter(
        Mandatory = false,
        ParameterSetName = ParamSetFromComputerCertificate
    )]
    [Alias("CreatedTime", "TimeCreated", "TimeGenerated")]
    public DateTime? CreationTime
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        ParameterSetName = ParamSetFromComputerCertificate
    )]
    public SwitchParameter IsComputerKey
    {
        get;
        set;
    }
    #endregion Parameters

    #region Cmdlet Overrides
    protected override void ProcessRecord()
    {
        KeyCredential keyCredential;

        switch (this.ParameterSetName)
        {
            case ParamSetFromDNBinary:
                foreach (string singleValue in this.DNWithBinaryData)
                {
                    keyCredential = KeyCredential.ParseDNBinary(singleValue);
                    this.WriteObject(keyCredential);
                }
                break;
            case ParamSetFromBinary:
                keyCredential = new KeyCredential(this.BinaryData, this.OwnerDN);
                this.WriteObject(keyCredential);
                break;
            case ParamSetFromUserCertificate:
            case ParamSetFromComputerCertificate:
                keyCredential = new KeyCredential(this.Certificate, this.DeviceId, this.OwnerDN, this.CreationTime, this.IsComputerKey.IsPresent);
                this.WriteObject(keyCredential);
                break;
        }
    }
    #endregion Cmdlet Overrides
}
