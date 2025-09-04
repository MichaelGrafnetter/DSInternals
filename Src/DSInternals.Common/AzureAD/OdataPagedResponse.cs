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
        [JsonPropertyName("value")]
        public List<T> Items
        {
            get;
            private set;
        }

        [JsonPropertyName("odata.nextlink")]
        public string NextLink
        {
            get;
            private set;
        }

        public bool HasMoreData
        {
            get
            {
                return this.NextLink != null;
            }
        }
    }
}
