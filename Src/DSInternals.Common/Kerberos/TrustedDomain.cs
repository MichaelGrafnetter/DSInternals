using System;
using System.Security.Principal;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.Common.Kerberos
{
    /// <summary>
    /// Represents a trusted domain or realm.
    /// </summary>
    /// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/c9efe39c-f5f9-43e9-9479-941c20d0e590</remarks>
    public class TrustedDomain
    {
        public string DistinguishedName
        {
            get;
            private set;
        }

        /// <summary>
        /// Contains the FQDN of the trusted domain.
        /// </summary>
        /// <remarks>This is a mandatory attribute.</remarks>
        public string TrustPartner
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the flat name of the <see cref="TrustedDomain"/>.
        /// </summary>
        /// <remarks>
        /// For Windows NT operating system domains, the flat name is the NetBIOS name.
        /// For links with non–Windows NT domains, the flat name is the identifying name of that domain or it is NULL.
        /// </remarks>
        public string? FlatName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Security ID (SID) of the <see cref="TrustedDomain"/>.
        /// </summary>
        public SecurityIdentifier? Sid
        {
            get;
            private set;
        }

        /// <summary>
        /// Specifies the direction of a trust.
        /// </summary>
        public TrustDirection Direction
        {
            get;
            private set;
        }

        /// <summary>
        /// Specifies the type of trust, for example, NT or MIT.
        /// </summary>
        public TrustType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Specifies the trust attributes for a trusted domain.
        /// </summary>
        public TrustAttributes Attributes
        {
            get;
            private set;
        }

        public string Source
        {
            get;
            private set;
        }

        public string SourceFlatName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the encryption types supported by this trust relationship.
        /// </summary>
        /// <remarks>Implemented on Windows Server 2008 operating system and later.</remarks>
        public SupportedEncryptionTypes? SupportedEncryptionTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a boolean value indicating whether this <see cref="TrustedDomain"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted
        {
            get;
            private set;
        }

        /// <summary>
        /// Specifies authentication information for the incoming portion of a trust.
        /// </summary>
        public TrustAuthInfos TrustAuthIncoming
        {
            get;
            private set;
        }

        /// <summary>
        /// Specifies authentication information for the outgoing portion of a trust.
        /// </summary>
        public TrustAuthInfos TrustAuthOutgoing
        {
            get;
            private set;
        }

        /// <summary>
        /// Calculates the salt for the incoming trust Kerberos keys.
        /// </summary>
        /// <remarks>Sample: CONTOSO.COMkrbtgtCORP</remarks>
        public string? IncomingTrustKeySalt =>
            this.Source != null && this.FlatName != null
                    ? $"{Source.ToUpperInvariant()}krbtgt{FlatName}"
                    : null;

        /// <summary>
        /// Calculates the salt for the outgoing trust Kerberos keys.
        /// </summary>
        /// <remarks>Sample: CONTOSO.COMkrbtgtCORP</remarks>
        public string? OutgoingTrustKeySalt =>
            this.TrustPartner != null && this.SourceFlatName != null
                    ? $"{TrustPartner.ToUpperInvariant()}krbtgt{SourceFlatName}"
                    : null;

        public TrustedDomain(DirectoryObject dsObject, string dnsDomainName, string netBiosDomainName, DirectorySecretDecryptor pek)
        {
            if (dsObject == null)
                throw new ArgumentNullException(nameof(dsObject));

            if (dnsDomainName == null)
                throw new ArgumentNullException(nameof(dnsDomainName));

            if (netBiosDomainName == null)
                throw new ArgumentNullException(nameof(netBiosDomainName));

            // Cache the source domain DNS and NetBIOS names for Kerberos salt derivation.
            this.Source = dnsDomainName;
            this.SourceFlatName = netBiosDomainName;

            // DN
            this.DistinguishedName = dsObject.DistinguishedName;

            // This is NOT objectSid. MIT Trusts do not have SIDs at all.
            dsObject.ReadAttribute(CommonDirectoryAttributes.SecurityIdentifier, out SecurityIdentifier? sid);
            this.Sid = sid;

            // Deleted:
            dsObject.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out bool isDeleted);
            this.Deleted = isDeleted;

            // SuportedEncryptionTypes:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SupportedEncryptionTypes, out int? numericSupportedEncryptionTypes);
            // Note: The value is stored as int in the DB, but the documentation says that it is an unsigned int
            this.SupportedEncryptionTypes = (SupportedEncryptionTypes?)numericSupportedEncryptionTypes;

            // DNS name of the trust. This is a mandatory attribute.
            dsObject.ReadAttribute(CommonDirectoryAttributes.TrustPartner, out string? trustPartner);
            this.TrustPartner = trustPartner ?? throw new ArgumentNullException(nameof(trustPartner));

            // NetBIOS name of the trust. This is an optional attribute.
            dsObject.ReadAttribute(CommonDirectoryAttributes.TrustPartnerFlatName, out string? flatName);
            this.FlatName = flatName;

            dsObject.ReadAttribute(CommonDirectoryAttributes.TrustDirection, out TrustDirection? trustDirection);
            this.Direction = trustDirection ?? TrustDirection.Disabled; // Treat null as disabled trust

            dsObject.ReadAttribute(CommonDirectoryAttributes.TrustAttributes, out TrustAttributes? trustAttributes);
            this.Attributes = trustAttributes ?? TrustAttributes.None;

            dsObject.ReadAttribute(CommonDirectoryAttributes.TrustType, out TrustType? trustType);
            this.Type = trustType ?? TrustType.Unknown; // Treat null as unknown trust type

            // Try to read and decrypt the trust password
            if (pek != null)
            {
                // Only continue if we have a decryption key
                dsObject.ReadAttribute(CommonDirectoryAttributes.TrustAuthIncoming, out byte[]? trustAuthIncoming);

                if (trustAuthIncoming != null)
                {
                    byte[] trustAuthIncomingBinary = pek.DecryptSecret(trustAuthIncoming);
                    this.TrustAuthIncoming = TrustAuthInfos.Parse(trustAuthIncomingBinary);
                }

                dsObject.ReadAttribute(CommonDirectoryAttributes.TrustAuthOutgoing, out byte[]? trustAuthOutgoing);

                if (trustAuthOutgoing != null)
                {
                    byte[] trustAuthOutgoingBinary = pek.DecryptSecret(trustAuthOutgoing);
                    this.TrustAuthOutgoing = TrustAuthInfos.Parse(trustAuthOutgoingBinary);
                }
            }
        }

        public KerberosCredentialNew? IncomingTrustKeys
        {
            get
            {
                string? salt = this.IncomingTrustKeySalt;
                return salt != null ? this.TrustAuthIncoming.DeriveKerberosKeys(salt) : null;
            }
        }

        public KerberosCredentialNew? OutgoingTrustKeys
        {
            get
            {
                string? salt = this.OutgoingTrustKeySalt;
                return salt != null ? this.TrustAuthOutgoing.DeriveKerberosKeys(salt) : null;
            }
        }
    }
}
