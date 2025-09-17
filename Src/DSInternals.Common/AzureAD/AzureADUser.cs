using System;
using System.Collections.Generic;
using DSInternals.Common.Data;
using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
    /// <summary>
    /// Represents an Azure Active Directory user object.
    /// </summary>
    public class AzureADUser
    {
        /// <summary>
        /// Gets the unique identifier (object ID) of the Azure AD user.
        /// </summary>
        [JsonPropertyName("objectId")]
        [JsonRequired]
        public Guid ObjectId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user principal name (UPN) of the Azure AD user.
        /// </summary>
        [JsonPropertyName("userPrincipalName")]
        [JsonRequired]
        public string UserPrincipalName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the Azure AD user account is enabled.
        /// </summary>
        [JsonPropertyName("accountEnabled")]
        public bool Enabled
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the display name of the Azure AD user.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of key credentials (device keys) associated with the Azure AD user.
        /// </summary>
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
