using System;
using Newtonsoft.Json;

namespace DSInternals.Common.AzureAD
{
    public class OdataErrorResponse
    {
        [JsonProperty("odata.error", Required = Required.Always)]
        public ODataError Error
        {
            get;
            private set;
        }

        public Exception GetException()
        {
            return new GraphApiException(this.Error);
        }
    }
}
