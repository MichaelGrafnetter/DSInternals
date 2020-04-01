using System;
using System.Collections.Generic;
using DSInternals.Common.Data;
using Newtonsoft.Json;

namespace DSInternals.Common.AzureAD
{
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class AzureADUser
    {
        [JsonProperty("objectId", Required = Required.Always)]
        public Guid ObjectId
        {
            get;
            private set;
        }

        [JsonProperty("userPrincipalName", Required = Required.Always)]
        public string UserPrincipalName
        {
            get;
            private set;
        }

        [JsonProperty("accountEnabled")]
        public bool Enabled
        {
            get;
            private set;
        }

        [JsonProperty("displayName")]
        public string DisplayName
        {
            get;
            private set;
        }

        [JsonProperty("searchableDeviceKey")]
        public List<KeyCredential> KeyCredentials
        {
            get;
            private set;
        }

        internal void UpdateKeyCredentialReferences()
        {
            if(this.KeyCredentials != null)
            {
                this.KeyCredentials.ForEach(credential => credential.Owner = this.UserPrincipalName);
            }
        }
    }
}
