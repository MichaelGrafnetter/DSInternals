namespace DSInternals.PowerShell.Commands
{
    using System.Management.Automation;
    using System.Security.Principal;

    public abstract class ADDBPrincipalCommandBase : ADDBObjectCommandBase
    {
        protected const string ParameterSetByName = "ByName";
        protected const string ParameterSetBySid = "BySID";

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParameterSetByName
        )]
        [ValidateNotNullOrEmpty]
        [Alias("Login", "sam")]
        public string SamAccountName
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParameterSetBySid
        )]
        [ValidateNotNullOrEmpty]
        [Alias("Sid")]
        public SecurityIdentifier ObjectSid
        {
            get;
            set;
        }
    }
}
