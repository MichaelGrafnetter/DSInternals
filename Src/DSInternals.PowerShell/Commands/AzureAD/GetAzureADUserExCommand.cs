namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Globalization;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using DSInternals.Common.AzureAD;

    [Cmdlet(VerbsCommon.Get, "AzureADUserEx", DefaultParameterSetName = ParamSetAllUsers)]
    [OutputType(typeof(AzureADUser))]
    public class GetAzureADUserExCommand : PSCmdlet, IDisposable
    {
        private const string ParamSetAllUsers = "GetMultiple";
        private const string ParamSetSingleUserId = "GetById";
        private const string ParamSetSingleUserUPN = "GetByUPN";

        [Parameter(Mandatory = true)]
        [Alias("Token")]
        public string AccessToken
        {
            get;
            set;
        }

        [Parameter(Mandatory = false, ParameterSetName = ParamSetAllUsers)]
        [Alias("AllUsers")]
        public SwitchParameter All
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ParameterSetName = ParamSetSingleUserId)]
        [Alias("Identity","Id", "UserId","ObjectGuid")]
        public Guid? ObjectId
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ParameterSetName = ParamSetSingleUserUPN)]
        [Alias("UPN","UserName")]
        public string UserPrincipalName
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        [Alias("Tenant")]
        public Guid? TenantId
        {
            get;
            set;
        }

        protected AzureADClient Client
        {
            get;
            set;
        }

        protected override void BeginProcessing()
        {
            this.Client = new AzureADClient(this.AccessToken, this.TenantId);
        }

        protected override void ProcessRecord()
        {
            try
            {
                switch (this.ParameterSetName)
                {
                    case ParamSetAllUsers:
                        this.FetchMultipleUsers();
                        break;
                    case ParamSetSingleUserId:
                        this.WriteObject(this.Client.GetUserAsync(this.ObjectId.Value).Result);
                        break;
                    case ParamSetSingleUserUPN:
                        this.WriteObject(this.Client.GetUserAsync(this.UserPrincipalName).Result);
                        break;
                }
            }
            catch (AggregateException e)
            {
                // Unpack the actual exception from the GetUsersAsync call
                throw e.InnerException;
            }
        }

        private void FetchMultipleUsers()
        {
            // Initial progress report
            ProgressRecord progress = new ProgressRecord(0, "Reading users from Azure AD", "Starting...");
            // Disable the progress bar as we do not know the total number of accounts.
            progress.PercentComplete = -1;
            this.WriteProgress(progress);

            // Start fetching the first batch of users
            OdataPagedResponse<AzureADUser> response;
            Task<OdataPagedResponse<AzureADUser>> userRetriever = this.Client.GetUsersAsync();
            int userCount = 0;

            do
            {
                // Wait for the users to be fetched from AAD
                response = userRetriever.Result;

                if (response.HasMoreData && this.All.IsPresent)
                {
                    // Start fetching the next page
                    userRetriever = this.Client.GetUsersAsync(response.NextLink);
                }

                // Write progress
                userCount += response.Items.Count;
                progress.StatusDescription = String.Format(CultureInfo.InvariantCulture, "{0} user(s)", userCount);
                this.WriteProgress(progress);

                // Write the previously fetched users to the pipeline
                this.WriteObject(response.Items, true);
            } while (response.HasMoreData && this.All.IsPresent);

            // Final progress record
            progress.RecordType = ProgressRecordType.Completed;
            this.WriteProgress(progress);
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (this.Client != null)
            {
                if (disposing)
                {
                    this.Client.Dispose();
                }

                this.Client = null;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
