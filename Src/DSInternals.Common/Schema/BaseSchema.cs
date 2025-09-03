using System;
using System.Collections.Generic;

namespace DSInternals.Common.Schema
{
    /// <summary>
    /// Represents a BaseSchema.
    /// </summary>
    public class BaseSchema
    {
        private const int InitialDictionaryCapacity = 150;
        private PrefixTable _prefixTable;
        private IDictionary<string, AttributeSchema> _attributesByName;
        private IDictionary<AttributeType, AttributeSchema> _attributesById;

        public BaseSchema()
        {
            _attributesByName = new Dictionary<string, AttributeSchema>(InitialDictionaryCapacity, StringComparer.InvariantCultureIgnoreCase);
            _attributesById = new Dictionary<AttributeType, AttributeSchema>(InitialDictionaryCapacity);
            _prefixTable = new PrefixTable();
        }

        /// <summary>
        /// AddAttribute implementation.
        /// </summary>
        public void AddAttribute(AttributeSchema attribute)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));

            // This overwrites any pre-existing values and does not throw any exception for duplicate adds.
            _attributesByName[attribute.Name] = attribute;
            _attributesById[attribute.AttributeId] = attribute;
        }

        /// <summary>
        /// FindAttribute implementation.
        /// </summary>
        public AttributeSchema? FindAttribute(string attributeName)
        {
            _ = _attributesByName.TryGetValue(attributeName, out AttributeSchema attribute);
            return attribute;
        }

        /// <summary>
        /// FindAttributeId implementation.
        /// </summary>
        public AttributeType? FindAttributeId(string attributeName)
        {
            _ = _attributesByName.TryGetValue(attributeName, out AttributeSchema attribute);
            return attribute?.AttributeId;
        }

        /// <summary>
        /// FindAttributeInternalId implementation.
        /// </summary>
        public AttributeType? FindAttributeInternalId(string attributeName)
        {
            _ = _attributesByName.TryGetValue(attributeName, out AttributeSchema attribute);
            return attribute?.InternalId;
        }

        /// <summary>
        /// FindAttribute implementation.
        /// </summary>
        public AttributeSchema? FindAttribute(AttributeType attributeId)
        {
            _ = _attributesById.TryGetValue(attributeId, out AttributeSchema attribute);
            return attribute;
        }

        /// <summary>
        /// LoadPrefixTable implementation.
        /// </summary>
        public void LoadPrefixTable(byte[] blob)
        {
            _prefixTable.LoadFromBlob(blob);

            // After extending the prefix table, we can add attributes that use non-default prefixes.
            AddNonDefaultAttributes();
        }

        /// <summary>
        /// AddPrefix implementation.
        /// </summary>
        public void AddPrefix(ushort index, byte[] oidPrefix)
        {
            _prefixTable.Add(index, oidPrefix);

            // After extending the prefix table, we can add attributes that use non-default prefixes.
            AddNonDefaultAttributes();
        }

        /// <summary>
        /// Adds attributes with non-default prefixes to the schema.
        /// </summary>
        private void AddNonDefaultAttributes()
        {
            // Legacy LAPS
            AttributeType? lapsPasswordId = _prefixTable.TranslateToAttributeType(CommonDirectoryAttributes.LAPSPasswordOid);

            if (lapsPasswordId.HasValue)
            {
                // The prefix table contains Legacy LAPS OID prefix.
                this.AddAttribute(new AttributeSchema(
                    CommonDirectoryAttributes.LAPSPassword,
                    CommonDirectoryAttributes.LAPSPasswordOid,
                    lapsPasswordId.Value, // Already resolved, so do not use Create
                    AttributeSyntax.DNWithBinary));

                this.AddAttribute(AttributeSchema.Create(
                    CommonDirectoryAttributes.LAPSPasswordExpirationTime,
                    CommonDirectoryAttributes.LAPSPasswordExpirationTimeOid,
                    AttributeSyntax.Int64,
                    _prefixTable));
            }

            // Windows LAPS
            AttributeType? windowsLapsPasswordId = _prefixTable.TranslateToAttributeType(CommonDirectoryAttributes.WindowsLapsPasswordOid);

            if (windowsLapsPasswordId.HasValue)
            {
                // The prefix table contains Windows LAPS OID prefix.
                this.AddAttribute(new AttributeSchema(
                    CommonDirectoryAttributes.WindowsLapsPassword,
                    CommonDirectoryAttributes.WindowsLapsPasswordOid,
                    windowsLapsPasswordId.Value, // Already resolved, so do not use Create
                    AttributeSyntax.String));

                this.AddAttribute(AttributeSchema.Create(
                    CommonDirectoryAttributes.WindowsLapsEncryptedPassword,
                    CommonDirectoryAttributes.WindowsLapsEncryptedPasswordOid,
                    AttributeSyntax.OctetString,
                    _prefixTable));

                this.AddAttribute(AttributeSchema.Create(
                    CommonDirectoryAttributes.WindowsLapsEncryptedPasswordHistory,
                    CommonDirectoryAttributes.WindowsLapsEncryptedPasswordHistoryOid,
                    AttributeSyntax.OctetString,
                    _prefixTable));

                this.AddAttribute(AttributeSchema.Create(
                    CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPassword,
                    CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPasswordOid,
                    AttributeSyntax.OctetString,
                    _prefixTable));

                this.AddAttribute(AttributeSchema.Create(
                    CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPasswordHistory,
                    CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPasswordHistoryOid,
                    AttributeSyntax.OctetString,
                    _prefixTable));

                this.AddAttribute(AttributeSchema.Create(
                    CommonDirectoryAttributes.WindowsLapsPasswordExpirationTime,
                    CommonDirectoryAttributes.WindowsLapsPasswordExpirationTimeOid,
                    AttributeSyntax.Int64,
                    _prefixTable));
            }
        }

        /// <summary>
        /// Create implementation.
        /// </summary>
        public static BaseSchema Create()
        {
            var schema = new BaseSchema();
            var prefixTable = schema._prefixTable;

            // Schema
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.CommonName, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SchemaIdGuid, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.ObjectCategory, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.ObjectClass, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.PrefixMap, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.LdapDisplayName, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.AttributeId, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.GovernsId, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.InternalId, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.LinkId, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.IsSingleValued, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.IsInGlobalCatalog, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SystemOnly, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.AttributeSyntax, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.AttributeOmSyntax, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.RangeLower, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.RangeUpper, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SystemFlags, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.IsDefunct, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SearchFlags, prefixTable));

            // Account
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.RDN, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SamAccountName, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SamAccountType, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.IsDeleted, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SidHistory, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Description, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.UserAccountControl, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.LastLogon, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.PasswordLastSet, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.LastLogonTimestamp, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.UserPrincipalName, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.PrimaryGroupId, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SecurityDescriptor, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.AdminCount, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.ServicePrincipalName, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KeyCredentialLink, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SupportedEncryptionTypes, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.WhenChanged, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.WhenCreated, prefixTable));

            // Secrets
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.NTHash, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.NTHashHistory, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.LMHash, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.LMHashHistory, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.SupplementalCredentials, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.CurrentValue, prefixTable));

            // Credential Roaming
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.PKIRoamingTimeStamp, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.PKIDPAPIMasterKeys, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.PKIAccountCredentials, prefixTable));

            // User
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.DisplayName, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.GivenName, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Surname, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Initials, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.EmployeeId, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.EmployeeNumber, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Office, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.TelephoneNumber, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.EmailAddress, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.HomePhone, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.PagerNumber, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Mobile, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.IpPhone, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.WebPage, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.JobTitle, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Department, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Company, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.HomeDirectory, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.HomeDrive, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.UnixHomeDirectory, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.ProfilePath, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.ScriptPath, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.State, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Street, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.PostOfficeBox, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.City, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.PostalCode, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Country, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Comment, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Manager, prefixTable));

            // Computer
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.ManagedBy, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.Location, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.OperatingSystemName, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.OperatingSystemVersion, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.OperatingSystemHotfix, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.OperatingSystemServicePack, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.DnsHostName, prefixTable));

            // KDS Root Keys
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsVersion, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsDomainController, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsRootKeyData, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsCreationTime, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsEffectiveTime, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsKdfAlgorithmId, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsKdfParameters, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsSecretAgreementAlgorithmId, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsSecretAgreementParameters, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsSecretAgreementPrivateKeyLength, prefixTable));
            schema.AddAttribute(AttributeSchema.Create(CommonDirectoryAttributes.KdsSecretAgreementPublicKeyLength, prefixTable));

            return schema;
        }
    }
}
