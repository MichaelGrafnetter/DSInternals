using System.Collections.Generic;
using Newtonsoft.Json;

namespace DSInternals.Common.AzureAD
{
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class OdataPagedResponse<T>
    {
        [JsonProperty("value")]
        public List<T> Items
        {
            get;
            private set;
        }

        [JsonProperty("odata.nextlink")]
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
