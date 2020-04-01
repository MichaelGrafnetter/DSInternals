using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DSInternals.Common.Exceptions;
using Newtonsoft.Json;

namespace DSInternals.Common.AzureAD
{
    public class AzureADClient : IDisposable
    {
        private const string DefaultTenantId = "myorganization";
        private const string SelectParameter = "$select=userPrincipalName,searchableDeviceKey,objectId,displayName,accountEnabled";
        private const string ApiVersionParameter = "&api-version=1.6-internal";
        private const string BatchSizeParameterFormat = "&$top={0}";
        private const string UPNFilterParameterFormat = "&$filter=userPrincipalName eq '{0}'";
        private const string IdFilterParameterFormat = "&$filter=objectId eq '{0}'";
        private const string AuthenticationScheme = "Bearer";
        
        private const string UsersUrlFormat = "https://graph.windows.net/{0}/users/?";
        public const int MaxBatchSize = 999;
        private static readonly MediaTypeWithQualityHeaderValue s_contentType = MediaTypeWithQualityHeaderValue.Parse("application/json;odata=nometadata;streaming=false");
        private string _tenantId;
        private HttpClient _httpClient;
        private readonly string _batchSizeParameter;
        private JsonSerializer _jsonSerializer = JsonSerializer.CreateDefault();

        public AzureADClient(string accessToken, Guid? tenantId = null, int batchSize = MaxBatchSize)
        {
            // Validate inputs
            Validator.AssertNotNullOrWhiteSpace(accessToken, nameof(accessToken));

            this._tenantId = tenantId.HasValue ? tenantId.Value.ToString() : DefaultTenantId;
            this._batchSizeParameter = String.Format(CultureInfo.InvariantCulture, BatchSizeParameterFormat, batchSize);

            this._httpClient = new HttpClient();
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationScheme, accessToken);
            this._httpClient.DefaultRequestHeaders.Accept.Add(s_contentType);
        }

        public async Task<AzureADUser> GetUserAsync(string userPrincipalName)
        {
            // Vaidate the input
            Validator.AssertNotNullOrEmpty(userPrincipalName, nameof(userPrincipalName));

            string filter = String.Format(CultureInfo.InvariantCulture, UPNFilterParameterFormat, userPrincipalName);
            return await this.GetUserAsync(filter, userPrincipalName);
        }

        public async Task<AzureADUser> GetUserAsync(Guid objectId)
        {
            string filter = String.Format(CultureInfo.InvariantCulture, IdFilterParameterFormat, objectId);
            return await this.GetUserAsync(filter, objectId);
        }

        private async Task<AzureADUser> GetUserAsync(string filterParameter, object userIdentifier)
        {
            // Build uri with filter
            var url = new StringBuilder();
            url.AppendFormat(CultureInfo.InvariantCulture, UsersUrlFormat, this._tenantId);
            url.Append(SelectParameter);
            url.Append(filterParameter);

            // Send the request
            var result = await this.GetUsersAsync(url.ToString()).ConfigureAwait(false);
            if (result.Items == null || result.Items.Count == 0)
            {
                throw new DirectoryObjectNotFoundException(userIdentifier);
            }

            return result.Items[0];
        }

        public async Task<OdataPagedResponse<AzureADUser>> GetUsersAsync(string nextLink = null)
        {
            var url = new StringBuilder(nextLink);

            if (String.IsNullOrEmpty(nextLink))
            {
                // Build the intial URL
                url.AppendFormat(CultureInfo.InvariantCulture, UsersUrlFormat, this._tenantId);
                url.Append(SelectParameter);
            }

            // Add query string parameters
            url.Append(ApiVersionParameter)
               .Append(this._batchSizeParameter);

            // Perform API call
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url.ToString()))
                using (var response = await this._httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var streamReader = new StreamReader(responseStream))
                {
                    if(s_contentType.MediaType.Equals(response.Content.Headers.ContentType.MediaType, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // The response is a JSON document
                        using (var jsonTextReader = new JsonTextReader(streamReader))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = this._jsonSerializer.Deserialize<OdataPagedResponse<AzureADUser>>(jsonTextReader);
                                // Update key credential owner references
                                if(result.Items != null)
                                {
                                    result.Items.ForEach(user => user.UpdateKeyCredentialReferences());
                                }
                                return result;
                            }
                            else
                            {
                                // Translate OData response to an exception
                                var error = this._jsonSerializer.Deserialize<OdataErrorResponse>(jsonTextReader);
                                throw error.GetException();
                            }
                        }
                    }
                    else
                    {
                        // The response is not a JSON document, so we parse its first line as message text
                        string message = await streamReader.ReadLineAsync().ConfigureAwait(false);
                        throw new GraphApiException(message, response.StatusCode.ToString());
                    }
                }
            }
            catch(JsonException e)
            {
                throw new GraphApiException("The data returned by the REST API call has an unexpected format.", e);
            }
            catch(HttpRequestException e)
            {
                // Unpack a more meaningful message, e. g. DNS error
                throw new GraphApiException(e?.InnerException.Message ?? "An error occured while trying to call the REST API.", e);
            }
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (this._httpClient != null)
            {
                if (disposing)
                {
                    this._httpClient.Dispose();
                }

                this._httpClient = null;
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
