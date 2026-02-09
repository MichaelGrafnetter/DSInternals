using System.ComponentModel;
using System.Management.Automation;
using System.Net;
using System.Security;
using DSInternals.Common.Interop;
using DSInternals.Replication.Model;

namespace DSInternals.PowerShell.Commands;

/// <summary>
/// Adds SID history from a source principal to a destination principal through the MS-DRSR protocol.
/// </summary>
[Cmdlet(VerbsCommon.Add, "ADReplSidHistory")]
[Alias("Add-ADSidHistory")]
[OutputType("None")]
public class AddADReplSidHistoryCommand : ADReplCommandBase
{
    private const string ParameterSetCheckSecureChannel = "CheckSecureChannel";
    private const string ParameterSetIntraDomain = "IntraDomain";
    private const string ParameterSetCrossForest = "CrossForest";

    /// <summary>
    /// Name of the source domain to query for the SID of the source principal.
    /// </summary>
    [Parameter(Mandatory = true, ParameterSetName = ParameterSetCrossForest)]
    [ValidateNotNullOrEmpty]
    [Alias("SrcDomain")]
    public string SourceDomain
    {
        get;
        set;
    }

    /// <summary>
    /// Name of the source security principal whose SID history is to be added.
    /// </summary>
    [Parameter(Mandatory = true, ParameterSetName = ParameterSetCrossForest, ValueFromPipelineByPropertyName = true)]
    [Parameter(Mandatory = true, ParameterSetName = ParameterSetIntraDomain, ValueFromPipelineByPropertyName = true)]
    [ValidateNotNullOrEmpty]
    [Alias("SrcPrincipal")]
    public string SourcePrincipal
    {
        get;
        set;
    }

    /// <summary>
    /// Name of the source domain controller (PDC) in the source domain.
    /// </summary>
    [Parameter(Mandatory = false, ParameterSetName = ParameterSetCrossForest)]
    [ValidateNotNullOrEmpty]
    [Alias("SrcDomainController", "SourceServer")]
    public string SourceDomainController
    {
        get;
        set;
    }

    /// <summary>
    /// Credentials to be used in the source domain.
    /// </summary>
    [Parameter(Mandatory = false, ParameterSetName = ParameterSetCrossForest)]
    [ValidateNotNull]
    [Alias("SrcCreds")]
    public PSCredential SourceCredential
    {
        get;
        set;
    }

    /// <summary>
    /// Name of the destination domain containing the destination principal.
    /// </summary>
    [Parameter(Mandatory = true, ParameterSetName = ParameterSetCrossForest)]
    [ValidateNotNullOrEmpty]
    [Alias("DstDomain")]
    public string DestinationDomain
    {
        get;
        set;
    }

    /// <summary>
    /// Name of the destination security principal receiving the SID history.
    /// </summary>
    [Parameter(Mandatory = true, ParameterSetName = ParameterSetCrossForest, ValueFromPipelineByPropertyName = true)]
    [Parameter(Mandatory = true, ParameterSetName = ParameterSetIntraDomain, ValueFromPipelineByPropertyName = true)]
    [ValidateNotNullOrEmpty]
    [Alias("DstPrincipal")]
    public string DestinationPrincipal
    {
        get;
        set;
    }

    /// <summary>
    /// Verifies whether the channel is secure and returns the result of the verification.
    /// </summary>
    [Parameter(Mandatory = true, ParameterSetName = ParameterSetCheckSecureChannel)]
    public SwitchParameter CheckSecureChannel
    {
        get;
        set;
    }

    /// <summary>
    /// Deletes the source principal after its SID history is appended to the destination principal.
    /// </summary>
    [Parameter(Mandatory = true, ParameterSetName = ParameterSetIntraDomain)]
    public SwitchParameter DeleteSourceObject
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        try
        {
            // Convert switch parameters to operation flags
            AddSidHistoryOptions flags = AddSidHistoryOptions.None;

            if (this.CheckSecureChannel.IsPresent)
            {
                flags |= AddSidHistoryOptions.CheckSecureChannel;
                this.WriteVerbose("Verifying secure channel to the destination domain controller.");
            }
            else if (this.DeleteSourceObject.IsPresent)
            {
                flags |= AddSidHistoryOptions.DeleteSourceObject;
                this.WriteVerbose($"Performing intra-domain SID history migration from '{this.SourcePrincipal}' to '{this.DestinationPrincipal}' and deleting the source object.");
            }
            else
            {
                this.WriteVerbose($"Performing cross-forest SID history migration from '{this.SourcePrincipal}' to '{this.DestinationPrincipal}'.");
            }

            // Build optional source credentials only for the cross-forest flow.
            // The protocol allows omitted credentials (use current context), and in other modes
            // credentials must be absent, so we only normalize when explicitly provided here.
            NetworkCredential normalizedCredential = null;
            if (this.ParameterSetName == ParameterSetCrossForest && this.SourceCredential != null)
            {
                // Normalize user/domain so the DRS request receives separate fields even when
                // the credential is provided as DOMAIN\\user or user@domain.
                NetworkCredential sourceCredential = this.SourceCredential.GetNetworkCredential();
                string sourceUser = sourceCredential.UserName;
                string sourceDomain = sourceCredential.Domain;

                if (string.IsNullOrWhiteSpace(sourceDomain))
                {
                    // Fallback to parsing the raw username when the domain field is empty.
                    string rawUser = this.SourceCredential.UserName;
                    if (!string.IsNullOrWhiteSpace(rawUser))
                    {
                        int separatorIndex = rawUser.IndexOf('\\');
                        if (separatorIndex > 0)
                        {
                            // DOMAIN\user format
                            sourceDomain = rawUser.Substring(0, separatorIndex);
                            sourceUser = rawUser.Substring(separatorIndex + 1);
                        }
                        else
                        {
                            int atIndex = rawUser.IndexOf('@');
                            if (atIndex > 0)
                            {
                                // user@domain format
                                sourceUser = rawUser.Substring(0, atIndex);
                                sourceDomain = rawUser.Substring(atIndex + 1);
                            }
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(sourceDomain))
                {
                    // Default the domain to the source domain parameter if still missing.
                    sourceDomain = this.SourceDomain;
                }

                // Use a NetworkCredential so the interop layer can pass discrete fields.
                SecureString sourcePassword = this.SourceCredential.Password;
                normalizedCredential = new NetworkCredential(sourceUser, sourcePassword, sourceDomain);
            }

            this.ReplicationClient.AddSidHistory(
                this.SourceDomain,
                this.SourcePrincipal,
                this.SourceDomainController,
                normalizedCredential,
                this.DestinationDomain,
                this.DestinationPrincipal,
                flags);
        }
        catch (Win32Exception ex)
        {
            ErrorCategory category = ((Win32ErrorCode)ex.NativeErrorCode).ToPSCategory();
            var error = new ErrorRecord(ex, "AddADReplSidHistoryFailed", category, null);
            this.WriteError(error);
        }
        catch (Exception ex)
        {
            var error = new ErrorRecord(ex, "AddADReplSidHistoryFailed", ErrorCategory.NotSpecified, null);
            this.WriteError(error);
        }
    }
}
