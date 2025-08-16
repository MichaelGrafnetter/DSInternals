using System;
using System.Collections.Generic;
using DSInternals.Common.Data;
using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
    public class AzureADUser
    {
        [JsonPropertyName("objectId")]
        [JsonRequired]
        public Guid ObjectId
        {
            get;
            private set;
        }

        [JsonPropertyName("userPrincipalName")]
        [JsonRequired]
        public string UserPrincipalName
        {
            get;
            private set;
        }

        [JsonPropertyName("accountEnabled")]
        public bool Enabled
        {
            get;
            private set;
        }

        [JsonPropertyName("displayName")]
        public string DisplayName
        {
            get;
            private set;
        }

        [JsonPropertyName("searchableDeviceKey")]
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
