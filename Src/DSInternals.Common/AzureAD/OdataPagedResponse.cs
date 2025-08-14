using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
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
