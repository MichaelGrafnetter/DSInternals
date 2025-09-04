using System;

namespace DSInternals.Common.Schema
{
    public static class CommonDirectoryAttributes
    {
        /// <summary>
        /// The AdminCount.
        /// </summary>
        public const string AdminCount = "adminCount";
        /// <summary>
        /// The AttributeId.
        /// </summary>
        public const string AttributeId = "attributeID";
        /// <summary>
        /// The AttributeOmSyntax.
        /// </summary>
        public const string AttributeOmSyntax = "oMSyntax";
        /// <summary>
        /// The AttributeSyntax.
        /// </summary>
        public const string AttributeSyntax = "attributeSyntax";
        /// <summary>
        /// The CommonName.
        /// </summary>
        public const string CommonName = "cn";
        /// <summary>
        /// The CurrentValue.
        /// </summary>
        public const string CurrentValue = "currentValue";
        /// <summary>
        /// The Description.
        /// </summary>
        public const string Description = "description";
        /// <summary>
        /// The DisplayName.
        /// </summary>
        public const string DisplayName = "displayName";
        /// <summary>
        /// The DistinguishedName.
        /// </summary>
        public const string DistinguishedName = "distinguishedName";
        /// <summary>
        /// The DnsHostName.
        /// </summary>
        public const string DnsHostName = "dNSHostName";
        /// <summary>
        /// The DnsRoot.
        /// </summary>
        public const string DnsRoot = "dnsRoot";
        /// <summary>
        /// The FunctionalLevel.
        /// </summary>
        public const string FunctionalLevel = "msDS-Behavior-Version";
        /// <summary>
        /// The NamingContextName.
        /// </summary>
        public const string NamingContextName = "nCName";
        /// <summary>
        /// The DomainComponent.
        /// </summary>
        public const string DomainComponent = "dc";
        /// <summary>
        /// The DomainNamingContexts.
        /// </summary>
        public const string DomainNamingContexts = "msDS-HasDomainNCs";
        /// <summary>
        /// The GivenName.
        /// </summary>
        public const string GivenName = "givenName";
        /// <summary>
        /// The GovernsId.
        /// </summary>
        public const string GovernsId = "governsID";
        /// <summary>
        /// The InstanceType.
        /// </summary>
        public const string InstanceType = "instanceType";
        /// <summary>
        /// The InternalId.
        /// </summary>
        public const string InternalId = "msDS-IntId";
        /// <summary>
        /// The InvocationId.
        /// </summary>
        public const string InvocationId = "invocationId";
        /// <summary>
        /// The IsDefunct.
        /// </summary>
        public const string IsDefunct = "isDefunct";
        /// <summary>
        /// The IsDeleted.
        /// </summary>
        public const string IsDeleted = "isDeleted";
        /// <summary>
        /// The IsInGlobalCatalog.
        /// </summary>
        public const string IsInGlobalCatalog = "isMemberOfPartialAttributeSet";
        /// <summary>
        /// The IsSingleValued.
        /// </summary>
        public const string IsSingleValued = "isSingleValued";
        /// <summary>
        /// The KdsCreationTime.
        /// </summary>
        public const string KdsCreationTime = "msKds-CreateTime";
        /// <summary>
        /// The KdsDomainController.
        /// </summary>
        public const string KdsDomainController = "msKds-DomainID";
        /// <summary>
        /// The KdsKdfAlgorithmId.
        /// </summary>
        public const string KdsKdfAlgorithmId = "msKds-KDFAlgorithmID";
        /// <summary>
        /// The KdsKdfParameters.
        /// </summary>
        public const string KdsKdfParameters = "msKds-KDFParam";
        /// <summary>
        /// The KdsSecretAgreementPrivateKeyLength.
        /// </summary>
        public const string KdsSecretAgreementPrivateKeyLength = "msKds-PrivateKeyLength";
        /// <summary>
        /// The KdsSecretAgreementPublicKeyLength.
        /// </summary>
        public const string KdsSecretAgreementPublicKeyLength = "msKds-PublicKeyLength";
        /// <summary>
        /// The KdsRootKeyData.
        /// </summary>
        public const string KdsRootKeyData = "msKds-RootKeyData";
        /// <summary>
        /// The KdsSecretAgreementAlgorithmId.
        /// </summary>
        public const string KdsSecretAgreementAlgorithmId = "msKds-SecretAgreementAlgorithmID";
        /// <summary>
        /// The KdsSecretAgreementParameters.
        /// </summary>
        public const string KdsSecretAgreementParameters = "msKds-SecretAgreementParam";
        /// <summary>
        /// The KdsEffectiveTime.
        /// </summary>
        public const string KdsEffectiveTime = "msKds-UseStartTime";
        /// <summary>
        /// The KdsVersion.
        /// </summary>
        public const string KdsVersion = "msKds-Version";
        /// <summary>
        /// The KeyCredentialLink.
        /// </summary>
        public const string KeyCredentialLink = "msDS-KeyCredentialLink";
        /// <summary>
        /// The LastLogon.
        /// </summary>
        public const string LastLogon = "lastLogon";
        /// <summary>
        /// The LastLogonTimestamp.
        /// </summary>
        public const string LastLogonTimestamp = "lastLogonTimestamp";
        /// <summary>
        /// The LdapDisplayName.
        /// </summary>
        public const string LdapDisplayName = "lDAPDisplayName";
        /// <summary>
        /// The LinkId.
        /// </summary>
        public const string LinkId = "linkID";
        /// <summary>
        /// The LMHash.
        /// </summary>
        public const string LMHash = "dBCSPwd";
        /// <summary>
        /// The LMHashHistory.
        /// </summary>
        public const string LMHashHistory = "lmPwdHistory";
        /// <summary>
        /// The LockoutTime.
        /// </summary>
        public const string LockoutTime = "lockoutTime";
        /// <summary>
        /// The ManagedPasswordId.
        /// </summary>
        public const string ManagedPasswordId = "msDS-ManagedPasswordId";
        /// <summary>
        /// The ManagedPasswordPreviousId.
        /// </summary>
        public const string ManagedPasswordPreviousId = "msDS-ManagedPasswordPreviousId";
        /// <summary>
        /// The ManagedPasswordInterval.
        /// </summary>
        public const string ManagedPasswordInterval = "msDS-ManagedPasswordInterval";
        /// <summary>
        /// The MasterNamingContexts.
        /// </summary>
        public const string MasterNamingContexts = "msDS-hasMasterNCs";
        /// <summary>
        /// The Member.
        /// </summary>
        public const string Member = "member";
        /// <summary>
        /// The NetBIOSName.
        /// </summary>
        public const string NetBIOSName = "nETBIOSName";
        /// <summary>
        /// The NTHash.
        /// </summary>
        public const string NTHash = "unicodePwd";
        /// <summary>
        /// The NTHashHistory.
        /// </summary>
        public const string NTHashHistory = "ntPwdHistory";
        /// <summary>
        /// The ObjectCategory.
        /// </summary>
        public const string ObjectCategory = "objectCategory";
        /// <summary>
        /// The ObjectClass.
        /// </summary>
        public const string ObjectClass = "objectClass";
        /// <summary>
        /// The ObjectGuid.
        /// </summary>
        public const string ObjectGuid = "objectGUID";
        /// <summary>
        /// The ObjectSid.
        /// </summary>
        public const string ObjectSid = "objectSid";
        /// <summary>
        /// The OperatingSystemName.
        /// </summary>
        public const string OperatingSystemName = "operatingSystem";
        /// <summary>
        /// The Options.
        /// </summary>
        public const string Options = "options";
        /// <summary>
        /// The OrganizationalUnitName.
        /// </summary>
        public const string OrganizationalUnitName = "ou";
        /// <summary>
        /// The PasswordLastSet.
        /// </summary>
        public const string PasswordLastSet = "pwdLastSet";
        /// <summary>
        /// The PEKList.
        /// </summary>
        public const string PEKList = "pekList";
        /// <summary>
        /// The PEKChangeInterval.
        /// </summary>
        public const string PEKChangeInterval = "pekKeyChangeInterval";
        /// <summary>
        /// The PKIRoamingTimeStamp.
        /// </summary>
        public const string PKIRoamingTimeStamp = "msPKIRoamingTimeStamp";
        /// <summary>
        /// The PKIDPAPIMasterKeys.
        /// </summary>
        public const string PKIDPAPIMasterKeys = "msPKIDPAPIMasterKeys";
        /// <summary>
        /// The PKIAccountCredentials.
        /// </summary>
        public const string PKIAccountCredentials = "msPKIAccountCredentials";
        /// <summary>
        /// The PrefixMap.
        /// </summary>
        public const string PrefixMap = "prefixMap";
        /// <summary>
        /// The PrimaryGroupId.
        /// </summary>
        public const string PrimaryGroupId = "primaryGroupID";
        /// <summary>
        /// The ReplicationPropertyMetaData.
        /// </summary>
        public const string ReplicationPropertyMetaData = "replPropertyMetaData";
        /// <summary>
        /// The RangeLower.
        /// </summary>
        public const string RangeLower = "rangeLower";
        /// <summary>
        /// The RangeUpper.
        /// </summary>
        public const string RangeUpper = "rangeUpper";
        /// <summary>
        /// The RDN.
        /// </summary>
        public const string RDN = "name";
        /// <summary>
        /// The SamAccountName.
        /// </summary>
        public const string SamAccountName = "sAMAccountName";
        /// <summary>
        /// The SamAccountType.
        /// </summary>
        public const string SamAccountType = "sAMAccountType";
        /// <summary>
        /// The SchemaLocation.
        /// </summary>
        public const string SchemaLocation = "dMDLocation";
        /// <summary>
        /// The SearchFlags.
        /// </summary>
        public const string SearchFlags = "searchFlags";
        /// <summary>
        /// The SecurityDescriptor.
        /// </summary>
        public const string SecurityDescriptor = "nTSecurityDescriptor";
        /// <summary>
        /// The SecurityIdentifier.
        /// </summary>
        public const string SecurityIdentifier = "securityIdentifier";
        /// <summary>
        /// The ServerReference.
        /// </summary>
        public const string ServerReference = "serverReference";
        /// <summary>
        /// The SchemaIdGuid.
        /// </summary>
        public const string SchemaIdGuid = "schemaIDGUID";
        /// <summary>
        /// The ServicePrincipalName.
        /// </summary>
        public const string ServicePrincipalName = "servicePrincipalName";
        /// <summary>
        /// The SidHistory.
        /// </summary>
        public const string SidHistory = "sIDHistory";
        /// <summary>
        /// The SupplementalCredentials.
        /// </summary>
        public const string SupplementalCredentials = "supplementalCredentials";
        /// <summary>
        /// The SupportedEncryptionTypes.
        /// </summary>
        public const string SupportedEncryptionTypes = "msDS-SupportedEncryptionTypes";
        /// <summary>
        /// The Surname.
        /// </summary>
        public const string Surname = "sn";
        /// <summary>
        /// The SystemFlags.
        /// </summary>
        public const string SystemFlags = "systemFlags";
        /// <summary>
        /// The SystemOnly.
        /// </summary>
        public const string SystemOnly = "systemOnly";
        /// <summary>
        /// The UnixUserPassword.
        /// </summary>
        public const string UnixUserPassword = "unixUserPassword";
        /// <summary>
        /// The UserAccountControl.
        /// </summary>
        public const string UserAccountControl = "userAccountControl";
        /// <summary>
        /// The UserPrincipalName.
        /// </summary>
        public const string UserPrincipalName = "userPrincipalName";
        /// <summary>
        /// The USNCreated.
        /// </summary>
        public const string USNCreated = "uSNCreated";
        /// <summary>
        /// The USNChanged.
        /// </summary>
        public const string USNChanged = "uSNChanged";
        /// <summary>
        /// The WhenCreated.
        /// </summary>
        public const string WhenCreated = "whenCreated";
        /// <summary>
        /// The WhenChanged.
        /// </summary>
        public const string WhenChanged = "whenChanged";
        /// <summary>
        /// The Initials.
        /// </summary>
        public const string Initials = "initials";
        /// <summary>
        /// The EmployeeId.
        /// </summary>
        public const string EmployeeId = "employeeID";
        /// <summary>
        /// The EmployeeNumber.
        /// </summary>
        public const string EmployeeNumber = "employeeNumber";
        /// <summary>
        /// The Office.
        /// </summary>
        public const string Office = "physicalDeliveryOfficeName";
        /// <summary>
        /// The TelephoneNumber.
        /// </summary>
        public const string TelephoneNumber = "telephoneNumber";
        /// <summary>
        /// The EmailAddress.
        /// </summary>
        public const string EmailAddress = "mail";
        /// <summary>
        /// The HomePhone.
        /// </summary>
        public const string HomePhone = "homePhone";
        /// <summary>
        /// The PagerNumber.
        /// </summary>
        public const string PagerNumber = "pager";
        /// <summary>
        /// The Mobile.
        /// </summary>
        public const string Mobile = "mobile";
        /// <summary>
        /// The IpPhone.
        /// </summary>
        public const string IpPhone = "ipPhone";
        /// <summary>
        /// The WebPage.
        /// </summary>
        public const string WebPage = "wWWHomePage";
        /// <summary>
        /// The JobTitle.
        /// </summary>
        public const string JobTitle = "title";
        /// <summary>
        /// The Department.
        /// </summary>
        public const string Department = "department";
        /// <summary>
        /// The Company.
        /// </summary>
        public const string Company = "company";
        /// <summary>
        /// The Manager.
        /// </summary>
        public const string Manager = "manager";
        /// <summary>
        /// The HomeDirectory.
        /// </summary>
        public const string HomeDirectory = "homeDirectory";
        /// <summary>
        /// The HomeDrive.
        /// </summary>
        public const string HomeDrive = "homeDrive";
        /// <summary>
        /// The UnixHomeDirectory.
        /// </summary>
        public const string UnixHomeDirectory = "unixHomeDirectory";
        /// <summary>
        /// The ProfilePath.
        /// </summary>
        public const string ProfilePath = "profilePath";
        /// <summary>
        /// The ScriptPath.
        /// </summary>
        public const string ScriptPath = "scriptPath";
        /// <summary>
        /// The State.
        /// </summary>
        public const string State = "st";
        /// <summary>
        /// The Street.
        /// </summary>
        public const string Street = "streetAddress";
        /// <summary>
        /// The PostOfficeBox.
        /// </summary>
        public const string PostOfficeBox = "postOfficeBox";
        /// <summary>
        /// The City.
        /// </summary>
        public const string City = "l";
        /// <summary>
        /// The Country.
        /// </summary>
        public const string Country = "c";
        /// <summary>
        /// The Comment.
        /// </summary>
        public const string Comment = "info";
        /// <summary>
        /// The PostalCode.
        /// </summary>
        public const string PostalCode = "postalCode";
        /// <summary>
        /// The ManagedBy.
        /// </summary>
        public const string ManagedBy = "managedBy";
        /// <summary>
        /// The Location.
        /// </summary>
        public const string Location = "location";
        /// <summary>
        /// The OperatingSystemVersion.
        /// </summary>
        public const string OperatingSystemVersion = "operatingSystemVersion";
        /// <summary>
        /// The OperatingSystemHotfix.
        /// </summary>
        public const string OperatingSystemHotfix = "operatingSystemHotfix";
        /// <summary>
        /// The OperatingSystemServicePack.
        /// </summary>
        public const string OperatingSystemServicePack = "operatingSystemServicePack";
        /// <summary>
        /// The TPMOwnerInformation.
        /// </summary>
        public const string TPMOwnerInformation = "msTPM-OwnerInformation";
        /// <summary>
        /// The TPMInformationForComputer.
        /// </summary>
        public const string TPMInformationForComputer = "msTPM-TpmInformationForComputer";
        /// <summary>
        /// The TrustAttributes.
        /// </summary>
        public const string TrustAttributes = "trustAttributes";
        /// <summary>
        /// The TrustAuthIncoming.
        /// </summary>
        public const string TrustAuthIncoming = "trustAuthIncoming";
        /// <summary>
        /// The TrustAuthOutgoing.
        /// </summary>
        public const string TrustAuthOutgoing = "trustAuthOutgoing";
        /// <summary>
        /// The TrustDirection.
        /// </summary>
        public const string TrustDirection = "trustDirection";
        /// <summary>
        /// The TrustPartner.
        /// </summary>
        public const string TrustPartner = "trustPartner";
        /// <summary>
        /// The TrustPartnerFlatName.
        /// </summary>
        public const string TrustPartnerFlatName = "flatName";
        /// <summary>
        /// The TrustPosixOffset.
        /// </summary>
        public const string TrustPosixOffset = "trustPosixOffset";
        /// <summary>
        /// The TrustType.
        /// </summary>
        public const string TrustType = "trustType";
        /// <summary>
        /// The FVEKeyPackage.
        /// </summary>
        public const string FVEKeyPackage = "msFVE-KeyPackage";
        /// <summary>
        /// The FVEVolumeGuid.
        /// </summary>
        public const string FVEVolumeGuid = "msFVE-VolumeGuid";
        /// <summary>
        /// The FVERecoveryGuid.
        /// </summary>
        public const string FVERecoveryGuid = "msFVE-RecoveryGuid";
        /// <summary>
        /// The FVERecoveryPassword.
        /// </summary>
        public const string FVERecoveryPassword = "msFVE-RecoveryPassword";
        /// <summary>
        /// The LAPSPassword.
        /// </summary>
        public const string LAPSPassword = "ms-Mcs-AdmPwd";
        /// <summary>
        /// The LAPSPasswordOid.
        /// </summary>
        public const string LAPSPasswordOid = "1.2.840.113556.1.8000.2554.50051.45980.28112.18903.35903.6685103.1224907.2.1";
        /// <summary>
        /// The LAPSPasswordExpirationTime.
        /// </summary>
        public const string LAPSPasswordExpirationTime = "ms-Mcs-AdmPwdExpirationTime";
        /// <summary>
        /// The LAPSPasswordExpirationTimeOid.
        /// </summary>
        public const string LAPSPasswordExpirationTimeOid = "1.2.840.113556.1.8000.2554.50051.45980.28112.18903.35903.6685103.1224907.2.2";
        /// <summary>
        /// The WindowsLapsPasswordExpirationTime.
        /// </summary>
        public const string WindowsLapsPasswordExpirationTime = "msLAPS-PasswordExpirationTime";
        /// <summary>
        /// The WindowsLapsPasswordExpirationTimeOid.
        /// </summary>
        public const string WindowsLapsPasswordExpirationTimeOid = "1.2.840.113556.1.6.44.1.1";
        /// <summary>
        /// The WindowsLapsPassword.
        /// </summary>
        public const string WindowsLapsPassword = "msLAPS-Password";
        /// <summary>
        /// The WindowsLapsPasswordOid.
        /// </summary>
        public const string WindowsLapsPasswordOid = "1.2.840.113556.1.6.44.1.2";
        /// <summary>
        /// The WindowsLapsEncryptedPassword.
        /// </summary>
        public const string WindowsLapsEncryptedPassword = "msLAPS-EncryptedPassword";
        /// <summary>
        /// The WindowsLapsEncryptedPasswordOid.
        /// </summary>
        public const string WindowsLapsEncryptedPasswordOid = "1.2.840.113556.1.6.44.1.3";
        /// <summary>
        /// The WindowsLapsEncryptedPasswordHistory.
        /// </summary>
        public const string WindowsLapsEncryptedPasswordHistory = "msLAPS-EncryptedPasswordHistory";
        /// <summary>
        /// The WindowsLapsEncryptedPasswordHistoryOid.
        /// </summary>
        public const string WindowsLapsEncryptedPasswordHistoryOid = "1.2.840.113556.1.6.44.1.4";
        /// <summary>
        /// The WindowsLapsEncryptedDsrmPassword.
        /// </summary>
        public const string WindowsLapsEncryptedDsrmPassword = "msLAPS-EncryptedDSRMPassword";
        /// <summary>
        /// The WindowsLapsEncryptedDsrmPasswordOid.
        /// </summary>
        public const string WindowsLapsEncryptedDsrmPasswordOid = "1.2.840.113556.1.6.44.1.5";
        /// <summary>
        /// The WindowsLapsEncryptedDsrmPasswordHistory.
        /// </summary>
        public const string WindowsLapsEncryptedDsrmPasswordHistory = "msLAPS-EncryptedDSRMPasswordHistory";
        /// <summary>
        /// The WindowsLapsEncryptedDsrmPasswordHistoryOid.
        /// </summary>
        public const string WindowsLapsEncryptedDsrmPasswordHistoryOid = "1.2.840.113556.1.6.44.1.6";
        /// <summary>
        /// The WindowsLapsCurrentPasswordVersion.
        /// </summary>
        public const string WindowsLapsCurrentPasswordVersion = "msLAPS-CurrentPasswordVersion";
        /// <summary>
        /// The WindowsLapsCurrentPasswordVersionOid.
        /// </summary>
        public const string WindowsLapsCurrentPasswordVersionOid = "1.2.840.113556.1.6.44.1.7";
        /// <summary>
        /// The DnsRecord.
        /// </summary>
        public const string DnsRecord = "dnsRecord";
        /// <summary>
        /// The DnsTombstoned.
        /// </summary>
        public const string DnsTombstoned = "dNSTombstoned";

