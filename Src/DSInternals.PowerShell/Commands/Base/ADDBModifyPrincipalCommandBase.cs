namespace DSInternals.PowerShell.Commands
{
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

        protected override bool ReadOnly
        {
            get
            {
                return false;
            }
        }

        protected void WriteVerboseResult(bool hasChanged)
        {
            if (hasChanged)
            {
                // TODO: Extract as resource:
                this.WriteVerbose("The object has been updated successfully.");
            }
            else
            {
                // TODO: Extract as resource:
                this.WriteVerbose("The object already contained the value to be added.");
            }
        }
    }
}