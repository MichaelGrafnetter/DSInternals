namespace DSInternals.PowerShell.Commands
{
    using System.Management.Automation;
    using System.Security.Principal;

    public abstract class ADDBPrincipalCommandBase : ADDBObjectCommandBase
    {
        protected const string parameterSetByName = "ByName";
        protected const string parameterSetBySid = "BySID";

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = parameterSetByName
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
            ParameterSetName = parameterSetBySid
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