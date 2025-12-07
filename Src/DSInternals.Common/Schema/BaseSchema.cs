namespace DSInternals.Common.Schema;

public class BaseSchema
{
    /// <summary>
    /// Gets the OID prefix table.
    /// </summary>
    public PrefixTable PrefixTable
    {
        get;
        private set;
    }

    protected IDictionary<AttributeType, AttributeSchema> AttributesById
    {
        get;
        private set;
    }

    protected IDictionary<AttributeType, AttributeSchema> AttributesByInternalId
    {
        get;
        private set;
    }

    protected IDictionary<string, AttributeSchema> AttributesByName
    {
        get;
        private set;
    }

    protected IDictionary<string, ClassType> ClassesByName
    {
        get;
        private set;
    }

    // Static schema only contains a limited set of attributes and classes.
    protected virtual int InitialAttributeDictionaryCapacity => 150;
    protected virtual int InitialClassDictionaryCapacity => 20;

    protected BaseSchema()
    {
        AttributesByName = new Dictionary<string, AttributeSchema>(InitialAttributeDictionaryCapacity, StringComparer.InvariantCultureIgnoreCase);
        AttributesById = new Dictionary<AttributeType, AttributeSchema>(InitialAttributeDictionaryCapacity);
        AttributesByInternalId = new Dictionary<AttributeType, AttributeSchema>(InitialAttributeDictionaryCapacity);
        ClassesByName = new Dictionary<string, ClassType>(InitialClassDictionaryCapacity, StringComparer.InvariantCultureIgnoreCase);
        PrefixTable = new PrefixTable();
    }

    /// <summary>
    /// Gets all Active Directory schema attributes.
    /// </summary>
    public ICollection<AttributeSchema> FindAllAttributes()
    {
        return AttributesByName.Values;
    }

    /// <summary>
    /// Finds an attribute by its LDAP display name.
    /// </summary>
    public AttributeSchema? FindAttribute(string attributeName)
    {
        Validator.AssertNotNullOrWhiteSpace(attributeName, nameof(attributeName));
        bool found = AttributesByName.TryGetValue(attributeName, out AttributeSchema? attribute);
        return found ? attribute : null;
    }

    /// <summary>
    /// Finds an attribute by its attribute type (OID-based identifier).
    /// </summary>
    public AttributeSchema? FindAttribute(AttributeType attributeId)
    {
        // Try to find the attribute either by attributeID or msDS-IntId.
        var attributeDictionary = attributeId.IsCompressedOid() ? AttributesById : AttributesByInternalId;
        bool attributeFound = attributeDictionary.TryGetValue(attributeId, out AttributeSchema? attribute);
        return attributeFound ? attribute : null;
    }

    /// <summary>
    /// Retrieves the identifier of the specified attribute type by its name, if it exists.
    /// </summary>
    public AttributeType? FindAttributeId(string attributeName)
    {
        var attribute = FindAttribute(attributeName);

        // Prefer the internal ID if it exists.
        return attribute?.InternalId ?? attribute?.AttributeId;
    }

    public ClassType? FindClass(string className)
    {
        bool found = ClassesByName.TryGetValue(className, out ClassType classType);
        return found ? classType : null;
    }

    protected void AddAttribute(AttributeSchema attribute)
    {
        if (attribute == null) throw new ArgumentNullException(nameof(attribute));

        // This overwrites any pre-existing values and does not throw any exception for duplicate adds.
        AttributesByName[attribute.Name] = attribute;
        AttributesById[attribute.AttributeId] = attribute;

        if (attribute.InternalId.HasValue)
        {
            AttributesByInternalId[attribute.InternalId.Value] = attribute;
        }
    }

    public static BaseSchema Create()
    {
        var schema = new BaseSchema();
        var prefixTable = schema.PrefixTable;

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

        // Add classes
        schema.ClassesByName[CommonDirectoryClasses.User] = ClassType.User;
        schema.ClassesByName[CommonDirectoryClasses.Secret] = ClassType.Secret;
        schema.ClassesByName[CommonDirectoryClasses.ClassSchema] = ClassType.ClassSchema;
        schema.ClassesByName[CommonDirectoryClasses.AttributeSchema] = ClassType.AttributeSchema;
        schema.ClassesByName[CommonDirectoryClasses.Schema] = ClassType.Schema;
        schema.ClassesByName[CommonDirectoryClasses.KdsRootKey] = ClassType.KdsRootKey;
        schema.ClassesByName[CommonDirectoryClasses.GroupManagedServiceAccount] = ClassType.GroupManagedServiceAccount;
        schema.ClassesByName[CommonDirectoryClasses.DelegatedManagedServiceAccount] = ClassType.DelegatedManagedServiceAccount;
        schema.ClassesByName[CommonDirectoryClasses.FVERecoveryInformation] = ClassType.FVERecoveryInformation;
        schema.ClassesByName[CommonDirectoryClasses.DnsZone] = ClassType.DnsZone;
        schema.ClassesByName[CommonDirectoryClasses.DnsNode] = ClassType.DnsNode;
        schema.ClassesByName[CommonDirectoryClasses.NtdsSettings] = ClassType.NtdsSettings;
        schema.ClassesByName[CommonDirectoryClasses.NtdsSettingsRO] = ClassType.NtdsSettingsRO;

        return schema;
    }
}
