using System.Globalization;
using System.Management.Automation;
using System.Threading.Tasks;
using DSInternals.Common.AzureAD;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "AzureADUserEx", DefaultParameterSetName = ParamSetAllUsers)]
    [OutputType(typeof(AzureADUser))]
    public class GetAzureADUserExCommand : AzureADCommandBase
    {
        private const string ParamSetAllUsers = "Multiple";

        [Parameter(Mandatory = false, ParameterSetName = ParamSetAllUsers)]
        [Alias("AllUsers")]
        public SwitchParameter All
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case ParamSetAllUsers:
                    FetchMultipleUsers();
                    break;
                case ParamSetSingleUserId:
                    WriteObject(Client.GetUserAsync(ObjectId.Value).GetAwaiter().GetResult());
                    break;
                case ParamSetSingleUserUPN:
                    WriteObject(Client.GetUserAsync(UserPrincipalName).GetAwaiter().GetResult());
                    break;
            }
        }

        private void FetchMultipleUsers()
        {
            // Initial progress report
            ProgressRecord progress = new ProgressRecord(0, "Reading users from Azure AD", "Starting...");
            // Disable the progress bar as we do not know the total number of accounts.
            progress.PercentComplete = -1;
            WriteProgress(progress);

            // Start fetching the first batch of users
            OdataPagedResponse<AzureADUser> response;
            Task<OdataPagedResponse<AzureADUser>> userRetriever = Client.GetUsersAsync();
            int userCount = 0;

            do
            {
                // Wait for the users to be fetched from AAD
                response = userRetriever.GetAwaiter().GetResult();

                if (response.HasMoreData && All.IsPresent)
                {
                    // Start fetching the next page
                    userRetriever = Client.GetUsersAsync(response.NextLink);
                }

                // Write progress
                userCount += response.Items.Count;
                progress.StatusDescription = string.Format(CultureInfo.InvariantCulture, "{0} user(s)", userCount);
                WriteProgress(progress);

                // Write the previously fetched users to the pipeline
                WriteObject(response.Items, true);
            } while (response.HasMoreData && All.IsPresent);

            // Final progress record
            progress.RecordType = ProgressRecordType.Completed;
            WriteProgress(progress);
        }
    }
}
