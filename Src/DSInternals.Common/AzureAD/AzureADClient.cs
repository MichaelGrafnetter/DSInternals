using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;
using Newtonsoft.Json;

namespace DSInternals.Common.AzureAD
{
    public class AzureADClient : IDisposable
    {
        private const string DefaultTenantId = "myorganization";
        private const string SelectParameter = "$select=userPrincipalName,searchableDeviceKey,objectId,displayName,accountEnabled";
        private const string ApiVersionParameter = "api-version=1.6-internal";
        private const string BatchSizeParameterFormat = "$top={0}";
        private const string UPNFilterParameterFormat = "$filter=userPrincipalName eq '{0}'";
        private const string IdFilterParameterFormat = "$filter=objectId eq '{0}'";
        private const string AuthenticationScheme = "Bearer";
        private const char UriParameterSeparator = '&';
        private const string UsersUrlFormat = "https://graph.windows.net/{0}/users/{1}?";
        private const string JsonContentType = "application/json";
        private const string KeyCredentialAttributeName = "searchableDeviceKey";
        public const int MaxBatchSize = 999;
        private static readonly MediaTypeWithQualityHeaderValue s_odataContentType = MediaTypeWithQualityHeaderValue.Parse("application/json;odata=nometadata;streaming=false");
        private string _tenantId;
        private HttpClient _httpClient;
        private readonly string _batchSizeParameter;
        private JsonSerializer _jsonSerializer = JsonSerializer.CreateDefault();

        public AzureADClient(string accessToken, Guid? tenantId = null, int batchSize = MaxBatchSize)
        {
            // Validate inputs
            Validator.AssertNotNullOrWhiteSpace(accessToken, nameof(accessToken));

            _tenantId = tenantId?.ToString() ?? DefaultTenantId;
            _batchSizeParameter = string.Format(CultureInfo.InvariantCulture, BatchSizeParameterFormat, batchSize);

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationScheme, accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(s_odataContentType);
        }

        public async Task<AzureADUser> GetUserAsync(string userPrincipalName)
        {
            // Vaidate the input
            Validator.AssertNotNullOrEmpty(userPrincipalName, nameof(userPrincipalName));

            var filter = string.Format(CultureInfo.InvariantCulture, UPNFilterParameterFormat, userPrincipalName);
            return await GetUserAsync(filter, userPrincipalName);
        }

        public async Task<AzureADUser> GetUserAsync(Guid objectId)
        {
            var filter = string.Format(CultureInfo.InvariantCulture, IdFilterParameterFormat, objectId);
            return await GetUserAsync(filter, objectId);
        }

        private async Task<AzureADUser> GetUserAsync(string filterParameter, object userIdentifier)
        {
            // Build uri with filter
            var url = new StringBuilder();
            url.AppendFormat(CultureInfo.InvariantCulture, UsersUrlFormat, _tenantId, String.Empty);
            url.Append(SelectParameter);
            url.Append(UriParameterSeparator);
            url.Append(filterParameter);

            // Send the request
            var result = await GetUsersAsync(url.ToString()).ConfigureAwait(false);
            if ((result.Items?.Count ?? 0) == 0)
            {
                throw new DirectoryObjectNotFoundException(userIdentifier);
            }

            return result.Items[0];
        }

        public async Task<OdataPagedResponse<AzureADUser>> GetUsersAsync(string nextLink = null)
        {
            var url = new StringBuilder(nextLink);

            if (string.IsNullOrEmpty(nextLink))
            {
                // Build the intial URL
                url.AppendFormat(CultureInfo.InvariantCulture, UsersUrlFormat, _tenantId, String.Empty);
                url.Append(SelectParameter);
            }

            // Add query string parameters
            url.Append(UriParameterSeparator);
            url.Append(ApiVersionParameter);
            url.Append(UriParameterSeparator);
            url.Append(_batchSizeParameter);

            // Perform API call
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url.ToString()))
                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var streamReader = new StreamReader(responseStream))
                {
                    if (s_odataContentType.MediaType.Equals(response.Content.Headers.ContentType.MediaType, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // The response is a JSON document
                        using (var jsonTextReader = new JsonTextReader(streamReader))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = _jsonSerializer.Deserialize<OdataPagedResponse<AzureADUser>>(jsonTextReader);
                                // Update key credential owner references
                                if (result.Items != null)
                                {
                                    result.Items.ForEach(user => user.UpdateKeyCredentialReferences());
                                }
                                return result;
                            }
                            else
                            {
                                // Translate OData response to an exception
                                var error = _jsonSerializer.Deserialize<OdataErrorResponse>(jsonTextReader);
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
            catch (JsonException e)
            {
                throw new GraphApiException("The data returned by the REST API call has an unexpected format.", e);
            }
            catch (HttpRequestException e)
            {
                // Unpack a more meaningful message, e. g. DNS error
                throw new GraphApiException(e?.InnerException.Message ?? "An error occured while trying to call the REST API.", e);
            }
        }

        public async Task SetUserAsync(string userPrincipalName, KeyCredential[] keyCredentials)
        {
            // Vaidate the input
            Validator.AssertNotNullOrEmpty(userPrincipalName, nameof(userPrincipalName));

            var properties = new Hashtable() { { KeyCredentialAttributeName, keyCredentials } };
            await SetUserAsync(userPrincipalName, properties);
        }

        public async Task SetUserAsync(Guid objectId, KeyCredential[] keyCredentials)
        {
            var properties = new Hashtable() { { KeyCredentialAttributeName, keyCredentials } };
            await SetUserAsync(objectId.ToString(), properties);
        }

        private async Task SetUserAsync(string userIdentifier, Hashtable properties)
        {
            // Build the request uri
            var url = new StringBuilder();
            url.AppendFormat(CultureInfo.InvariantCulture, UsersUrlFormat, _tenantId, userIdentifier);
            url.Append(ApiVersionParameter);

            // Perform API call
            try
            {
                // TODO: Switch to HttpMethod.Patch after migrating to .NET Standard 2.1 / .NET 5
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), url.ToString()))
                {
                    // Build the request body
                    request.Content = new StringContent(JsonConvert.SerializeObject(properties), Encoding.UTF8, JsonContentType);

                    // Send the request
                    using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                    {
                        // TODO: Error handling
                        // response.StatusCode;
                    }
                }
            }
            catch (JsonException e)
            {
                throw new GraphApiException("The data returned by the REST API call has an unexpected format.", e);
            }
            catch (HttpRequestException e)
            {
                // Unpack a more meaningful message, e. g. DNS error
                throw new GraphApiException(e?.InnerException.Message ?? "An error occured while trying to call the REST API.", e);
            }
        }

        #region IDisposable Support
        public virtual void Dispose()
        {
            _httpClient.Dispose();
        }
        #endregion
    }
}
