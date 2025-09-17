using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
    /// <summary>
    /// Represents a paged response from OData-compliant Azure AD Graph API endpoints.
    /// </summary>
    /// <typeparam name="T">The type of items contained in the response.</typeparam>
    public class OdataPagedResponse<T>
    {
        /// <summary>
        /// Gets the collection of items returned in this page of results.
        /// </summary>
        [JsonPropertyName("value")]
        public List<T> Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the URL to retrieve the next page of results, or null if this is the last page.
        /// </summary>
        [JsonPropertyName("odata.nextlink")]
        public string NextLink
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether there are additional pages of data available to retrieve.
        /// </summary>
        public bool HasMoreData
        {
            get
            {
                return this.NextLink != null;
            }
        }
    }
}
