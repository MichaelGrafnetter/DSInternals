namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;

    public abstract class ADDBModifyPrincipalCommandBase : ADDBPrincipalCommandBase
    {
        [Parameter(Mandatory = false)]
        [Alias("SkipMeta", "NoMetaUpdate", "NoMeta", "SkipObjMeta", "NoObjMeta", "SkipMetaDataUpdate", "NoMetaDataUpdate")]
        public SwitchParameter SkipMetaUpdate
        {
            get;
            set;
        }

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
            if (!Force.IsPresent)
            {
                // Do not continue with the operation until the user enforces it.
                var exception = new ArgumentException("This command physically modifies the database, which is not supported by Microsoft. Use at your own risk after performing a proper AD backup. To suppress this warning, reissue the command specifying the Force parameter.");
                var error = new ErrorRecord(exception, "ModifyPrincipal_ForceRequired", ErrorCategory.InvalidArgument, null);
                this.ThrowTerminatingError(error);
            }

            base.BeginProcessing();
        }

        protected void WriteVerboseResult(bool hasChanged)
        {
            if (hasChanged)
            {
                this.WriteVerbose("The object has been updated successfully.");
            }
            else
            {
                this.WriteVerbose("The object already contained the value to be added.");
            }
        }
    }
}