        /// <summary>
        /// Translates the input to the target format.
        /// </summary>
        public static AttributeType? Translate(string ldapDisplayName)
        {
            if (ldapDisplayName == null) throw new ArgumentNullException(nameof(ldapDisplayName));

            return ldapDisplayName switch
            {
                AdminCount => AttributeType.AdminCount,
                AttributeId => AttributeType.AttributeId,
                AttributeOmSyntax => AttributeType.OMSyntax,
                AttributeSyntax => AttributeType.AttributeSyntax,
                CommonName => AttributeType.CommonName,
                CurrentValue => AttributeType.CurrentValue,
                Description => AttributeType.Description,
                DisplayName => AttributeType.DisplayName,
                DistinguishedName => AttributeType.DistinguishedName,
                DnsHostName => AttributeType.DnsHostName,
                DnsRoot => AttributeType.DnsRoot,
                FunctionalLevel => AttributeType.FunctionalLevel,
                NamingContextName => AttributeType.NamingContextName,
                DomainComponent => AttributeType.DomainComponent,
                DomainNamingContexts => AttributeType.DomainNamingContexts,
                GivenName => AttributeType.GivenName,
                GovernsId => AttributeType.GovernsId,
                InstanceType => AttributeType.InstanceType,
                InternalId => AttributeType.InternalId,
                InvocationId => AttributeType.InvocationId,
                IsDefunct => AttributeType.IsDefunct,
                IsDeleted => AttributeType.IsDeleted,
                IsInGlobalCatalog => AttributeType.IsInGlobalCatalog,
                IsSingleValued => AttributeType.IsSingleValued,
                KdsCreationTime => AttributeType.KdsCreationTime,
                KdsDomainController => AttributeType.KdsDomainController,
                KdsKdfAlgorithmId => AttributeType.KdsKdfAlgorithmId,
                KdsKdfParameters => AttributeType.KdsKdfParameters,
                KdsSecretAgreementPrivateKeyLength => AttributeType.KdsSecretAgreementPrivateKeyLength,
                KdsSecretAgreementPublicKeyLength => AttributeType.KdsSecretAgreementPublicKeyLength,
                KdsRootKeyData => AttributeType.KdsRootKeyData,
                KdsSecretAgreementAlgorithmId => AttributeType.KdsSecretAgreementAlgorithmId,
                KdsSecretAgreementParameters => AttributeType.KdsSecretAgreementParameters,
                KdsEffectiveTime => AttributeType.KdsEffectiveTime,
                KdsVersion => AttributeType.KdsVersion,
                KeyCredentialLink => AttributeType.KeyCredentialLink,
                LastLogon => AttributeType.LastLogon,
                LastLogonTimestamp => AttributeType.LastLogonTimestamp,
                LdapDisplayName => AttributeType.LdapDisplayName,
                LinkId => AttributeType.LinkId,
                LMHash => AttributeType.LMHash,
                LMHashHistory => AttributeType.LMHashHistory,
                LockoutTime => AttributeType.LockoutTime,
                ManagedPasswordId => AttributeType.ManagedPasswordId,
                ManagedPasswordPreviousId => AttributeType.ManagedPasswordPreviousId,
                ManagedPasswordInterval => AttributeType.ManagedPasswordInterval,
                MasterNamingContexts => AttributeType.MasterNamingContexts,
                Member => AttributeType.Member,
                RDN => AttributeType.RDN,
                NetBIOSName => AttributeType.NetBIOSName,
                NTHash => AttributeType.NTHash,
                NTHashHistory => AttributeType.NTHashHistory,
                ObjectCategory => AttributeType.ObjectCategory,
                ObjectClass => AttributeType.ObjectClass,
                ObjectGuid => AttributeType.ObjectGuid,
                ObjectSid => AttributeType.ObjectSid,
                OperatingSystemName => AttributeType.OperatingSystemName,
                Options => AttributeType.Options,
                OrganizationalUnitName => AttributeType.OrganizationalUnitName,
                PasswordLastSet => AttributeType.PasswordLastSet,
                PEKList => AttributeType.PEKList,
                PEKChangeInterval => AttributeType.PEKChangeInterval,
                PKIRoamingTimeStamp => AttributeType.PKIRoamingTimeStamp,
                PKIDPAPIMasterKeys => AttributeType.PKIDPAPIMasterKeys,
                PKIAccountCredentials => AttributeType.PKIAccountCredentials,
                PrefixMap => AttributeType.PrefixMap,
                PrimaryGroupId => AttributeType.PrimaryGroupId,
                RangeLower => AttributeType.RangeLower,
                RangeUpper => AttributeType.RangeUpper,
                ReplicationPropertyMetaData => AttributeType.ReplicationPropertyMetaData,
                SamAccountName => AttributeType.SamAccountName,
                SamAccountType => AttributeType.SamAccountType,
                SchemaLocation => AttributeType.SchemaLocation,
                SearchFlags => AttributeType.SearchFlags,
                SecurityDescriptor => AttributeType.SecurityDescriptor,
                ServerReference => AttributeType.ServerReference,
                SchemaIdGuid => AttributeType.SchemaIdGuid,
                ServicePrincipalName => AttributeType.ServicePrincipalName,
                SidHistory => AttributeType.SidHistory,
                SupplementalCredentials => AttributeType.SupplementalCredentials,
                SupportedEncryptionTypes => AttributeType.SupportedEncryptionTypes,
                Surname => AttributeType.Surname,
                SystemFlags => AttributeType.SystemFlags,
                SystemOnly => AttributeType.SystemOnly,
                UnixUserPassword => AttributeType.UnixUserPassword,
                UserAccountControl => AttributeType.UserAccountControl,
                UserPrincipalName => AttributeType.UserPrincipalName,
                USNCreated => AttributeType.USNCreated,
                USNChanged => AttributeType.USNChanged,
                WhenCreated => AttributeType.WhenCreated,
                WhenChanged => AttributeType.WhenChanged,
                Initials => AttributeType.Initials,
                EmployeeId => AttributeType.EmployeeId,
                EmployeeNumber => AttributeType.EmployeeNumber,
                Office => AttributeType.Office,
                TelephoneNumber => AttributeType.TelephoneNumber,
                EmailAddress => AttributeType.EmailAddresses,
                HomePhone => AttributeType.PhoneHomePrimary,
                PagerNumber => AttributeType.PhonePagerPrimary,
                Mobile => AttributeType.PhoneMobilePrimary,
                IpPhone => AttributeType.PhoneIpPrimary,
                WebPage => AttributeType.WebPage,
                JobTitle => AttributeType.JobTitle,
                Department => AttributeType.Department,
                Company => AttributeType.Company,
                Manager => AttributeType.Manager,
                HomeDirectory => AttributeType.HomeDirectory,
                HomeDrive => AttributeType.HomeDrive,
                UnixHomeDirectory => AttributeType.UnixHomeDirectory,
                ProfilePath => AttributeType.ProfilePath,
                ScriptPath => AttributeType.ScriptPath,
                State => AttributeType.State,
                Street => AttributeType.Street,
                PostOfficeBox => AttributeType.PostOfficeBox,
                City => AttributeType.City,
                Country => AttributeType.Country,
                Comment => AttributeType.Comment,
                PostalCode => AttributeType.PostalCode,
                ManagedBy => AttributeType.ManagedBy,
                Location => AttributeType.Location,
                OperatingSystemVersion => AttributeType.OperatingSystemVersion,
                OperatingSystemHotfix => AttributeType.OperatingSystemHotfix,
                OperatingSystemServicePack => AttributeType.OperatingSystemServicePack,
                TPMOwnerInformation => AttributeType.TPMOwnerInformation,
                TPMInformationForComputer => AttributeType.TPMInformationForComputer,
                FVEKeyPackage => AttributeType.FVEKeyPackage,
                FVEVolumeGuid => AttributeType.FVEVolumeGuid,
                FVERecoveryGuid => AttributeType.FVERecoveryGuid,
                FVERecoveryPassword => AttributeType.FVERecoveryPassword,
                DnsRecord => AttributeType.DnsRecord,
                DnsTombstoned => AttributeType.DnsTombstoned,
                _ => null
            };
        }
    }
}
