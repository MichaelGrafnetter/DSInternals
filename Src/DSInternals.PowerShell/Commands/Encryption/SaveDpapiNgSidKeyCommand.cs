using System.Linq;
using System.Management.Automation;
using System.Security.Principal;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsData.Save, "DpapiNgSidKey", DefaultParameterSetName = ParsedIdentifierParameterSetName)]
[Alias("Save-CngDpapiSidKey")]
[OutputType("None")]
public class SaveDpapiNgSidKeyCommand : PSCmdlet
{
    private const string ParsedIdentifierParameterSetName = "ParsedIdentifier";
    private const string IdentifierBlobParameterSetName = "IdentifierBlob";

    [Parameter(
        Mandatory = true,
        Position = 0,
        ParameterSetName = ParsedIdentifierParameterSetName
    )]
    [ValidateNotNull]
    [Alias("ProtectionKeyIdentifier", "KeyIdentifier", "KeyId")]
    public ProtectionKeyIdentifier Identifier
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 0,
        ParameterSetName = IdentifierBlobParameterSetName
    )]
    [ValidateNotNullOrEmpty]
    [AcceptHexString]
    [Alias("ProtectionKeyIdentifierBlob", "KeyIdentifierBlob", "KeyIdBlob", "Blob")]
    public byte[] IdentifierBlob
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 1
    )]
    [ValidateNotNullOrEmpty]
    [Alias("KdsRootKeys", "RootKey", "RootKeys")]
    public KdsRootKey[] KdsRootKey
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 2
    )]
    [ValidateNotNull]
    [Alias("Sid", "TargetSid", "ObjectSid")]
    public SecurityIdentifier SecurityIdentifier
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        try
        {
            ProtectionKeyIdentifier identifier = this.ParameterSetName == IdentifierBlobParameterSetName
                ? new ProtectionKeyIdentifier(this.IdentifierBlob)
                : this.Identifier;

            KdsRootKey? matchingRootKey = this.KdsRootKey.FirstOrDefault(key => key.KeyId == identifier.RootKeyId);

            if (matchingRootKey == null)
            {
                throw new ArgumentException("None of the supplied KDS root keys matches the protection key identifier.", nameof(this.KdsRootKey));
            }

            GroupKeyEnvelope envelope = GroupKeyEnvelope.Create(matchingRootKey, identifier, this.SecurityIdentifier);
            string cachedFilePath = envelope.WriteToCache();
            this.WriteVerbose($"Saved DPAPI-NG SID key to '{cachedFilePath}'.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            var error = new ErrorRecord(ex, "SaveDpapiNgSidKey_InvalidBlob", ErrorCategory.InvalidData, this.IdentifierBlob);
            this.WriteError(error);
        }
        catch (ArgumentException ex)
        {
            var error = new ErrorRecord(ex, "SaveDpapiNgSidKey_InvalidArgument", ErrorCategory.InvalidArgument, this.Identifier);
            this.WriteError(error);
        }
        catch (InvalidOperationException ex)
        {
            var error = new ErrorRecord(ex, "SaveDpapiNgSidKey_InvalidOperation", ErrorCategory.InvalidOperation, this.Identifier);
            this.WriteError(error);
        }
    }
}
