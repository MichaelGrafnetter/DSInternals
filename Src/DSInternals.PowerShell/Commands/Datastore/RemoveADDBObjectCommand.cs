namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Data;
    using DSInternals.Common.Exceptions;
    using DSInternals.DataStore;
    using DSInternals.PowerShell.Properties;
    using System;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Remove, "ADDBObject",
        ConfirmImpact = ConfirmImpact.High,
        SupportsShouldProcess = true
        )]
    [OutputType("None")]
    public class RemoveADDBObjectCommand : ADDBObjectCommandBase
    {
        [Parameter]
        public SwitchParameter Force
        {
            get;
            set;
        }

        protected override bool ReadOnly
        {
            get
            {
                return false;
            }
        }

        protected override void BeginProcessing()
        {
            if(! Force.IsPresent)
            {
                // Do not continue with operation until the user enforces it.
                // TODO: Move to resources
                var exception = new ArgumentException("This command physically deletes objects from the database. It does not leave tombstones, nor does it remove links and security descriptors associated with this object. As a consequence, database integrity errors and lingering objects will appear. Use at your own risk and never on production databases. To suppress this warning, reissue the command specifying the Force parameter.");
                var error = new ErrorRecord(exception, "RemoveADDBObject_ForceRequired", ErrorCategory.InvalidArgument, null);
                this.ThrowTerminatingError(error);
            }

            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            // TODO: Extract message strings as resources
            try
            {
                switch (this.ParameterSetName)
                {
                    case ParameterSetByDN:
                        var dn = new DistinguishedName(this.DistinguishedName);
                        if (this.ShouldProcess(this.DistinguishedName, "Remove Object from Database"))
                        {
                            this.DirectoryAgent.RemoveObject(dn);
                        }
                        else
                        {
                            // Just test the objects's presence.
                            this.DirectoryAgent.FindObject(dn);
                        }
                        break;

                    case ParameterSetByGuid:
                        if (this.ShouldProcess(this.ObjectGuid.ToString(), "Remove Object from Database"))
                        {
                            this.DirectoryAgent.RemoveObject(this.ObjectGuid);
                        }
                        else
                        {
                            // Just test the objects's presence.
                            this.DirectoryAgent.FindObject(this.ObjectGuid);
                        }
                        break;

                    default:
                        // This should never happen:
                        throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
                }
            }
            catch(DirectoryObjectNotFoundException ex)
            {
                var error = new ErrorRecord(ex, "RemoveADDBObject_NotFound", ErrorCategory.ObjectNotFound, ex.ObjectIdentifier);
                this.WriteError(error);
            }
        }
    }
}
