
/// <summary>
/// Any OID-valued quantity is stored as a 32-bit unsigned integer. 
/// </summary>
using ATTRTYP = uint;

namespace DSInternals.Common.Schema;
/// <summary>
/// Compressed representation of a schema attribute OIDs (subset of ATTRTYP).
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/98b55783-7029-4a04-8f8b-9df9344089c3</remarks>
public enum AttributeType : ATTRTYP
{
    /// <summary>
    /// objectClass
    /// </summary>
    ObjectClass = 0,

    /// <summary>
    /// knowledgeInformation
    /// </summary>
    KnowledgeInformation = 2,

    /// <summary>
    /// cn
    /// </summary>
    CommonName = 3,

    /// <summary>
    /// sn
    /// </summary>
    Surname = 4,

    /// <summary>
    /// serialNumber
    /// </summary>
    SerialNumber = 5,

    /// <summary>
    /// c
    /// </summary>
    Country = 6,

    /// <summary>
    /// l
    /// </summary>
    City = 7,

    /// <summary>
    /// st
    /// </summary>
    State = 8,

    /// <summary>
    /// street
    /// </summary>
    /// <remarks>street is indeed swapped with streetAddress in the schema.</remarks>
    StreetAddress = 9,

    /// <summary>
    /// o
    /// </summary>
    OrganizationName = 10,

    /// <summary>
    /// ou
    /// </summary>
    OrganizationalUnitName = 11,

    /// <summary>
    /// title
    /// </summary>
    JobTitle = 12,

    /// <summary>
    /// description
    /// </summary>
    Description = 13,

    /// <summary>
    /// searchGuide
    /// </summary>
    SearchGuide = 14,

    /// <summary>
    /// businessCategory
    /// </summary>
    BusinessCategory = 15,

    /// <summary>
    /// postalAddress
    /// </summary>
    PostalAddress = 16,

    /// <summary>
    /// postalCode
    /// </summary>
    PostalCode = 17,

    /// <summary>
    /// postOfficeBox
    /// </summary>
    PostOfficeBox = 18,

    /// <summary>
    /// physicalDeliveryOfficeName
    /// </summary>
    Office = 19,

    /// <summary>
    /// telephoneNumber
    /// </summary>
    TelephoneNumber = 20,

    /// <summary>
    /// telexNumber
    /// </summary>
    TelexNumber = 21,

    /// <summary>
    /// teletexTerminalIdentifier
    /// </summary>
    TeletexTerminalIdentifier = 22,

    /// <summary>
    /// facsimileTelephoneNumber
    /// </summary>
    FacsimileTelephoneNumber = 23,

    /// <summary>
    /// x121Address
    /// </summary>
    X121Address = 24,

    /// <summary>
    /// internationalISDNNumber
    /// </summary>
    InternationalISDNNumber = 25,

    /// <summary>
    /// registeredAddress
    /// </summary>
    RegisteredAddress = 26,

    /// <summary>
    /// destinationIndicator
    /// </summary>
    DestinationIndicator = 27,

    /// <summary>
    /// preferredDeliveryMethod
    /// </summary>
    PreferredDeliveryMethod = 28,

    /// <summary>
    /// presentationAddress
    /// </summary>
    PresentationAddress = 29,

    /// <summary>
    /// supportedApplicationContext
    /// </summary>
    SupportedApplicationContext = 30,

    /// <summary>
    /// roleOccupant
    /// </summary>
    RoleOccupant = 33,

    /// <summary>
    /// seeAlso
    /// </summary>
    SeeAlso = 34,

    /// <summary>
    /// userPassword
    /// </summary>
    UserPassword = 35,

    /// <summary>
    /// userCertificate
    /// </summary>
    X509Certificate = 36,

    /// <summary>
    /// cACertificate
    /// </summary>
    CACertificate = 37,

    /// <summary>
    /// authorityRevocationList
    /// </summary>
    AuthorityRevocationList = 38,

    /// <summary>
    /// certificateRevocationList
    /// </summary>
    CertificateRevocationList = 39,

    /// <summary>
    /// crossCertificatePair
    /// </summary>
    CrossCertificatePair = 40,

    /// <summary>
    /// givenName
    /// </summary>
    GivenName = 42,

    /// <summary>
    /// initials
    /// </summary>
    Initials = 43,

    /// <summary>
    /// generationQualifier
    /// </summary>
    GenerationQualifier = 44,

    /// <summary>
    /// x500uniqueIdentifier
    /// </summary>
    X500uniqueIdentifier = 45,

    /// <summary>
    /// distinguishedName
    /// </summary>
    DistinguishedName = 49,

    /// <summary>
    /// uniqueMember
    /// </summary>
    UniqueMember = 50,

    /// <summary>
    /// houseIdentifier
    /// </summary>
    HouseIdentifier = 51,

    /// <summary>
    /// deltaRevocationList
    /// </summary>
    DeltaRevocationList = 53,

    /// <summary>
    /// attributeCertificateAttribute
    /// </summary>
    AttributeCertificateAttribute = 58,

    /// <summary>
    /// instanceType
    /// </summary>
    InstanceType = 131073,

    /// <summary>
    /// whenCreated
    /// </summary>
    WhenCreated = 131074,

    /// <summary>
    /// whenChanged
    /// </summary>
    WhenChanged = 131075,

    /// <summary>
    /// subRefs
    /// </summary>
    SubRefs = 131079,

    /// <summary>
    /// possSuperiors
    /// </summary>
    PossibleSuperiors = 131080,

    /// <summary>
    /// helpData32
    /// </summary>
    HelpData32 = 131081,

    /// <summary>
    /// displayName
    /// </summary>
    DisplayName = 131085,

    /// <summary>
    /// nCName
    /// </summary>
    NamingContextName = 131088,

    /// <summary>
    /// otherTelephone
    /// </summary>
    PhoneOfficeOther = 131090,

    /// <summary>
    /// uSNCreated
    /// </summary>
    USNCreated = 131091,

    /// <summary>
    /// subClassOf
    /// </summary>
    SubClassOf = 131093,

    /// <summary>
    /// governsID
    /// </summary>
    GovernsId = 131094,

    /// <summary>
    /// mustContain
    /// </summary>
    MustContain = 131096,

    /// <summary>
    /// mayContain
    /// </summary>
    MayContain = 131097,

    /// <summary>
    /// rDNAttID
    /// </summary>
    RDNAttributeId = 131098,

    /// <summary>
    /// attributeID
    /// </summary>
    AttributeId = 131102,

    /// <summary>
    /// attributeSyntax
    /// </summary>
    AttributeSyntax = 131104,

    /// <summary>
    /// isSingleValued
    /// </summary>
    IsSingleValued = 131105,

    /// <summary>
    /// rangeLower
    /// </summary>
    RangeLower = 131106,

    /// <summary>
    /// rangeUpper
    /// </summary>
    RangeUpper = 131107,

    /// <summary>
    /// dMDLocation
    /// </summary>
    SchemaLocation = 131108,

    /// <summary>
    /// isDeleted
    /// </summary>
    IsDeleted = 131120,

    /// <summary>
    /// mAPIID
    /// </summary>
    MapiId = 131121,

    /// <summary>
    /// linkID
    /// </summary>
    LinkId = 131122,

    /// <summary>
    /// tombstoneLifetime
    /// </summary>
    TombstoneLifetime = 131126,

    /// <summary>
    /// dSASignature
    /// </summary>
    DSASignature = 131146,

    /// <summary>
    /// objectVersion
    /// </summary>
    ObjectVersion = 131148,

    /// <summary>
    /// info
    /// </summary>
    Comment = 131153,

    /// <summary>
    /// repsTo
    /// </summary>
    ReplicatesTo = 131155,

    /// <summary>
    /// repsFrom
    /// </summary>
    ReplicatesFrom = 131163,

    /// <summary>
    /// invocationId
    /// </summary>
    InvocationId = 131187,

    /// <summary>
    /// otherPager
    /// </summary>
    PhonePagerOther = 131190,

    /// <summary>
    /// uSNChanged
    /// </summary>
    USNChanged = 131192,

    /// <summary>
    /// uSNLastObjRem
    /// </summary>
    USNLastObjRem = 131193,

    /// <summary>
    /// co
    /// </summary>
    TextCountry = 131203,

    /// <summary>
    /// cost
    /// </summary>
    Cost = 131207,

    /// <summary>
    /// department
    /// </summary>
    Department = 131213,

    /// <summary>
    /// company
    /// </summary>
    Company = 131218,

    /// <summary>
    /// showInAdvancedViewOnly
    /// </summary>
    ShowInAdvancedViewOnly = 131241,

    /// <summary>
    /// adminDisplayName
    /// </summary>
    AdminDisplayName = 131266,

    /// <summary>
    /// proxyAddresses
    /// </summary>
    ProxyAddresses = 131282,

    /// <summary>
    /// dSHeuristics
    /// </summary>
    DSHeuristics = 131284,

    /// <summary>
    /// originalDisplayTableMSDOS
    /// </summary>
    OriginalDisplayTableMSDOS = 131286,

    /// <summary>
    /// oMObjectClass
    /// </summary>
    OMObjectClass = 131290,

    /// <summary>
    /// adminDescription
    /// </summary>
    AdminDescription = 131298,

    /// <summary>
    /// extensionName
    /// </summary>
    ExtensionName = 131299,

    /// <summary>
    /// oMSyntax
    /// </summary>
    OMSyntax = 131303,

    /// <summary>
    /// addressSyntax
    /// </summary>
    AddressSyntax = 131327,

    /// <summary>
    /// streetAddress
    /// </summary>
    /// <remarks>street is indeed swapped with streetAddress in the schema.</remarks>
    Street = 131328,

    /// <summary>
    /// uSNDSALastObjRemoved
    /// </summary>
    USNDSALastObjRemoved = 131339,

    /// <summary>
    /// otherHomePhone
    /// </summary>
    PhoneHomeOther = 131349,

    /// <summary>
    /// nTSecurityDescriptor
    /// </summary>
    SecurityDescriptor = 131353,

    /// <summary>
    /// garbageCollPeriod
    /// </summary>
    GarbageCollPeriod = 131373,

    /// <summary>
    /// addressEntryDisplayTable
    /// </summary>
    AddressEntryDisplayTable = 131396,

    /// <summary>
    /// perMsgDialogDisplayTable
    /// </summary>
    PerMsgDialogDisplayTable = 131397,

    /// <summary>
    /// perRecipDialogDisplayTable
    /// </summary>
    PerRecipDialogDisplayTable = 131398,

    /// <summary>
    /// helpFileName
    /// </summary>
    HelpFileName = 131399,

    /// <summary>
    /// searchFlags
    /// </summary>
    SearchFlags = 131406,

    /// <summary>
    /// addressType
    /// </summary>
    AddressType = 131422,

    /// <summary>
    /// auxiliaryClass
    /// </summary>
    AuxiliaryClass = 131423,

    /// <summary>
    /// displayNamePrintable
    /// </summary>
    DisplayNamePrintable = 131425,

    /// <summary>
    /// objectClassCategory
    /// </summary>
    ObjectClassCategory = 131442,

    /// <summary>
    /// extendedCharsAllowed
    /// </summary>
    ExtendedCharsAllowed = 131452,

    /// <summary>
    /// addressEntryDisplayTableMSDOS
    /// </summary>
    AddressEntryDisplayTableMSDOS = 131472,

    /// <summary>
    /// helpData16
    /// </summary>
    HelpData16 = 131474,

    /// <summary>
    /// msExchAssistantName
    /// </summary>
    MSExchAssistantName = 131516,

    /// <summary>
    /// originalDisplayTable
    /// </summary>
    OriginalDisplayTable = 131517,

    /// <summary>
    /// networkAddress
    /// </summary>
    NetworkAddress = 131531,

    /// <summary>
    /// lDAPDisplayName
    /// </summary>
    LdapDisplayName = 131532,

    /// <summary>
    /// wWWHomePage
    /// </summary>
    WebPage = 131536,

    /// <summary>
    /// USNIntersite
    /// </summary>
    USNIntersite = 131541,

    /// <summary>
    /// schemaVersion
    /// </summary>
    SchemaVersion = 131543,

    /// <summary>
    /// proxyGenerationEnabled
    /// </summary>
    ProxyGenerationEnabled = 131595,

    /// <summary>
    /// Enabled
    /// </summary>
    Enabled = 131629,

    /// <summary>
    /// msExchLabeledURI
    /// </summary>
    MSExchangeLabeledURI = 131665,

    /// <summary>
    /// msExchHouseIdentifier
    /// </summary>
    MSExchangeHouseIdentifier = 131668,

    /// <summary>
    /// dmdName
    /// </summary>
    DMDName = 131670,

    /// <summary>
    /// employeeNumber
    /// </summary>
    EmployeeNumber = 131682,

    /// <summary>
    /// employeeType
    /// </summary>
    EmployeeType = 131685,

    /// <summary>
    /// personalTitle
    /// </summary>
    PersonalTitle = 131687,

    /// <summary>
    /// homePostalAddress
    /// </summary>
    AddressHome = 131689,

    /// <summary>
    /// name
    /// </summary>
    RDN = 589825,

    /// <summary>
    /// objectGUID
    /// </summary>
    ObjectGuid = 589826,

    /// <summary>
    /// replPropertyMetaData
    /// </summary>
    ReplicationPropertyMetaData = 589827,

    /// <summary>
    /// replUpToDateVector
    /// </summary>
    ReplUpToDateVector = 589828,

    /// <summary>
    /// userAccountControl
    /// </summary>
    UserAccountControl = 589832,

    /// <summary>
    /// authenticationOptions
    /// </summary>
    AuthenticationOptions = 589835,

    /// <summary>
    /// badPwdCount
    /// </summary>
    BadPwdCount = 589836,

    /// <summary>
    /// builtinCreationTime
    /// </summary>
    BuiltinCreationTime = 589837,

    /// <summary>
    /// builtinModifiedCount
    /// </summary>
    BuiltinModifiedCount = 589838,

    /// <summary>
    /// msiScriptPath
    /// </summary>
    MsiScriptPath = 589839,

    /// <summary>
    /// codePage
    /// </summary>
    CodePage = 589840,

    /// <summary>
    /// cOMClassID
    /// </summary>
    COMClassId = 589843,

    /// <summary>
    /// cOMInterfaceID
    /// </summary>
    COMInterfaceId = 589844,

    /// <summary>
    /// cOMProgID
    /// </summary>
    COMProgId = 589845,

    /// <summary>
    /// contentIndexingAllowed
    /// </summary>
    ContentIndexingAllowed = 589848,

    /// <summary>
    /// countryCode
    /// </summary>
    CountryCode = 589849,

    /// <summary>
    /// creationTime
    /// </summary>
    CreationTime = 589850,

    /// <summary>
    /// currentValue
    /// </summary>
    CurrentValue = 589851,

    /// <summary>
    /// dnsRoot
    /// </summary>
    DnsRoot = 589852,

    /// <summary>
    /// fRSReplicaSetType
    /// </summary>
    FRSReplicaSetType = 589855,

    /// <summary>
    /// domainPolicyObject
    /// </summary>
    DomainPolicyObject = 589856,

    /// <summary>
    /// employeeID
    /// </summary>
    EmployeeId = 589859,

    /// <summary>
    /// enabledConnection
    /// </summary>
    EnabledConnection = 589860,

    /// <summary>
    /// flags
    /// </summary>
    Flags = 589862,

    /// <summary>
    /// forceLogoff
    /// </summary>
    ForceLogoff = 589863,

    /// <summary>
    /// fromServer
    /// </summary>
    FromServer = 589864,

    /// <summary>
    /// generatedConnection
    /// </summary>
    GeneratedConnection = 589865,

    /// <summary>
    /// fRSVersionGUID
    /// </summary>
    FRSVersionGuid = 589867,

    /// <summary>
    /// homeDirectory
    /// </summary>
    HomeDirectory = 589868,

    /// <summary>
    /// homeDrive
    /// </summary>
    HomeDrive = 589869,

    /// <summary>
    /// keywords
    /// </summary>
    Keywords = 589872,

    /// <summary>
    /// badPasswordTime
    /// </summary>
    BadPasswordTime = 589873,

    /// <summary>
    /// lastContentIndexed
    /// </summary>
    LastContentIndexed = 589874,

    /// <summary>
    /// lastLogoff
    /// </summary>
    LastLogoff = 589875,

    /// <summary>
    /// lastLogon
    /// </summary>
    LastLogon = 589876,

    /// <summary>
    /// lastSetTime
    /// </summary>
    LastSetTime = 589877,

    /// <summary>
    /// dBCSPwd
    /// </summary>
    LMHash = 589879,

    /// <summary>
    /// localPolicyFlags
    /// </summary>
    LocalPolicyFlags = 589880,

    /// <summary>
    /// defaultLocalPolicyObject
    /// </summary>
    DefaultLocalPolicyObject = 589881,

    /// <summary>
    /// localeID
    /// </summary>
    LocaleId = 589882,

    /// <summary>
    /// lockoutDuration
    /// </summary>
    LockoutDuration = 589884,

    /// <summary>
    /// lockOutObservationWindow
    /// </summary>
    LockOutObservationWindow = 589885,

    /// <summary>
    /// scriptPath
    /// </summary>
    ScriptPath = 589886,

    /// <summary>
    /// logonHours
    /// </summary>
    LogonHours = 589888,

    /// <summary>
    /// logonWorkstation
    /// </summary>
    LogonWorkstation = 589889,

    /// <summary>
    /// lSACreationTime
    /// </summary>
    LSACreationTime = 589890,

    /// <summary>
    /// lSAModifiedCount
    /// </summary>
    LSAModifiedCount = 589891,

    /// <summary>
    /// machineArchitecture
    /// </summary>
    MachineArchitecture = 589892,

    /// <summary>
    /// machineRole
    /// </summary>
    MachineRole = 589895,

    /// <summary>
    /// marshalledInterface
    /// </summary>
    MarshalledInterface = 589896,

    /// <summary>
    /// lockoutThreshold
    /// </summary>
    LockoutThreshold = 589897,

    /// <summary>
    /// maxPwdAge
    /// </summary>
    MaximumPasswordAge = 589898,

    /// <summary>
    /// maxRenewAge
    /// </summary>
    MaximumRenewAge = 589899,

    /// <summary>
    /// maxStorage
    /// </summary>
    MaximumStorage = 589900,

    /// <summary>
    /// maxTicketAge
    /// </summary>
    MaximumTicketAge = 589901,

    /// <summary>
    /// minPwdAge
    /// </summary>
    MinimumPasswordAge = 589902,

    /// <summary>
    /// minPwdLength
    /// </summary>
    MinimumPasswordLength = 589903,

    /// <summary>
    /// minTicketAge
    /// </summary>
    MinimumTicketAge = 589904,

    /// <summary>
    /// modifiedCountAtLastProm
    /// </summary>
    ModifiedCountAtLastProm = 589905,

    /// <summary>
    /// moniker
    /// </summary>
    Moniker = 589906,

    /// <summary>
    /// monikerDisplayName
    /// </summary>
    MonikerDisplayName = 589907,

    /// <summary>
    /// userWorkstations
    /// </summary>
    UserWorkstations = 589910,

    /// <summary>
    /// nETBIOSName
    /// </summary>
    NetBIOSName = 589911,

    /// <summary>
    /// nextRid
    /// </summary>
    NextRid = 589912,

    /// <summary>
    /// nTGroupMembers
    /// </summary>
    NTGroupMembers = 589913,

    /// <summary>
    /// unicodePwd
    /// </summary>
    NTHash = 589914,

    /// <summary>
    /// otherLoginWorkstations
    /// </summary>
    OtherLoginWorkstations = 589915,

    /// <summary>
    /// pwdProperties
    /// </summary>
    PasswordProperties = 589917,

    /// <summary>
    /// ntPwdHistory
    /// </summary>
    NTHashHistory = 589918,

    /// <summary>
    /// pwdHistoryLength
    /// </summary>
    PasswordHistoryLength = 589919,

    /// <summary>
    /// pwdLastSet
    /// </summary>
    PasswordLastSet = 589920,

    /// <summary>
    /// preferredOU
    /// </summary>
    PreferredOU = 589921,

    /// <summary>
    /// primaryGroupID
    /// </summary>
    PrimaryGroupId = 589922,

    /// <summary>
    /// priorSetTime
    /// </summary>
    PriorSetTime = 589923,

    /// <summary>
    /// priorValue
    /// </summary>
    PriorValue = 589924,

    /// <summary>
    /// privateKey
    /// </summary>
    PrivateKey = 589925,

    /// <summary>
    /// proxyLifetime
    /// </summary>
    ProxyLifetime = 589927,

    /// <summary>
    /// remoteServerName
    /// </summary>
    RemoteServerName = 589929,

    /// <summary>
    /// remoteSource
    /// </summary>
    RemoteSource = 589931,

    /// <summary>
    /// remoteSourceType
    /// </summary>
    RemoteSourceType = 589932,

    /// <summary>
    /// replicaSource
    /// </summary>
    ReplicaSource = 589933,

    /// <summary>
    /// rpcNsBindings
    /// </summary>
    RpcNsBindings = 589937,

    /// <summary>
    /// rpcNsGroup
    /// </summary>
    RpcNsGroup = 589938,

    /// <summary>
    /// rpcNsInterfaceID
    /// </summary>
    RpcNsInterfaceId = 589939,

    /// <summary>
    /// rpcNsPriority
    /// </summary>
    RpcNsPriority = 589941,

    /// <summary>
    /// rpcNsProfileEntry
    /// </summary>
    RpcNsProfileEntry = 589942,

    /// <summary>
    /// schemaFlagsEx
    /// </summary>
    SchemaFlagsEx = 589944,

    /// <summary>
    /// securityIdentifier
    /// </summary>
    SecurityIdentifier = 589945,

    /// <summary>
    /// serviceClassID
    /// </summary>
    ServiceClassId = 589946,

    /// <summary>
    /// serviceClassInfo
    /// </summary>
    ServiceClassInfo = 589947,

    /// <summary>
    /// supplementalCredentials
    /// </summary>
    SupplementalCredentials = 589949,

    /// <summary>
    /// trustAuthIncoming
    /// </summary>
    TrustAuthIncoming = 589953,

    /// <summary>
    /// trustDirection
    /// </summary>
    TrustDirection = 589956,

    /// <summary>
    /// trustPartner
    /// </summary>
    TrustPartner = 589957,

    /// <summary>
    /// trustPosixOffset
    /// </summary>
    TrustPosixOffset = 589958,

    /// <summary>
    /// trustAuthOutgoing
    /// </summary>
    TrustAuthOutgoing = 589959,

    /// <summary>
    /// trustType
    /// </summary>
    TrustType = 589960,

    /// <summary>
    /// uNCName
    /// </summary>
    UNCName = 589961,

    /// <summary>
    /// userParameters
    /// </summary>
    UserParameters = 589962,

    /// <summary>
    /// profilePath
    /// </summary>
    ProfilePath = 589963,

    /// <summary>
    /// versionNumber
    /// </summary>
    VersionNumber = 589965,

    /// <summary>
    /// winsockAddresses
    /// </summary>
    WinsockAddresses = 589966,

    /// <summary>
    /// operatorCount
    /// </summary>
    OperatorCount = 589968,

    /// <summary>
    /// revision
    /// </summary>
    Revision = 589969,

    /// <summary>
    /// objectSid
    /// </summary>
    ObjectSid = 589970,

    /// <summary>
    /// schemaIDGUID
    /// </summary>
    SchemaIdGuid = 589972,

    /// <summary>
    /// attributeSecurityGUID
    /// </summary>
    AttributeSecurityGuid = 589973,

    /// <summary>
    /// adminCount
    /// </summary>
    AdminCount = 589974,

    /// <summary>
    /// oEMInformation
    /// </summary>
    OEMInformation = 589975,

    /// <summary>
    /// groupAttributes
    /// </summary>
    GroupAttributes = 589976,

    /// <summary>
    /// rid
    /// </summary>
    Rid = 589977,

    /// <summary>
    /// serverState
    /// </summary>
    ServerState = 589978,

    /// <summary>
    /// uASCompat
    /// </summary>
    UASCompat = 589979,

    /// <summary>
    /// comment
    /// </summary>
    UserComment = 589980,

    /// <summary>
    /// serverRole
    /// </summary>
    ServerRole = 589981,

    /// <summary>
    /// domainReplica
    /// </summary>
    DomainReplica = 589982,

    /// <summary>
    /// accountExpires
    /// </summary>
    AccountExpires = 589983,

    /// <summary>
    /// lmPwdHistory
    /// </summary>
    LMHashHistory = 589984,

    /// <summary>
    /// groupMembershipSAM
    /// </summary>
    GroupMembershipSAM = 589990,

    /// <summary>
    /// modifiedCount
    /// </summary>
    ModifiedCount = 589992,

    /// <summary>
    /// logonCount
    /// </summary>
    LogonCount = 589993,

    /// <summary>
    /// systemOnly
    /// </summary>
    SystemOnly = 589994,

    /// <summary>
    /// systemPossSuperiors
    /// </summary>
    SystemPossSuperiors = 590019,

    /// <summary>
    /// systemMayContain
    /// </summary>
    SystemMayContain = 590020,

    /// <summary>
    /// systemMustContain
    /// </summary>
    SystemMustContain = 590021,

    /// <summary>
    /// systemAuxiliaryClass
    /// </summary>
    SystemAuxiliaryClass = 590022,

    /// <summary>
    /// serviceInstanceVersion
    /// </summary>
    ServiceInstanceVersion = 590023,

    /// <summary>
    /// controlAccessRights
    /// </summary>
    ControlAccessRights = 590024,

    /// <summary>
    /// auditingPolicy
    /// </summary>
    AuditingPolicy = 590026,

    /// <summary>
    /// pKTGuid
    /// </summary>
    PKTGuid = 590029,

    /// <summary>
    /// pKT
    /// </summary>
    PKT = 590030,

    /// <summary>
    /// schedule
    /// </summary>
    Schedule = 590035,

    /// <summary>
    /// defaultClassStore
    /// </summary>
    DefaultClassStore = 590037,

    /// <summary>
    /// nextLevelStore
    /// </summary>
    NextLevelStore = 590038,

    /// <summary>
    /// applicationName
    /// </summary>
    ApplicationName = 590042,

    /// <summary>
    /// iconPath
    /// </summary>
    IconPath = 590043,

    /// <summary>
    /// sAMAccountName
    /// </summary>
    SamAccountName = 590045,

    /// <summary>
    /// location
    /// </summary>
    Location = 590046,

    /// <summary>
    /// serverName
    /// </summary>
    ServerName = 590047,

    /// <summary>
    /// defaultSecurityDescriptor
    /// </summary>
    DefaultSecurityDescriptor = 590048,

    /// <summary>
    /// portName
    /// </summary>
    PortName = 590052,

    /// <summary>
    /// driverName
    /// </summary>
    DriverName = 590053,

    /// <summary>
    /// printSeparatorFile
    /// </summary>
    PrintSeparatorFile = 590054,

    /// <summary>
    /// priority
    /// </summary>
    Priority = 590055,

    /// <summary>
    /// defaultPriority
    /// </summary>
    DefaultPriority = 590056,

    /// <summary>
    /// printStartTime
    /// </summary>
    PrintStartTime = 590057,

    /// <summary>
    /// printEndTime
    /// </summary>
    PrintEndTime = 590058,

    /// <summary>
    /// printFormName
    /// </summary>
    PrintFormName = 590059,

    /// <summary>
    /// printBinNames
    /// </summary>
    PrintBinNames = 590061,

    /// <summary>
    /// printMaxResolutionSupported
    /// </summary>
    PrintMaxResolutionSupported = 590062,

    /// <summary>
    /// printOrientationsSupported
    /// </summary>
    PrintOrientationsSupported = 590064,

    /// <summary>
    /// printMaxCopies
    /// </summary>
    PrintMaxCopies = 590065,

    /// <summary>
    /// printCollate
    /// </summary>
    PrintCollate = 590066,

    /// <summary>
    /// printColor
    /// </summary>
    PrintColor = 590067,

    /// <summary>
    /// printLanguage
    /// </summary>
    PrintLanguage = 590070,

    /// <summary>
    /// printAttributes
    /// </summary>
    PrintAttributes = 590071,

    /// <summary>
    /// cOMCLSID
    /// </summary>
    COMCLSID = 590073,

    /// <summary>
    /// cOMUniqueLIBID
    /// </summary>
    COMUniqueLIBID = 590074,

    /// <summary>
    /// cOMTreatAsClassId
    /// </summary>
    COMTreatAsClassId = 590075,

    /// <summary>
    /// cOMOtherProgId
    /// </summary>
    COMOtherProgId = 590077,

    /// <summary>
    /// cOMTypelibId
    /// </summary>
    COMTypelibId = 590078,

    /// <summary>
    /// vendor
    /// </summary>
    Vendor = 590079,

    /// <summary>
    /// division
    /// </summary>
    Division = 590085,

    /// <summary>
    /// notes
    /// </summary>
    AdditionalInformation = 590089,

    /// <summary>
    /// eFSPolicy
    /// </summary>
    EFSPolicy = 590092,

    /// <summary>
    /// linkTrackSecret
    /// </summary>
    LinkTrackSecret = 590093,

    /// <summary>
    /// printShareName
    /// </summary>
    PrintShareName = 590094,

    /// <summary>
    /// printOwner
    /// </summary>
    PrintOwner = 590095,

    /// <summary>
    /// printNotify
    /// </summary>
    PrintNotify = 590096,

    /// <summary>
    /// printStatus
    /// </summary>
    PrintStatus = 590097,

    /// <summary>
    /// printSpooling
    /// </summary>
    PrintSpooling = 590098,

    /// <summary>
    /// printKeepPrintedJobs
    /// </summary>
    PrintKeepPrintedJobs = 590099,

    /// <summary>
    /// driverVersion
    /// </summary>
    DriverVersion = 590100,

    /// <summary>
    /// printMaxXExtent
    /// </summary>
    PrintMaxXExtent = 590101,

    /// <summary>
    /// printMaxYExtent
    /// </summary>
    PrintMaxYExtent = 590102,

    /// <summary>
    /// printMinXExtent
    /// </summary>
    PrintMinXExtent = 590103,

    /// <summary>
    /// printMinYExtent
    /// </summary>
    PrintMinYExtent = 590104,

    /// <summary>
    /// printStaplingSupported
    /// </summary>
    PrintStaplingSupported = 590105,

    /// <summary>
    /// printMemory
    /// </summary>
    PrintMemory = 590106,

    /// <summary>
    /// assetNumber
    /// </summary>
    AssetNumber = 590107,

    /// <summary>
    /// bytesPerMinute
    /// </summary>
    BytesPerMinute = 590108,

    /// <summary>
    /// printRate
    /// </summary>
    PrintRate = 590109,

    /// <summary>
    /// printRateUnit
    /// </summary>
    PrintRateUnit = 590110,

    /// <summary>
    /// printNetworkAddress
    /// </summary>
    PrintNetworkAddress = 590111,

    /// <summary>
    /// printMACAddress
    /// </summary>
    PrintMACAddress = 590112,

    /// <summary>
    /// printMediaReady
    /// </summary>
    PrintMediaReady = 590113,

    /// <summary>
    /// printNumberUp
    /// </summary>
    PrintNumberUp = 590114,

    /// <summary>
    /// printMediaSupported
    /// </summary>
    PrintMediaSupported = 590123,

    /// <summary>
    /// printerName
    /// </summary>
    PrinterName = 590124,

    /// <summary>
    /// wbemPath
    /// </summary>
    WbemPath = 590125,

    /// <summary>
    /// sAMAccountType
    /// </summary>
    SamAccountType = 590126,

    /// <summary>
    /// notificationList
    /// </summary>
    NotificationList = 590127,

    /// <summary>
    /// options
    /// </summary>
    Options = 590131,

    /// <summary>
    /// rpcNsObjectID
    /// </summary>
    RpcNsObjectId = 590136,

    /// <summary>
    /// rpcNsTransferSyntax
    /// </summary>
    RpcNsTransferSyntax = 590138,

    /// <summary>
    /// implementedCategories
    /// </summary>
    ImplementedCategories = 590144,

    /// <summary>
    /// requiredCategories
    /// </summary>
    RequiredCategories = 590145,

    /// <summary>
    /// categoryId
    /// </summary>
    CategoryId = 590146,

    /// <summary>
    /// packageType
    /// </summary>
    PackageType = 590148,

    /// <summary>
    /// setupCommand
    /// </summary>
    SetupCommand = 590149,

    /// <summary>
    /// packageName
    /// </summary>
    PackageName = 590150,

    /// <summary>
    /// packageFlags
    /// </summary>
    PackageFlags = 590151,

    /// <summary>
    /// versionNumberHi
    /// </summary>
    VersionNumberHigh = 590152,

    /// <summary>
    /// versionNumberLo
    /// </summary>
    VersionNumberLow = 590153,

    /// <summary>
    /// lastUpdateSequence
    /// </summary>
    LastUpdateSequence = 590154,

    /// <summary>
    /// birthLocation
    /// </summary>
    BirthLocation = 590156,

    /// <summary>
    /// oMTIndxGuid
    /// </summary>
    OMTIndxGuid = 590157,

    /// <summary>
    /// volTableIdxGUID
    /// </summary>
    VolumeTableIndexGuid = 590158,

    /// <summary>
    /// currentLocation
    /// </summary>
    CurrentLocation = 590159,

    /// <summary>
    /// volTableGUID
    /// </summary>
    VolumeTableGuid = 590160,

    /// <summary>
    /// currMachineId
    /// </summary>
    CurrentMachineId = 590161,

    /// <summary>
    /// rightsGuid
    /// </summary>
    RightsGuid = 590164,

    /// <summary>
    /// appliesTo
    /// </summary>
    AppliesTo = 590165,

    /// <summary>
    /// groupsToIgnore
    /// </summary>
    GroupstoIgnore = 590168,

    /// <summary>
    /// groupPriority
    /// </summary>
    GroupPriority = 590169,

    /// <summary>
    /// desktopProfile
    /// </summary>
    DesktopProfile = 590170,

    /// <summary>
    /// foreignIdentifier
    /// </summary>
    ForeignIdentifier = 590180,

    /// <summary>
    /// nTMixedDomain
    /// </summary>
    NTMixedDomain = 590181,

    /// <summary>
    /// netbootInitialization
    /// </summary>
    NetbootInitialization = 590182,

    /// <summary>
    /// netbootGUID
    /// </summary>
    NetbootGuid = 590183,

    /// <summary>
    /// netbootMachineFilePath
    /// </summary>
    NetbootMachineFilePath = 590185,

    /// <summary>
    /// siteGUID
    /// </summary>
    SiteGuid = 590186,

    /// <summary>
    /// operatingSystem
    /// </summary>
    OperatingSystemName = 590187,

    /// <summary>
    /// operatingSystemVersion
    /// </summary>
    OperatingSystemVersion = 590188,

    /// <summary>
    /// operatingSystemServicePack
    /// </summary>
    OperatingSystemServicePack = 590189,

    /// <summary>
    /// rpcNsAnnotation
    /// </summary>
    RpcNsAnnotation = 590190,

    /// <summary>
    /// rpcNsCodeset
    /// </summary>
    RpcNsCodeset = 590191,

    /// <summary>
    /// rIDManagerReference
    /// </summary>
    RidManagerReference = 590192,

    /// <summary>
    /// fSMORoleOwner
    /// </summary>
    FSMORoleOwner = 590193,

    /// <summary>
    /// rIDAvailablePool
    /// </summary>
    RidAvailablePool = 590194,

    /// <summary>
    /// rIDAllocationPool
    /// </summary>
    RidAllocationPool = 590195,

    /// <summary>
    /// rIDPreviousAllocationPool
    /// </summary>
    RidPreviousAllocationPool = 590196,

    /// <summary>
    /// rIDUsedPool
    /// </summary>
    RidUsedPool = 590197,

    /// <summary>
    /// rIDNextRID
    /// </summary>
    RidNextRid = 590198,

    /// <summary>
    /// systemFlags
    /// </summary>
    SystemFlags = 590199,

    /// <summary>
    /// dnsAllowDynamic
    /// </summary>
    DnsAllowDynamic = 590202,

    /// <summary>
    /// dnsAllowXFR
    /// </summary>
    DnsAllowXFR = 590203,

    /// <summary>
    /// dnsSecureSecondaries
    /// </summary>
    DnsSecureSecondaries = 590204,

    /// <summary>
    /// dnsNotifySecondaries
    /// </summary>
    DnsNotifySecondaries = 590205,

    /// <summary>
    /// dnsRecord
    /// </summary>
    DnsRecord = 590206,

    /// <summary>
    /// operatingSystemHotfix
    /// </summary>
    OperatingSystemHotfix = 590239,

    /// <summary>
    /// publicKeyPolicy
    /// </summary>
    PublicKeyPolicy = 590244,

    /// <summary>
    /// domainWidePolicy
    /// </summary>
    DomainWidePolicy = 590245,

    /// <summary>
    /// domainPolicyReference
    /// </summary>
    DomainPolicyReference = 590246,

    /// <summary>
    /// localPolicyReference
    /// </summary>
    LocalPolicyReference = 590281,

    /// <summary>
    /// qualityOfService
    /// </summary>
    QualityOfService = 590282,

    /// <summary>
    /// machineWidePolicy
    /// </summary>
    MachineWidePolicy = 590283,

    /// <summary>
    /// trustAttributes
    /// </summary>
    TrustAttributes = 590294,

    /// <summary>
    /// trustParent
    /// </summary>
    TrustParent = 590295,

    /// <summary>
    /// domainCrossRef
    /// </summary>
    DomainCrossRef = 590296,

    /// <summary>
    /// defaultGroup
    /// </summary>
    DefaultGroup = 590304,

    /// <summary>
    /// schemaUpdate
    /// </summary>
    SchemaUpdate = 590305,

    /// <summary>
    /// fRSFileFilter
    /// </summary>
    FRSFileFilter = 590307,

    /// <summary>
    /// fRSDirectoryFilter
    /// </summary>
    FRSDirectoryFilter = 590308,

    /// <summary>
    /// fRSUpdateTimeout
    /// </summary>
    FRSUpdateTimeout = 590309,

    /// <summary>
    /// fRSWorkingPath
    /// </summary>
    FRSWorkingPath = 590310,

    /// <summary>
    /// fRSRootPath
    /// </summary>
    FRSRootPath = 590311,

    /// <summary>
    /// fRSStagingPath
    /// </summary>
    FRSStagingPath = 590312,

    /// <summary>
    /// fRSDSPoll
    /// </summary>
    FRSDSPoll = 590314,

    /// <summary>
    /// fRSFaultCondition
    /// </summary>
    FRSFaultCondition = 590315,

    /// <summary>
    /// siteServer
    /// </summary>
    SiteServer = 590318,

    /// <summary>
    /// creationWizard
    /// </summary>
    CreationWizard = 590322,

    /// <summary>
    /// contextMenu
    /// </summary>
    ContextMenu = 590323,

    /// <summary>
    /// fRSServiceCommand
    /// </summary>
    FRSServiceCommand = 590324,

    /// <summary>
    /// timeVolChange
    /// </summary>
    TimeVolumeChange = 590326,

    /// <summary>
    /// timeRefresh
    /// </summary>
    TimeRefresh = 590327,

    /// <summary>
    /// seqNotification
    /// </summary>
    SeqNotification = 590328,

    /// <summary>
    /// oMTGuid
    /// </summary>
    OMTGuid = 590329,

    /// <summary>
    /// objectCount
    /// </summary>
    ObjectCount = 590330,

    /// <summary>
    /// volumeCount
    /// </summary>
    VolumeCount = 590331,

    /// <summary>
    /// serviceClassName
    /// </summary>
    ServiceClassName = 590333,

    /// <summary>
    /// serviceBindingInformation
    /// </summary>
    ServiceBindingInformation = 590334,

    /// <summary>
    /// flatName
    /// </summary>
    FlatName = 590335,

    /// <summary>
    /// physicalLocationObject
    /// </summary>
    PhysicalLocationObject = 590338,

    /// <summary>
    /// ipsecPolicyReference
    /// </summary>
    IpsecPolicyReference = 590341,

    /// <summary>
    /// defaultHidingValue
    /// </summary>
    DefaultHidingValue = 590342,

    /// <summary>
    /// lastBackupRestorationTime
    /// </summary>
    LastBackupRestorationTime = 590343,

    /// <summary>
    /// machinePasswordChangeInterval
    /// </summary>
    MachinePasswordChangeInterval = 590344,

    /// <summary>
    /// superiorDNSRoot
    /// </summary>
    SuperiorDNSRoot = 590356,

    /// <summary>
    /// fRSReplicaSetGUID
    /// </summary>
    FRSReplicaSetGuid = 590357,

    /// <summary>
    /// fRSLevelLimit
    /// </summary>
    FRSLevelLimit = 590358,

    /// <summary>
    /// fRSRootSecurity
    /// </summary>
    FRSRootSecurity = 590359,

    /// <summary>
    /// fRSExtensions
    /// </summary>
    FRSExtensions = 590360,

    /// <summary>
    /// dynamicLDAPServer
    /// </summary>
    DynamicLDAPServer = 590361,

    /// <summary>
    /// prefixMap
    /// </summary>
    PrefixMap = 590362,

    /// <summary>
    /// initialAuthIncoming
    /// </summary>
    InitialAuthIncoming = 590363,

    /// <summary>
    /// initialAuthOutgoing
    /// </summary>
    InitialAuthOutgoing = 590364,

    /// <summary>
    /// parentCA
    /// </summary>
    ParentCA = 590381,

    /// <summary>
    /// adminPropertyPages
    /// </summary>
    AdminPropertyPages = 590386,

    /// <summary>
    /// shellPropertyPages
    /// </summary>
    ShellPropertyPages = 590387,

    /// <summary>
    /// meetingID
    /// </summary>
    MeetingId = 590389,

    /// <summary>
    /// meetingName
    /// </summary>
    MeetingName = 590390,

    /// <summary>
    /// meetingDescription
    /// </summary>
    MeetingDescription = 590391,

    /// <summary>
    /// meetingKeyword
    /// </summary>
    MeetingKeyword = 590392,

    /// <summary>
    /// meetingLocation
    /// </summary>
    MeetingLocation = 590393,

    /// <summary>
    /// meetingProtocol
    /// </summary>
    MeetingProtocol = 590394,

    /// <summary>
    /// meetingType
    /// </summary>
    MeetingType = 590395,

    /// <summary>
    /// meetingApplication
    /// </summary>
    MeetingApplication = 590397,

    /// <summary>
    /// meetingLanguage
    /// </summary>
    MeetingLanguage = 590398,

    /// <summary>
    /// meetingMaxParticipants
    /// </summary>
    MeetingMaxParticipants = 590400,

    /// <summary>
    /// meetingOriginator
    /// </summary>
    MeetingOriginator = 590401,

    /// <summary>
    /// meetingContactInfo
    /// </summary>
    MeetingContactInfo = 590402,

    /// <summary>
    /// meetingOwner
    /// </summary>
    MeetingOwner = 590403,

    /// <summary>
    /// meetingIP
    /// </summary>
    MeetingIP = 590404,

    /// <summary>
    /// meetingScope
    /// </summary>
    MeetingScope = 590405,

    /// <summary>
    /// meetingAdvertiseScope
    /// </summary>
    MeetingAdvertiseScope = 590406,

    /// <summary>
    /// meetingURL
    /// </summary>
    MeetingURL = 590407,

    /// <summary>
    /// meetingRating
    /// </summary>
    MeetingRating = 590408,

    /// <summary>
    /// meetingIsEncrypted
    /// </summary>
    MeetingIsEncrypted = 590409,

    /// <summary>
    /// meetingRecurrence
    /// </summary>
    MeetingRecurrence = 590410,

    /// <summary>
    /// meetingStartTime
    /// </summary>
    MeetingStartTime = 590411,

    /// <summary>
    /// meetingEndTime
    /// </summary>
    MeetingEndTime = 590412,

    /// <summary>
    /// meetingBandwidth
    /// </summary>
    MeetingBandwidth = 590413,

    /// <summary>
    /// meetingBlob
    /// </summary>
    MeetingBlob = 590414,

    /// <summary>
    /// sIDHistory
    /// </summary>
    SidHistory = 590433,

    /// <summary>
    /// classDisplayName
    /// </summary>
    ClassDisplayName = 590434,

    /// <summary>
    /// adminContextMenu
    /// </summary>
    AdminContextMenu = 590438,

    /// <summary>
    /// shellContextMenu
    /// </summary>
    ShellContextMenu = 590439,

    /// <summary>
    /// wellKnownObjects
    /// </summary>
    WellKnownObjects = 590442,

    /// <summary>
    /// dNSHostName
    /// </summary>
    DnsHostName = 590443,

    /// <summary>
    /// ipsecName
    /// </summary>
    IpsecName = 590444,

    /// <summary>
    /// ipsecID
    /// </summary>
    IpsecId = 590445,

    /// <summary>
    /// ipsecDataType
    /// </summary>
    IpsecDataType = 590446,

    /// <summary>
    /// ipsecData
    /// </summary>
    IpsecData = 590447,

    /// <summary>
    /// ipsecOwnersReference
    /// </summary>
    IpsecOwnersReference = 590448,

    /// <summary>
    /// ipsecISAKMPReference
    /// </summary>
    IpsecISAKMPReference = 590450,

    /// <summary>
    /// ipsecNFAReference
    /// </summary>
    IpsecNFAReference = 590451,

    /// <summary>
    /// ipsecNegotiationPolicyReference
    /// </summary>
    IpsecNegotiationPolicyReference = 590452,

    /// <summary>
    /// ipsecFilterReference
    /// </summary>
    IpsecFilterReference = 590453,

    /// <summary>
    /// printPagesPerMinute
    /// </summary>
    PrintPagesPerMinute = 590455,

    /// <summary>
    /// policyReplicationFlags
    /// </summary>
    PolicyReplicationFlags = 590457,

    /// <summary>
    /// privilegeDisplayName
    /// </summary>
    PrivilegeDisplayName = 590458,

    /// <summary>
    /// privilegeValue
    /// </summary>
    PrivilegeValue = 590459,

    /// <summary>
    /// privilegeAttributes
    /// </summary>
    PrivilegeAttributes = 590460,

    /// <summary>
    /// isMemberOfPartialAttributeSet
    /// </summary>
    IsInGlobalCatalog = 590463,

    /// <summary>
    /// partialAttributeSet
    /// </summary>
    PartialAttributeSet = 590464,

    /// <summary>
    /// showInAddressBook
    /// </summary>
    ShowInAddressBook = 590468,

    /// <summary>
    /// userCert
    /// </summary>
    UserCert = 590469,

    /// <summary>
    /// otherFacsimileTelephoneNumber
    /// </summary>
    PhoneFaxOther = 590470,

    /// <summary>
    /// otherMobile
    /// </summary>
    PhoneMobileOther = 590471,

    /// <summary>
    /// primaryTelexNumber
    /// </summary>
    TelexPrimary = 590472,

    /// <summary>
    /// primaryInternationalISDNNumber
    /// </summary>
    PhoneISDNPrimary = 590473,

    /// <summary>
    /// mhsORAddress
    /// </summary>
    MHSORAddress = 590474,

    /// <summary>
    /// otherMailbox
    /// </summary>
    OtherMailbox = 590475,

    /// <summary>
    /// assistant
    /// </summary>
    Assistant = 590476,

    /// <summary>
    /// legacyExchangeDN
    /// </summary>
    LegacyExchangeDN = 590479,

    /// <summary>
    /// userPrincipalName
    /// </summary>
    UserPrincipalName = 590480,

    /// <summary>
    /// serviceDNSName
    /// </summary>
    ServiceDnsName = 590481,

    /// <summary>
    /// serviceDNSNameType
    /// </summary>
    ServiceDnsNameType = 590483,

    /// <summary>
    /// treeName
    /// </summary>
    TreeName = 590484,

    /// <summary>
    /// isDefunct
    /// </summary>
    IsDefunct = 590485,

    /// <summary>
    /// lockoutTime
    /// </summary>
    LockoutTime = 590486,

    /// <summary>
    /// partialAttributeDeletionList
    /// </summary>
    PartialAttributeDeletionList = 590487,

    /// <summary>
    /// syncWithObject
    /// </summary>
    SyncWithObject = 590488,

    /// <summary>
    /// syncAttributes
    /// </summary>
    SyncAttributes = 590490,

    /// <summary>
    /// syncWithSID
    /// </summary>
    SyncWithSid = 590491,

    /// <summary>
    /// domainCAs
    /// </summary>
    DomainCertificateAuthorities = 590492,

    /// <summary>
    /// rIDSetReferences
    /// </summary>
    RIDSetReferences = 590493,

    /// <summary>
    /// msiFileList
    /// </summary>
    MsiFileList = 590495,

    /// <summary>
    /// categories
    /// </summary>
    Categories = 590496,

    /// <summary>
    /// retiredReplDSASignatures
    /// </summary>
    RetiredReplDSASignatures = 590497,

    /// <summary>
    /// rootTrust
    /// </summary>
    RootTrust = 590498,

    /// <summary>
    /// catalogs
    /// </summary>
    Catalogs = 590499,

    /// <summary>
    /// replTopologyStayOfExecution
    /// </summary>
    ReplTopologyStayOfExecution = 590501,

    /// <summary>
    /// creator
    /// </summary>
    Creator = 590503,

    /// <summary>
    /// queryPoint
    /// </summary>
    QueryPoint = 590504,

    /// <summary>
    /// indexedScopes
    /// </summary>
    IndexedScopes = 590505,

    /// <summary>
    /// friendlyNames
    /// </summary>
    FriendlyNames = 590506,

    /// <summary>
    /// cRLPartitionedRevocationList
    /// </summary>
    CRLPartitionedRevocationList = 590507,

    /// <summary>
    /// certificateAuthorityObject
    /// </summary>
    CertificateAuthorityObject = 590508,

    /// <summary>
    /// parentCACertificateChain
    /// </summary>
    ParentCACertificateChain = 590509,

    /// <summary>
    /// domainID
    /// </summary>
    DomainId = 590510,

    /// <summary>
    /// cAConnect
    /// </summary>
    CAConnect = 590511,

    /// <summary>
    /// cAWEBURL
    /// </summary>
    CAWEBURL = 590512,

    /// <summary>
    /// cRLObject
    /// </summary>
    CRLObject = 590513,

    /// <summary>
    /// cAUsages
    /// </summary>
    CAUsages = 590514,

    /// <summary>
    /// previousCACertificates
    /// </summary>
    PreviousCACertificates = 590516,

    /// <summary>
    /// pendingCACertificates
    /// </summary>
    PendingCACertificates = 590517,

    /// <summary>
    /// previousParentCA
    /// </summary>
    PreviousParentCA = 590518,

    /// <summary>
    /// pendingParentCA
    /// </summary>
    PendingParentCA = 590519,

    /// <summary>
    /// currentParentCA
    /// </summary>
    CurrentParentCA = 590520,

    /// <summary>
    /// cACertificateDN
    /// </summary>
    CACertificateDN = 590521,

    /// <summary>
    /// dhcpUniqueKey
    /// </summary>
    DHCPUniqueKey = 590522,

    /// <summary>
    /// dhcpType
    /// </summary>
    DHCPType = 590523,

    /// <summary>
    /// dhcpFlags
    /// </summary>
    DHCPFlags = 590524,

    /// <summary>
    /// dhcpIdentification
    /// </summary>
    DHCPIdentification = 590525,

    /// <summary>
    /// dhcpObjName
    /// </summary>
    DHCPObjName = 590526,

    /// <summary>
    /// dhcpObjDescription
    /// </summary>
    DHCPObjDescription = 590527,

    /// <summary>
    /// dhcpServers
    /// </summary>
    DHCPServers = 590528,

    /// <summary>
    /// dhcpSubnets
    /// </summary>
    DHCPSubnets = 590529,

    /// <summary>
    /// dhcpMask
    /// </summary>
    DHCPMask = 590530,

    /// <summary>
    /// dhcpRanges
    /// </summary>
    DHCPRanges = 590531,

    /// <summary>
    /// dhcpSites
    /// </summary>
    DHCPSites = 590532,

    /// <summary>
    /// dhcpReservations
    /// </summary>
    DHCPReservations = 590533,

    /// <summary>
    /// superScopes
    /// </summary>
    SuperScopes = 590534,

    /// <summary>
    /// superScopeDescription
    /// </summary>
    SuperScopeDescription = 590535,

    /// <summary>
    /// optionDescription
    /// </summary>
    OptionDescription = 590536,

    /// <summary>
    /// optionsLocation
    /// </summary>
    OptionsLocation = 590537,

    /// <summary>
    /// dhcpOptions
    /// </summary>
    DHCPOptions = 590538,

    /// <summary>
    /// dhcpClasses
    /// </summary>
    DHCPClasses = 590539,

    /// <summary>
    /// mscopeId
    /// </summary>
    MScopeId = 590540,

    /// <summary>
    /// dhcpState
    /// </summary>
    DHCPState = 590541,

    /// <summary>
    /// dhcpProperties
    /// </summary>
    DHCPProperties = 590542,

    /// <summary>
    /// dhcpMaxKey
    /// </summary>
    DHCPMaxKey = 590543,

    /// <summary>
    /// dhcpUpdateTime
    /// </summary>
    DHCPUpdateTime = 590544,

    /// <summary>
    /// ipPhone
    /// </summary>
    PhoneIpPrimary = 590545,

    /// <summary>
    /// otherIpPhone
    /// </summary>
    PhoneIpOther = 590546,

    /// <summary>
    /// attributeDisplayNames
    /// </summary>
    AttributeDisplayNames = 590572,

    /// <summary>
    /// url
    /// </summary>
    WWWPageOther = 590573,

    /// <summary>
    /// groupType
    /// </summary>
    GroupType = 590574,

    /// <summary>
    /// userSharedFolder
    /// </summary>
    UserSharedFolder = 590575,

    /// <summary>
    /// userSharedFolderOther
    /// </summary>
    UserSharedFolderOther = 590576,

    /// <summary>
    /// nameServiceFlags
    /// </summary>
    NameServiceFlags = 590577,

    /// <summary>
    /// rpcNsEntryFlags
    /// </summary>
    RpcNsEntryFlags = 590578,

    /// <summary>
    /// domainIdentifier
    /// </summary>
    DomainIdentifier = 590579,

    /// <summary>
    /// aCSTimeOfDay
    /// </summary>
    ACSTimeOfDay = 590580,

    /// <summary>
    /// aCSDirection
    /// </summary>
    ACSDirection = 590581,

    /// <summary>
    /// aCSMaxTokenRatePerFlow
    /// </summary>
    ACSMaxTokenRatePerFlow = 590582,

    /// <summary>
    /// aCSMaxPeakBandwidthPerFlow
    /// </summary>
    ACSMaxPeakBandwidthPerFlow = 590583,

    /// <summary>
    /// aCSAggregateTokenRatePerUser
    /// </summary>
    ACSAggregateTokenRatePerUser = 590584,

    /// <summary>
    /// aCSMaxDurationPerFlow
    /// </summary>
    ACSMaxDurationPerFlow = 590585,

    /// <summary>
    /// aCSServiceType
    /// </summary>
    ACSServiceType = 590586,

    /// <summary>
    /// aCSTotalNoOfFlows
    /// </summary>
    ACSTotalNoOfFlows = 590587,

    /// <summary>
    /// aCSPriority
    /// </summary>
    ACSPriority = 590588,

    /// <summary>
    /// aCSPermissionBits
    /// </summary>
    ACSPermissionBits = 590589,

    /// <summary>
    /// aCSAllocableRSVPBandwidth
    /// </summary>
    ACSAllocableRSVPBandwidth = 590590,

    /// <summary>
    /// aCSMaxPeakBandwidth
    /// </summary>
    ACSMaxPeakBandwidth = 590591,

    /// <summary>
    /// aCSEnableRSVPMessageLogging
    /// </summary>
    ACSEnableRSVPMessageLogging = 590592,

    /// <summary>
    /// aCSEventLogLevel
    /// </summary>
    ACSEventLogLevel = 590593,

    /// <summary>
    /// aCSEnableACSService
    /// </summary>
    ACSEnableACSService = 590594,

    /// <summary>
    /// servicePrincipalName
    /// </summary>
    ServicePrincipalName = 590595,

    /// <summary>
    /// aCSPolicyName
    /// </summary>
    ACSPolicyName = 590596,

    /// <summary>
    /// aCSRSVPLogFilesLocation
    /// </summary>
    ACSRSVPLogFilesLocation = 590597,

    /// <summary>
    /// aCSMaxNoOfLogFiles
    /// </summary>
    ACSMaxNoOfLogFiles = 590598,

    /// <summary>
    /// aCSMaxSizeOfRSVPLogFile
    /// </summary>
    ACSMaxSizeOfRSVPLogFile = 590599,

    /// <summary>
    /// aCSDSBMPriority
    /// </summary>
    ACSDSBMPriority = 590600,

    /// <summary>
    /// aCSDSBMRefresh
    /// </summary>
    ACSDSBMRefresh = 590601,

    /// <summary>
    /// aCSDSBMDeadTime
    /// </summary>
    ACSDSBMDeadTime = 590602,

    /// <summary>
    /// aCSCacheTimeout
    /// </summary>
    ACSCacheTimeout = 590603,

    /// <summary>
    /// aCSNonReservedTxLimit
    /// </summary>
    ACSNonReservedTxLimit = 590604,

    /// <summary>
    /// lastKnownParent
    /// </summary>
    LastKnownParent = 590605,

    /// <summary>
    /// objectCategory
    /// </summary>
    ObjectCategory = 590606,

    /// <summary>
    /// defaultObjectCategory
    /// </summary>
    DefaultObjectCategory = 590607,

    /// <summary>
    /// aCSIdentityName
    /// </summary>
    ACSIdentityName = 590608,

    /// <summary>
    /// mailAddress
    /// </summary>
    SMTPMailAddress = 590610,

    /// <summary>
    /// transportDLLName
    /// </summary>
    TransportDLLName = 590613,

    /// <summary>
    /// transportType
    /// </summary>
    TransportType = 590615,

    /// <summary>
    /// treatAsLeaf
    /// </summary>
    TreatAsLeaf = 590630,

    /// <summary>
    /// remoteStorageGUID
    /// </summary>
    RemoteStorageGuid = 590633,

    /// <summary>
    /// createDialog
    /// </summary>
    CreateDialog = 590634,

    /// <summary>
    /// createWizardExt
    /// </summary>
    CreateWizardExt = 590636,

    /// <summary>
    /// upgradeProductCode
    /// </summary>
    UpgradeProductCode = 590637,

    /// <summary>
    /// msiScript
    /// </summary>
    MsiScript = 590638,

    /// <summary>
    /// canUpgradeScript
    /// </summary>
    CanUpgradeScript = 590639,

    /// <summary>
    /// fileExtPriority
    /// </summary>
    FileExtPriority = 590640,

    /// <summary>
    /// localizedDescription
    /// </summary>
    LocalizedDescription = 590641,

    /// <summary>
    /// productCode
    /// </summary>
    ProductCode = 590642,

    /// <summary>
    /// certificateTemplates
    /// </summary>
    CertificateTemplates = 590647,

    /// <summary>
    /// signatureAlgorithms
    /// </summary>
    SignatureAlgorithms = 590648,

    /// <summary>
    /// enrollmentProviders
    /// </summary>
    EnrollmentProviders = 590649,

    /// <summary>
    /// lDAPAdminLimits
    /// </summary>
    LDAPAdminLimits = 590667,

    /// <summary>
    /// lDAPIPDenyList
    /// </summary>
    LDAPIPDenyList = 590668,

    /// <summary>
    /// msiScriptName
    /// </summary>
    MsiScriptName = 590669,

    /// <summary>
    /// msiScriptSize
    /// </summary>
    MsiScriptSize = 590670,

    /// <summary>
    /// installUiLevel
    /// </summary>
    InstallUiLevel = 590671,

    /// <summary>
    /// appSchemaVersion
    /// </summary>
    AppSchemaVersion = 590672,

    /// <summary>
    /// netbootAllowNewClients
    /// </summary>
    NetbootAllowNewClients = 590673,

    /// <summary>
    /// netbootLimitClients
    /// </summary>
    NetbootLimitClients = 590674,

    /// <summary>
    /// netbootMaxClients
    /// </summary>
    NetbootMaxClients = 590675,

    /// <summary>
    /// netbootCurrentClientCount
    /// </summary>
    NetbootCurrentClientCount = 590676,

    /// <summary>
    /// netbootAnswerRequests
    /// </summary>
    NetbootAnswerRequests = 590677,

    /// <summary>
    /// netbootAnswerOnlyValidClients
    /// </summary>
    NetbootAnswerOnlyValidClients = 590678,

    /// <summary>
    /// netbootNewMachineNamingPolicy
    /// </summary>
    NetbootNewMachineNamingPolicy = 590679,

    /// <summary>
    /// netbootNewMachineOU
    /// </summary>
    NetbootNewMachineOU = 590680,

    /// <summary>
    /// netbootIntelliMirrorOSes
    /// </summary>
    NetbootIntelliMirrorOSes = 590681,

    /// <summary>
    /// netbootTools
    /// </summary>
    NetbootTools = 590682,

    /// <summary>
    /// netbootLocallyInstalledOSes
    /// </summary>
    NetbootLocallyInstalledOSes = 590683,

    /// <summary>
    /// pekList
    /// </summary>
    PEKList = 590689,

    /// <summary>
    /// pekKeyChangeInterval
    /// </summary>
    PEKChangeInterval = 590690,

    /// <summary>
    /// altSecurityIdentities
    /// </summary>
    AltSecurityIdentities = 590691,

    /// <summary>
    /// isCriticalSystemObject
    /// </summary>
    IsCriticalSystemObject = 590692,

    /// <summary>
    /// fRSControlDataCreation
    /// </summary>
    FRSControlDataCreation = 590695,

    /// <summary>
    /// fRSControlInboundBacklog
    /// </summary>
    FRSControlInboundBacklog = 590696,

    /// <summary>
    /// fRSControlOutboundBacklog
    /// </summary>
    FRSControlOutboundBacklog = 590697,

    /// <summary>
    /// fRSFlags
    /// </summary>
    FRSFlags = 590698,

    /// <summary>
    /// fRSPartnerAuthLevel
    /// </summary>
    FRSPartnerAuthLevel = 590701,

    /// <summary>
    /// fRSServiceCommandStatus
    /// </summary>
    FRSServiceCommandStatus = 590703,

    /// <summary>
    /// fRSTimeLastCommand
    /// </summary>
    FRSTimeLastCommand = 590704,

    /// <summary>
    /// fRSTimeLastConfigChange
    /// </summary>
    FRSTimeLastConfigChange = 590705,

    /// <summary>
    /// fRSVersion
    /// </summary>
    FRSVersion = 590706,

    /// <summary>
    /// msRRASVendorAttributeEntry
    /// </summary>
    MSRRASVendorAttributeEntry = 590707,

    /// <summary>
    /// msRRASAttribute
    /// </summary>
    MSRRASAttribute = 590708,

    /// <summary>
    /// terminalServer
    /// </summary>
    TerminalServer = 590709,

    /// <summary>
    /// purportedSearch
    /// </summary>
    PurportedSearch = 590710,

    /// <summary>
    /// iPSECNegotiationPolicyType
    /// </summary>
    IPSECNegotiationPolicyType = 590711,

    /// <summary>
    /// iPSECNegotiationPolicyAction
    /// </summary>
    IPSECNegotiationPolicyAction = 590712,

    /// <summary>
    /// additionalTrustedServiceNames
    /// </summary>
    AdditionalTrustedServiceNames = 590713,

    /// <summary>
    /// uPNSuffixes
    /// </summary>
    UPNSuffixes = 590714,

    /// <summary>
    /// gPLink
    /// </summary>
    GPLink = 590715,

    /// <summary>
    /// gPOptions
    /// </summary>
    GPOptions = 590716,

    /// <summary>
    /// gPCFunctionalityVersion
    /// </summary>
    GPCFunctionalityVersion = 590717,

    /// <summary>
    /// gPCFileSysPath
    /// </summary>
    GPCFileSysPath = 590718,

    /// <summary>
    /// transportAddressAttribute
    /// </summary>
    TransportAddressAttribute = 590719,

    /// <summary>
    /// uSNSource
    /// </summary>
    USNSource = 590720,

    /// <summary>
    /// aCSMaxAggregatePeakRatePerUser
    /// </summary>
    ACSMaxAggregatePeakRatePerUser = 590721,

    /// <summary>
    /// aCSNonReservedTxSize
    /// </summary>
    ACSNonReservedTxSize = 590722,

    /// <summary>
    /// aCSEnableRSVPAccounting
    /// </summary>
    ACSEnableRSVPAccounting = 590723,

    /// <summary>
    /// aCSRSVPAccountFilesLocation
    /// </summary>
    ACSRSVPAccountFilesLocation = 590724,

    /// <summary>
    /// aCSMaxNoOfAccountFiles
    /// </summary>
    ACSMaxNoOfAccountFiles = 590725,

    /// <summary>
    /// aCSMaxSizeOfRSVPAccountFile
    /// </summary>
    ACSMaxSizeOfRSVPAccountFile = 590726,

    /// <summary>
    /// mSMQQueueType
    /// </summary>
    MSMQQueueType = 590741,

    /// <summary>
    /// mSMQJournal
    /// </summary>
    MSMQJournal = 590742,

    /// <summary>
    /// mSMQQuota
    /// </summary>
    MSMQQuota = 590743,

    /// <summary>
    /// mSMQBasePriority
    /// </summary>
    MSMQBasePriority = 590744,

    /// <summary>
    /// mSMQJournalQuota
    /// </summary>
    MSMQJournalQuota = 590745,

    /// <summary>
    /// mSMQLabel
    /// </summary>
    MSMQLabel = 590746,

    /// <summary>
    /// mSMQAuthenticate
    /// </summary>
    MSMQAuthenticate = 590747,

    /// <summary>
    /// mSMQPrivacyLevel
    /// </summary>
    MSMQPrivacyLevel = 590748,

    /// <summary>
    /// mSMQOwnerID
    /// </summary>
    MSMQOwnerId = 590749,

    /// <summary>
    /// mSMQTransactional
    /// </summary>
    MSMQTransactional = 590750,

    /// <summary>
    /// mSMQSites
    /// </summary>
    MSMQSites = 590751,

    /// <summary>
    /// mSMQOutRoutingServers
    /// </summary>
    MSMQOutRoutingServers = 590752,

    /// <summary>
    /// mSMQInRoutingServers
    /// </summary>
    MSMQInRoutingServers = 590753,

    /// <summary>
    /// mSMQServiceType
    /// </summary>
    MSMQServiceType = 590754,

    /// <summary>
    /// mSMQComputerType
    /// </summary>
    MSMQComputerType = 590757,

    /// <summary>
    /// mSMQForeign
    /// </summary>
    MSMQForeign = 590758,

    /// <summary>
    /// mSMQOSType
    /// </summary>
    MSMQOSType = 590759,

    /// <summary>
    /// mSMQEncryptKey
    /// </summary>
    MSMQEncryptKey = 590760,

    /// <summary>
    /// mSMQSignKey
    /// </summary>
    MSMQSignKey = 590761,

    /// <summary>
    /// mSMQNameStyle
    /// </summary>
    MSMQNameStyle = 590763,

    /// <summary>
    /// mSMQCSPName
    /// </summary>
    MSMQCSPName = 590764,

    /// <summary>
    /// mSMQLongLived
    /// </summary>
    MSMQLongLived = 590765,

    /// <summary>
    /// mSMQVersion
    /// </summary>
    MSMQVersion = 590766,

    /// <summary>
    /// mSMQSite1
    /// </summary>
    MSMQSite1 = 590767,

    /// <summary>
    /// mSMQSite2
    /// </summary>
    MSMQSite2 = 590768,

    /// <summary>
    /// mSMQSiteGates
    /// </summary>
    MSMQSiteGates = 590769,

    /// <summary>
    /// mSMQCost
    /// </summary>
    MSMQCost = 590770,

    /// <summary>
    /// mSMQSignCertificates
    /// </summary>
    MSMQSignCertificates = 590771,

    /// <summary>
    /// mSMQDigests
    /// </summary>
    MSMQDigests = 590772,

    /// <summary>
    /// mSMQServices
    /// </summary>
    MSMQServices = 590774,

    /// <summary>
    /// mSMQQMID
    /// </summary>
    MSMQQMId = 590775,

    /// <summary>
    /// mSMQMigrated
    /// </summary>
    MSMQMigrated = 590776,

    /// <summary>
    /// mSMQSiteID
    /// </summary>
    MSMQSiteId = 590777,

    /// <summary>
    /// mSMQNt4Stub
    /// </summary>
    MSMQNt4Stub = 590784,

    /// <summary>
    /// mSMQSiteForeign
    /// </summary>
    MSMQSiteForeign = 590785,

    /// <summary>
    /// mSMQQueueQuota
    /// </summary>
    MSMQQueueQuota = 590786,

    /// <summary>
    /// mSMQQueueJournalQuota
    /// </summary>
    MSMQQueueJournalQuota = 590787,

    /// <summary>
    /// mSMQNt4Flags
    /// </summary>
    MSMQNt4Flags = 590788,

    /// <summary>
    /// mSMQSiteName
    /// </summary>
    MSMQSiteName = 590789,

    /// <summary>
    /// mSMQDigestsMig
    /// </summary>
    MSMQDigestsMig = 590790,

    /// <summary>
    /// mSMQSignCertificatesMig
    /// </summary>
    MSMQSignCertificatesMig = 590791,

    /// <summary>
    /// msNPAllowDialin
    /// </summary>
    MSNPAllowDialin = 590943,

    /// <summary>
    /// msNPCalledStationID
    /// </summary>
    MSNPCalledStationId = 590947,

    /// <summary>
    /// msNPCallingStationID
    /// </summary>
    MSNPCallingStationId = 590948,

    /// <summary>
    /// msNPSavedCallingStationID
    /// </summary>
    MSNPSavedCallingStationId = 590954,

    /// <summary>
    /// msRADIUSCallbackNumber
    /// </summary>
    MSRADIUSCallbackNumber = 590969,

    /// <summary>
    /// msRADIUSFramedIPAddress
    /// </summary>
    MSRADIUSFramedIPAddress = 590977,

    /// <summary>
    /// msRADIUSFramedRoute
    /// </summary>
    MSRADIUSFramedRoute = 590982,

    /// <summary>
    /// msRADIUSServiceType
    /// </summary>
    MSRADIUSServiceType = 590995,

    /// <summary>
    /// msRASSavedCallbackNumber
    /// </summary>
    MSRASSavedCallbackNumber = 591013,

    /// <summary>
    /// msRASSavedFramedIPAddress
    /// </summary>
    MSRASSavedFramedIPAddress = 591014,

    /// <summary>
    /// msRASSavedFramedRoute
    /// </summary>
    MSRASSavedFramedRoute = 591015,

    /// <summary>
    /// shortServerName
    /// </summary>
    ShortServerName = 591033,

    /// <summary>
    /// isEphemeral
    /// </summary>
    IsEphemeral = 591036,

    /// <summary>
    /// assocNTAccount
    /// </summary>
    AssocNTAccount = 591037,

    /// <summary>
    /// mSMQPrevSiteGates
    /// </summary>
    MSMQPrevSiteGates = 591049,

    /// <summary>
    /// mSMQDependentClientServices
    /// </summary>
    MSMQDependentClientServices = 591050,

    /// <summary>
    /// mSMQRoutingServices
    /// </summary>
    MSMQRoutingServices = 591051,

    /// <summary>
    /// mSMQDsServices
    /// </summary>
    MSMQDsServices = 591052,

    /// <summary>
    /// mSMQRoutingService
    /// </summary>
    MSMQRoutingService = 591061,

    /// <summary>
    /// mSMQDsService
    /// </summary>
    MSMQDsService = 591062,

    /// <summary>
    /// mSMQDependentClientService
    /// </summary>
    MSMQDependentClientService = 591063,

    /// <summary>
    /// netbootSIFFile
    /// </summary>
    NetbootSIFFile = 591064,

    /// <summary>
    /// netbootMirrorDataFile
    /// </summary>
    NetbootMirrorDataFile = 591065,

    /// <summary>
    /// dNReferenceUpdate
    /// </summary>
    DNReferenceUpdate = 591066,

    /// <summary>
    /// mSMQQueueNameExt
    /// </summary>
    MSMQQueueNameExt = 591067,

    /// <summary>
    /// addressBookRoots
    /// </summary>
    AddressBookRoots = 591068,

    /// <summary>
    /// globalAddressList
    /// </summary>
    GlobalAddressList = 591069,

    /// <summary>
    /// interSiteTopologyGenerator
    /// </summary>
    InterSiteTopologyGenerator = 591070,

    /// <summary>
    /// interSiteTopologyRenew
    /// </summary>
    InterSiteTopologyRenew = 591071,

    /// <summary>
    /// interSiteTopologyFailover
    /// </summary>
    InterSiteTopologyFailover = 591072,

    /// <summary>
    /// proxiedObjectName
    /// </summary>
    ProxiedObjectName = 591073,

    /// <summary>
    /// moveTreeState
    /// </summary>
    MoveTreeState = 591129,

    /// <summary>
    /// dNSProperty
    /// </summary>
    DnsProperty = 591130,

    /// <summary>
    /// accountNameHistory
    /// </summary>
    AccountNameHistory = 591131,

    /// <summary>
    /// mSMQInterval1
    /// </summary>
    MSMQInterval1 = 591132,

    /// <summary>
    /// mSMQInterval2
    /// </summary>
    MSMQInterval2 = 591133,

    /// <summary>
    /// mSMQSiteGatesMig
    /// </summary>
    MSMQSiteGatesMig = 591134,

    /// <summary>
    /// printDuplexSupported
    /// </summary>
    PrintDuplexSupported = 591135,

    /// <summary>
    /// aCSServerList
    /// </summary>
    ACSServerList = 591136,

    /// <summary>
    /// aCSMaxTokenBucketPerFlow
    /// </summary>
    ACSMaxTokenBucketPerFlow = 591137,

    /// <summary>
    /// aCSMaximumSDUSize
    /// </summary>
    ACSMaximumSDUSize = 591138,

    /// <summary>
    /// aCSMinimumPolicedSize
    /// </summary>
    ACSMinimumPolicedSize = 591139,

    /// <summary>
    /// aCSMinimumLatency
    /// </summary>
    ACSMinimumLatency = 591140,

    /// <summary>
    /// aCSMinimumDelayVariation
    /// </summary>
    ACSMinimumDelayVariation = 591141,

    /// <summary>
    /// aCSNonReservedPeakRate
    /// </summary>
    ACSNonReservedPeakRate = 591142,

    /// <summary>
    /// aCSNonReservedTokenSize
    /// </summary>
    ACSNonReservedTokenSize = 591143,

    /// <summary>
    /// aCSNonReservedMaxSDUSize
    /// </summary>
    ACSNonReservedMaxSDUSize = 591144,

    /// <summary>
    /// aCSNonReservedMinPolicedSize
    /// </summary>
    ACSNonReservedMinPolicedSize = 591145,

    /// <summary>
    /// pKIDefaultKeySpec
    /// </summary>
    PKIDefaultKeySpec = 591151,

    /// <summary>
    /// pKIKeyUsage
    /// </summary>
    PKIKeyUsage = 591152,

    /// <summary>
    /// pKIMaxIssuingDepth
    /// </summary>
    PKIMaxIssuingDepth = 591153,

    /// <summary>
    /// pKICriticalExtensions
    /// </summary>
    PKICriticalExtensions = 591154,

    /// <summary>
    /// pKIExpirationPeriod
    /// </summary>
    PKIExpirationPeriod = 591155,

    /// <summary>
    /// pKIOverlapPeriod
    /// </summary>
    PKIOverlapPeriod = 591156,

    /// <summary>
    /// pKIExtendedKeyUsage
    /// </summary>
    PKIExtendedKeyUsage = 591157,

    /// <summary>
    /// pKIDefaultCSPs
    /// </summary>
    PKIDefaultCSPs = 591158,

    /// <summary>
    /// pKIEnrollmentAccess
    /// </summary>
    PKIEnrollmentAccess = 591159,

    /// <summary>
    /// replInterval
    /// </summary>
    ReplInterval = 591160,

    /// <summary>
    /// mSMQUserSid
    /// </summary>
    MSMQUserSid = 591161,

    /// <summary>
    /// dSUIAdminNotification
    /// </summary>
    DSUIAdminNotification = 591167,

    /// <summary>
    /// dSUIAdminMaximum
    /// </summary>
    DSUIAdminMaximum = 591168,

    /// <summary>
    /// dSUIShellMaximum
    /// </summary>
    DSUIShellMaximum = 591169,

    /// <summary>
    /// templateRoots
    /// </summary>
    TemplateRoots = 591170,

    /// <summary>
    /// sPNMappings
    /// </summary>
    SPNMappings = 591171,

    /// <summary>
    /// gPCMachineExtensionNames
    /// </summary>
    GPCMachineExtensionNames = 591172,

    /// <summary>
    /// gPCUserExtensionNames
    /// </summary>
    GPCUserExtensionNames = 591173,

    /// <summary>
    /// localizationDisplayId
    /// </summary>
    LocalizationDisplayId = 591177,

    /// <summary>
    /// scopeFlags
    /// </summary>
    ScopeFlags = 591178,

    /// <summary>
    /// queryFilter
    /// </summary>
    QueryFilter = 591179,

    /// <summary>
    /// validAccesses
    /// </summary>
    ValidAccesses = 591180,

    /// <summary>
    /// dSCorePropagationData
    /// </summary>
    DSCorePropagationData = 591181,

    /// <summary>
    /// schemaInfo
    /// </summary>
    SchemaInfo = 591182,

    /// <summary>
    /// otherWellKnownObjects
    /// </summary>
    OtherWellKnownObjects = 591183,

    /// <summary>
    /// mS-DS-ConsistencyGuid
    /// </summary>
    MSDSConsistencyGuid = 591184,

    /// <summary>
    /// mS-DS-ConsistencyChildCount
    /// </summary>
    MSDSConsistencyChildCount = 591185,

    /// <summary>
    /// mS-SQL-Name
    /// </summary>
    MSSQLName = 591187,

    /// <summary>
    /// mS-SQL-RegisteredOwner
    /// </summary>
    MSSQLRegisteredOwner = 591188,

    /// <summary>
    /// mS-SQL-Contact
    /// </summary>
    MSSQLContact = 591189,

    /// <summary>
    /// mS-SQL-Location
    /// </summary>
    MSSQLLocation = 591190,

    /// <summary>
    /// mS-SQL-Memory
    /// </summary>
    MSSQLMemory = 591191,

    /// <summary>
    /// mS-SQL-Build
    /// </summary>
    MSSQLBuild = 591192,

    /// <summary>
    /// mS-SQL-ServiceAccount
    /// </summary>
    MSSQLServiceAccount = 591193,

    /// <summary>
    /// mS-SQL-CharacterSet
    /// </summary>
    MSSQLCharacterSet = 591194,

    /// <summary>
    /// mS-SQL-SortOrder
    /// </summary>
    MSSQLSortOrder = 591195,

    /// <summary>
    /// mS-SQL-UnicodeSortOrder
    /// </summary>
    MSSQLUnicodeSortOrder = 591196,

    /// <summary>
    /// mS-SQL-Clustered
    /// </summary>
    MSSQLClustered = 591197,

    /// <summary>
    /// mS-SQL-NamedPipe
    /// </summary>
    MSSQLNamedPipe = 591198,

    /// <summary>
    /// mS-SQL-MultiProtocol
    /// </summary>
    MSSQLMultiProtocol = 591199,

    /// <summary>
    /// mS-SQL-SPX
    /// </summary>
    MSSQLSPX = 591200,

    /// <summary>
    /// mS-SQL-TCPIP
    /// </summary>
    MSSQLTCPIP = 591201,

    /// <summary>
    /// mS-SQL-AppleTalk
    /// </summary>
    MSSQLAppleTalk = 591202,

    /// <summary>
    /// mS-SQL-Vines
    /// </summary>
    MSSQLVines = 591203,

    /// <summary>
    /// mS-SQL-Status
    /// </summary>
    MSSQLStatus = 591204,

    /// <summary>
    /// mS-SQL-LastUpdatedDate
    /// </summary>
    MSSQLLastUpdatedDate = 591205,

    /// <summary>
    /// mS-SQL-InformationURL
    /// </summary>
    MSSQLInformationURL = 591206,

    /// <summary>
    /// mS-SQL-ConnectionURL
    /// </summary>
    MSSQLConnectionURL = 591207,

    /// <summary>
    /// mS-SQL-PublicationURL
    /// </summary>
    MSSQLPublicationURL = 591208,

    /// <summary>
    /// mS-SQL-GPSLatitude
    /// </summary>
    MSSQLGPSLatitude = 591209,

    /// <summary>
    /// mS-SQL-GPSLongitude
    /// </summary>
    MSSQLGPSLongitude = 591210,

    /// <summary>
    /// mS-SQL-GPSHeight
    /// </summary>
    MSSQLGPSHeight = 591211,

    /// <summary>
    /// mS-SQL-Version
    /// </summary>
    MSSQLVersion = 591212,

    /// <summary>
    /// mS-SQL-Language
    /// </summary>
    MSSQLLanguage = 591213,

    /// <summary>
    /// mS-SQL-Description
    /// </summary>
    MSSQLDescription = 591214,

    /// <summary>
    /// mS-SQL-Type
    /// </summary>
    MSSQLType = 591215,

    /// <summary>
    /// mS-SQL-InformationDirectory
    /// </summary>
    MSSQLInformationDirectory = 591216,

    /// <summary>
    /// mS-SQL-Database
    /// </summary>
    MSSQLDatabase = 591217,

    /// <summary>
    /// mS-SQL-AllowAnonymousSubscription
    /// </summary>
    MSSQLAllowAnonymousSubscription = 591218,

    /// <summary>
    /// mS-SQL-Alias
    /// </summary>
    MSSQLAlias = 591219,

    /// <summary>
    /// mS-SQL-Size
    /// </summary>
    MSSQLSize = 591220,

    /// <summary>
    /// mS-SQL-CreationDate
    /// </summary>
    MSSQLCreationDate = 591221,

    /// <summary>
    /// mS-SQL-LastBackupDate
    /// </summary>
    MSSQLLastBackupDate = 591222,

    /// <summary>
    /// mS-SQL-LastDiagnosticDate
    /// </summary>
    MSSQLLastDiagnosticDate = 591223,

    /// <summary>
    /// mS-SQL-Applications
    /// </summary>
    MSSQLApplications = 591224,

    /// <summary>
    /// mS-SQL-Keywords
    /// </summary>
    MSSQLKeywords = 591225,

    /// <summary>
    /// mS-SQL-Publisher
    /// </summary>
    MSSQLPublisher = 591226,

    /// <summary>
    /// mS-SQL-AllowKnownPullSubscription
    /// </summary>
    MSSQLAllowKnownPullSubscription = 591227,

    /// <summary>
    /// mS-SQL-AllowImmediateUpdatingSubscription
    /// </summary>
    MSSQLAllowImmediateUpdatingSubscription = 591228,

    /// <summary>
    /// mS-SQL-AllowQueuedUpdatingSubscription
    /// </summary>
    MSSQLAllowQueuedUpdatingSubscription = 591229,

    /// <summary>
    /// mS-SQL-AllowSnapshotFilesFTPDownloading
    /// </summary>
    MSSQLAllowSnapshotFilesFTPDownloading = 591230,

    /// <summary>
    /// mS-SQL-ThirdParty
    /// </summary>
    MSSQLThirdParty = 591231,

    /// <summary>
    /// mS-DS-ReplicatesNCReason
    /// </summary>
    MSDSReplicatesNCReason = 591232,

    /// <summary>
    /// mS-DS-CreatorSID
    /// </summary>
    MSDSCreatorSId = 591234,

    /// <summary>
    /// ms-DS-MachineAccountQuota
    /// </summary>
    MSDSMachineAccountQuota = 591235,

    /// <summary>
    /// dNSTombstoned
    /// </summary>
    DnsTombstoned = 591238,

    /// <summary>
    /// mSMQLabelEx
    /// </summary>
    MSMQLabelEx = 591239,

    /// <summary>
    /// mSMQSiteNameEx
    /// </summary>
    MSMQSiteNameEx = 591240,

    /// <summary>
    /// mSMQComputerTypeEx
    /// </summary>
    MSMQComputerTypeEx = 591241,

    /// <summary>
    /// msCOM-DefaultPartitionLink
    /// </summary>
    MSCOMDefaultPartitionLink = 591251,

    /// <summary>
    /// msCOM-ObjectId
    /// </summary>
    MSCOMObjectId = 591252,

    /// <summary>
    /// msPKI-RA-Signature
    /// </summary>
    MSPKIRASignature = 591253,

    /// <summary>
    /// msPKI-Enrollment-Flag
    /// </summary>
    MSPKIEnrollmentFlag = 591254,

    /// <summary>
    /// msPKI-Private-Key-Flag
    /// </summary>
    MSPKIPrivateKeyFlag = 591255,

    /// <summary>
    /// msPKI-Certificate-Name-Flag
    /// </summary>
    MSPKICertificateNameFlag = 591256,

    /// <summary>
    /// msPKI-Minimal-Key-Size
    /// </summary>
    MSPKIMinimalKeySize = 591257,

    /// <summary>
    /// msPKI-Template-Schema-Version
    /// </summary>
    MSPKITemplateSchemaVersion = 591258,

    /// <summary>
    /// msPKI-Template-Minor-Revision
    /// </summary>
    MSPKITemplateMinorRevision = 591259,

    /// <summary>
    /// msPKI-Cert-Template-OID
    /// </summary>
    MSPKICertTemplateOid = 591260,

    /// <summary>
    /// msPKI-Supersede-Templates
    /// </summary>
    MSPKISupersedeTemplates = 591261,

    /// <summary>
    /// msPKI-RA-Policies
    /// </summary>
    MSPKIRAPolicies = 591262,

    /// <summary>
    /// msPKI-Certificate-Policy
    /// </summary>
    MSPKICertificatePolicy = 591263,

    /// <summary>
    /// msDs-Schema-Extensions
    /// </summary>
    MSdsSchemaExtensions = 591264,

    /// <summary>
    /// msDS-Cached-Membership
    /// </summary>
    MSDSCachedMembership = 591265,

    /// <summary>
    /// msDS-Cached-Membership-Time-Stamp
    /// </summary>
    MSDSCachedMembershipTimeStamp = 591266,

    /// <summary>
    /// msDS-Site-Affinity
    /// </summary>
    SiteAffinity = 591267,

    /// <summary>
    /// msDS-Preferred-GC-Site
    /// </summary>
    MSDSPreferredGCSite = 591268,

    /// <summary>
    /// msDS-Behavior-Version
    /// </summary>
    FunctionalLevel = 591283,

    /// <summary>
    /// msDS-Other-Settings
    /// </summary>
    MSDSOtherSettings = 591445,

    /// <summary>
    /// msDS-Entry-Time-To-Die
    /// </summary>
    MSDSEntryTimeToDie = 591446,

    /// <summary>
    /// msWMI-Author
    /// </summary>
    MSWMIAuthor = 591447,

    /// <summary>
    /// msWMI-ChangeDate
    /// </summary>
    MSWMIChangeDate = 591448,

    /// <summary>
    /// msWMI-ClassDefinition
    /// </summary>
    MSWMIClassDefinition = 591449,

    /// <summary>
    /// msWMI-CreationDate
    /// </summary>
    MSWMICreationDate = 591450,

    /// <summary>
    /// msWMI-ID
    /// </summary>
    MSWMIId = 591451,

    /// <summary>
    /// msWMI-IntDefault
    /// </summary>
    MSWMIintDefault = 591452,

    /// <summary>
    /// msWMI-IntMax
    /// </summary>
    MSWMIintMax = 591453,

    /// <summary>
    /// msWMI-IntMin
    /// </summary>
    MSWMIintMin = 591454,

    /// <summary>
    /// msWMI-IntValidValues
    /// </summary>
    MSWMIintValidValues = 591455,

    /// <summary>
    /// msWMI-Int8Default
    /// </summary>
    MSWMIint8Default = 591456,

    /// <summary>
    /// msWMI-Int8Max
    /// </summary>
    MSWMIint8Max = 591457,

    /// <summary>
    /// msWMI-Int8Min
    /// </summary>
    MSWMIint8Min = 591458,

    /// <summary>
    /// msWMI-Int8ValidValues
    /// </summary>
    MSWMIint8ValidValues = 591459,

    /// <summary>
    /// msWMI-StringDefault
    /// </summary>
    MSWMIstringDefault = 591460,

    /// <summary>
    /// msWMI-StringValidValues
    /// </summary>
    MSWMIstringValidValues = 591461,

    /// <summary>
    /// msWMI-Mof
    /// </summary>
    MSWMIMof = 591462,

    /// <summary>
    /// msWMI-Name
    /// </summary>
    MSWMIName = 591463,

    /// <summary>
    /// msWMI-NormalizedClass
    /// </summary>
    MSWMINormalizedClass = 591464,

    /// <summary>
    /// msWMI-PropertyName
    /// </summary>
    MSWMIPropertyName = 591465,

    /// <summary>
    /// msWMI-Query
    /// </summary>
    MSWMIQuery = 591466,

    /// <summary>
    /// msWMI-QueryLanguage
    /// </summary>
    MSWMIQueryLanguage = 591467,

    /// <summary>
    /// msWMI-SourceOrganization
    /// </summary>
    MSWMISourceOrganization = 591468,

    /// <summary>
    /// msWMI-TargetClass
    /// </summary>
    MSWMITargetClass = 591469,

    /// <summary>
    /// msWMI-TargetNameSpace
    /// </summary>
    MSWMITargetNameSpace = 591470,

    /// <summary>
    /// msWMI-TargetObject
    /// </summary>
    MSWMITargetObject = 591471,

    /// <summary>
    /// msWMI-TargetPath
    /// </summary>
    MSWMITargetPath = 591472,

    /// <summary>
    /// msWMI-TargetType
    /// </summary>
    MSWMITargetType = 591473,

    /// <summary>
    /// msDS-Replication-Notify-First-DSA-Delay
    /// </summary>
    MSDSReplicationNotifyFirstDSADelay = 591487,

    /// <summary>
    /// msDS-Replication-Notify-Subsequent-DSA-Delay
    /// </summary>
    MSDSReplicationNotifySubsequentDSADelay = 591488,

    /// <summary>
    /// msPKI-OID-Attribute
    /// </summary>
    MSPKIOIDAttribute = 591495,

    /// <summary>
    /// msPKI-OID-CPS
    /// </summary>
    MSPKIOIDCPS = 591496,

    /// <summary>
    /// msPKI-OID-User-Notice
    /// </summary>
    MSPKIOIDUserNotice = 591497,

    /// <summary>
    /// msPKI-Certificate-Application-Policy
    /// </summary>
    MSPKICertificateApplicationPolicy = 591498,

    /// <summary>
    /// msPKI-RA-Application-Policies
    /// </summary>
    MSPKIRAApplicationPolicies = 591499,

    /// <summary>
    /// msWMI-Class
    /// </summary>
    MSWMIClass = 591500,

    /// <summary>
    /// msWMI-Genus
    /// </summary>
    MSWMIGenus = 591501,

    /// <summary>
    /// msWMI-intFlags1
    /// </summary>
    MSWMIintFlags1 = 591502,

    /// <summary>
    /// msWMI-intFlags2
    /// </summary>
    MSWMIintFlags2 = 591503,

    /// <summary>
    /// msWMI-intFlags3
    /// </summary>
    MSWMIintFlags3 = 591504,

    /// <summary>
    /// msWMI-intFlags4
    /// </summary>
    MSWMIintFlags4 = 591505,

    /// <summary>
    /// msWMI-Parm1
    /// </summary>
    MSWMIParm1 = 591506,

    /// <summary>
    /// msWMI-Parm2
    /// </summary>
    MSWMIParm2 = 591507,

    /// <summary>
    /// msWMI-Parm3
    /// </summary>
    MSWMIParm3 = 591508,

    /// <summary>
    /// msWMI-Parm4
    /// </summary>
    MSWMIParm4 = 591509,

    /// <summary>
    /// msWMI-ScopeGuid
    /// </summary>
    MSWMIScopeGuid = 591510,

    /// <summary>
    /// extraColumns
    /// </summary>
    ExtraColumns = 591511,

    /// <summary>
    /// msDS-Security-Group-Extra-Classes
    /// </summary>
    MSDSSecurityGroupExtraClasses = 591512,

    /// <summary>
    /// msDS-Non-Security-Group-Extra-Classes
    /// </summary>
    MSDSNonSecurityGroupExtraClasses = 591513,

    /// <summary>
    /// adminMultiselectPropertyPages
    /// </summary>
    AdminMultiselectPropertyPages = 591514,

    /// <summary>
    /// msFRS-Topology-Pref
    /// </summary>
    MSFRSTopologyPref = 591516,

    /// <summary>
    /// gPCWQLFilter
    /// </summary>
    GPCWQLFilter = 591518,

    /// <summary>
    /// msMQ-Recipient-FormatName
    /// </summary>
    MSMQRecipientFormatName = 591519,

    /// <summary>
    /// lastLogonTimestamp
    /// </summary>
    LastLogonTimestamp = 591520,

    /// <summary>
    /// msDS-Settings
    /// </summary>
    MSDSSettings = 591521,

    /// <summary>
    /// msTAPI-uid
    /// </summary>
    MSTAPIUniqueIdentifier = 591522,

    /// <summary>
    /// msTAPI-ProtocolId
    /// </summary>
    MSTAPIProtocolId = 591523,

    /// <summary>
    /// msTAPI-ConferenceBlob
    /// </summary>
    MSTAPIConferenceBlob = 591524,

    /// <summary>
    /// msTAPI-IpAddress
    /// </summary>
    MSTAPIIpAddress = 591525,

    /// <summary>
    /// msDS-TrustForestTrustInfo
    /// </summary>
    MSDSTrustForestTrustInfo = 591526,

    /// <summary>
    /// msDS-FilterContainers
    /// </summary>
    MSDSFilterContainers = 591527,

    /// <summary>
    /// msDS-AllowedDNSSuffixes
    /// </summary>
    MSDSAllowedDNSSuffixes = 591534,

    /// <summary>
    /// msPKI-OIDLocalizedName
    /// </summary>
    MSPKIOIDLocalizedName = 591536,

    /// <summary>
    /// MSMQ-SecuredSource
    /// </summary>
    MSMQSecuredSource = 591537,

    /// <summary>
    /// MSMQ-MulticastAddress
    /// </summary>
    MSMQMulticastAddress = 591538,

    /// <summary>
    /// msDS-SPNSuffixes
    /// </summary>
    MSDSSPNSuffixes = 591539,

    /// <summary>
    /// msDS-IntId
    /// </summary>
    InternalId = 591540,

    /// <summary>
    /// msDS-AdditionalDnsHostName
    /// </summary>
    MSDSAdditionalDnsHostName = 591541,

    /// <summary>
    /// msDS-AdditionalSamAccountName
    /// </summary>
    MSDSAdditionalSamAccountName = 591542,

    /// <summary>
    /// msDS-DnsRootAlias
    /// </summary>
    MSDSDnsRootAlias = 591543,

    /// <summary>
    /// msDS-ReplicationEpoch
    /// </summary>
    MSDSReplicationEpoch = 591544,

    /// <summary>
    /// msDS-UpdateScript
    /// </summary>
    MSDSUpdateScript = 591545,

    /// <summary>
    /// hideFromAB
    /// </summary>
    HideFromAB = 591604,

    /// <summary>
    /// msDS-ExecuteScriptPassword
    /// </summary>
    MSDSExecuteScriptPassword = 591607,

    /// <summary>
    /// msDS-LogonTimeSyncInterval
    /// </summary>
    MSDSLogonTimeSyncInterval = 591608,

    /// <summary>
    /// msIIS-FTPRoot
    /// </summary>
    MSIISFTPRoot = 591609,

    /// <summary>
    /// msIIS-FTPDir
    /// </summary>
    MSIISFTPDir = 591610,

    /// <summary>
    /// msDS-AllowedToDelegateTo
    /// </summary>
    MSDSAllowedToDelegateTo = 591611,

    /// <summary>
    /// msDS-PerUserTrustQuota
    /// </summary>
    MSDSPerUserTrustQuota = 591612,

    /// <summary>
    /// msDS-AllUsersTrustQuota
    /// </summary>
    MSDSAllUsersTrustQuota = 591613,

    /// <summary>
    /// msDS-PerUserTrustTombstonesQuota
    /// </summary>
    MSDSPerUserTrustTombstonesQuota = 591614,

    /// <summary>
    /// msDS-AzLDAPQuery
    /// </summary>
    MSDSAzLDAPQuery = 591616,

    /// <summary>
    /// msDS-AzDomainTimeout
    /// </summary>
    MSDSAzDomainTimeout = 591619,

    /// <summary>
    /// msDS-AzScriptEngineCacheMax
    /// </summary>
    MSDSAzScriptEngineCacheMax = 591620,

    /// <summary>
    /// msDS-AzScriptTimeout
    /// </summary>
    MSDSAzScriptTimeout = 591621,

    /// <summary>
    /// msDS-AzApplicationName
    /// </summary>
    MSDSAzApplicationName = 591622,

    /// <summary>
    /// msDS-AzScopeName
    /// </summary>
    MSDSAzScopeName = 591623,

    /// <summary>
    /// msDS-AzOperationID
    /// </summary>
    MSDSAzOperationId = 591624,

    /// <summary>
    /// msDS-AzBizRule
    /// </summary>
    MSDSAzBizRule = 591625,

    /// <summary>
    /// msDS-AzBizRuleLanguage
    /// </summary>
    MSDSAzBizRuleLanguage = 591626,

    /// <summary>
    /// msDS-AzLastImportedBizRulePath
    /// </summary>
    MSDSAzLastImportedBizRulePath = 591627,

    /// <summary>
    /// msDS-AzGenerateAudits
    /// </summary>
    MSDSAzGenerateAudits = 591629,

    /// <summary>
    /// msDS-AzClassId
    /// </summary>
    MSDSAzClassId = 591640,

    /// <summary>
    /// msDS-AzApplicationVersion
    /// </summary>
    MSDSAzApplicationVersion = 591641,

    /// <summary>
    /// msDS-AzTaskIsRoleDefinition
    /// </summary>
    MSDSAzTaskIsRoleDefinition = 591642,

    /// <summary>
    /// msDS-AzApplicationData
    /// </summary>
    MSDSAzApplicationData = 591643,

    /// <summary>
    /// msieee80211-Data
    /// </summary>
    MSIEEE80211Data = 591645,

    /// <summary>
    /// msieee80211-DataType
    /// </summary>
    MSIEEE80211DataType = 591646,

    /// <summary>
    /// msieee80211-ID
    /// </summary>
    MSIEEE80211Id = 591647,

    /// <summary>
    /// msDS-AzMajorVersion
    /// </summary>
    MSDSAzMajorVersion = 591648,

    /// <summary>
    /// msDS-AzMinorVersion
    /// </summary>
    MSDSAzMinorVersion = 591649,

    /// <summary>
    /// msDS-RetiredReplNCSignatures
    /// </summary>
    MSDSRetiredReplNCSignatures = 591650,

    /// <summary>
    /// msDS-ByteArray
    /// </summary>
    MSDSByteArray = 591655,

    /// <summary>
    /// msDS-DateTime
    /// </summary>
    MSDSDateTime = 591656,

    /// <summary>
    /// msDS-ExternalKey
    /// </summary>
    MSDSExternalKey = 591657,

    /// <summary>
    /// msDS-ExternalStore
    /// </summary>
    MSDSExternalStore = 591658,

    /// <summary>
    /// msDS-Integer
    /// </summary>
    MSDSInteger = 591659,

    /// <summary>
    /// msDs-MaxValues
    /// </summary>
    MSDSMaxValues = 591666,

    /// <summary>
    /// msDRM-IdentityCertificate
    /// </summary>
    MSDRMIdentityCertificate = 591667,

    /// <summary>
    /// msDS-QuotaTrustee
    /// </summary>
    MSDSQuotaTrustee = 591668,

    /// <summary>
    /// msDS-QuotaAmount
    /// </summary>
    MSDSQuotaAmount = 591669,

    /// <summary>
    /// msDS-DefaultQuota
    /// </summary>
    MSDSDefaultQuota = 591670,

    /// <summary>
    /// msDS-TombstoneQuotaFactor
    /// </summary>
    MSDSTombstoneQuotaFactor = 591671,

    /// <summary>
    /// msDS-SourceObjectDN
    /// </summary>
    MSDSSourceObjectDN = 591703,

    /// <summary>
    /// msPKIRoamingTimeStamp
    /// </summary>
    PKIRoamingTimeStamp = 591716,

    /// <summary>
    /// unixUserPassword
    /// </summary>
    UnixUserPassword = 591734,

    /// <summary>
    /// msRADIUS-FramedInterfaceId
    /// </summary>
    MSRADIUSFramedInterfaceId = 591737,

    /// <summary>
    /// msRADIUS-SavedFramedInterfaceId
    /// </summary>
    MSRADIUSSavedFramedInterfaceId = 591738,

    /// <summary>
    /// msRADIUS-FramedIpv6Prefix
    /// </summary>
    MSRADIUSFramedIpv6Prefix = 591739,

    /// <summary>
    /// msRADIUS-SavedFramedIpv6Prefix
    /// </summary>
    MSRADIUSSavedFramedIpv6Prefix = 591740,

    /// <summary>
    /// msRADIUS-FramedIpv6Route
    /// </summary>
    MSRADIUSFramedIpv6Route = 591741,

    /// <summary>
    /// msRADIUS-SavedFramedIpv6Route
    /// </summary>
    MSRADIUSSavedFramedIpv6Route = 591742,

    /// <summary>
    /// msDS-SecondaryKrbTgtNumber
    /// </summary>
    SecondaryKrbTgtNumber = 591753,

    /// <summary>
    /// msDS-PhoneticFirstName
    /// </summary>
    PhoneticFirstName = 591766,

    /// <summary>
    /// msDS-PhoneticLastName
    /// </summary>
    PhoneticLastName = 591767,

    /// <summary>
    /// msDS-PhoneticDepartment
    /// </summary>
    PhoneticDepartment = 591768,

    /// <summary>
    /// msDS-PhoneticCompanyName
    /// </summary>
    PhoneticCompanyName = 591769,

    /// <summary>
    /// msDS-PhoneticDisplayName
    /// </summary>
    PhoneticDisplayName = 591770,

    /// <summary>
    /// msDS-AzObjectGuid
    /// </summary>
    MSDSAzObjectGuid = 591773,

    /// <summary>
    /// msDS-AzGenericData
    /// </summary>
    MSDSAzGenericData = 591774,

    /// <summary>
    /// ms-net-ieee-80211-GP-PolicyGUID
    /// </summary>
    MSNetIEEE80211GPPolicyGuid = 591775,

    /// <summary>
    /// ms-net-ieee-80211-GP-PolicyData
    /// </summary>
    MSNetIEEE80211GPPolicyData = 591776,

    /// <summary>
    /// ms-net-ieee-80211-GP-PolicyReserved
    /// </summary>
    MSNetIEEE80211GPPolicyReserved = 591777,

    /// <summary>
    /// ms-net-ieee-8023-GP-PolicyGUID
    /// </summary>
    MSNetIEEE8023GPPolicyGuid = 591778,

    /// <summary>
    /// ms-net-ieee-8023-GP-PolicyData
    /// </summary>
    MSNetIEEE8023GPPolicyData = 591779,

    /// <summary>
    /// ms-net-ieee-8023-GP-PolicyReserved
    /// </summary>
    MSNetIEEE8023GPPolicyReserved = 591780,

    /// <summary>
    /// msDS-PromotionSettings
    /// </summary>
    MSDSPromotionSettings = 591786,

    /// <summary>
    /// msDS-SupportedEncryptionTypes
    /// </summary>
    SupportedEncryptionTypes = 591787,

    /// <summary>
    /// msFVE-RecoveryPassword
    /// </summary>
    FVERecoveryPassword = 591788,

    /// <summary>
    /// msFVE-RecoveryGuid
    /// </summary>
    FVERecoveryGuid = 591789,

    /// <summary>
    /// msTPM-OwnerInformation
    /// </summary>
    TPMOwnerInformation = 591790,

    /// <summary>
    /// samDomainUpdates
    /// </summary>
    SamDomainUpdates = 591793,

    /// <summary>
    /// msDS-LastSuccessfulInteractiveLogonTime
    /// </summary>
    MSDSLastSuccessfulInteractiveLogonTime = 591794,

    /// <summary>
    /// msDS-LastFailedInteractiveLogonTime
    /// </summary>
    MSDSLastFailedInteractiveLogonTime = 591795,

    /// <summary>
    /// msDS-FailedInteractiveLogonCount
    /// </summary>
    MSDSFailedInteractiveLogonCount = 591796,

    /// <summary>
    /// msDS-FailedInteractiveLogonCountAtLastSuccessfulLogon
    /// </summary>
    MSDSFailedInteractiveLogonCountAtLastSuccessfulLogon = 591797,

    /// <summary>
    /// msTSProfilePath
    /// </summary>
    MSTSProfilePath = 591800,

    /// <summary>
    /// msTSHomeDirectory
    /// </summary>
    MSTSHomeDirectory = 591801,

    /// <summary>
    /// msTSHomeDrive
    /// </summary>
    MSTSHomeDrive = 591802,

    /// <summary>
    /// msTSAllowLogon
    /// </summary>
    MSTSAllowLogon = 591803,

    /// <summary>
    /// msTSRemoteControl
    /// </summary>
    MSTSRemoteControl = 591804,

    /// <summary>
    /// msTSMaxDisconnectionTime
    /// </summary>
    MSTSMaxDisconnectionTime = 591805,

    /// <summary>
    /// msTSMaxConnectionTime
    /// </summary>
    MSTSMaxConnectionTime = 591806,

    /// <summary>
    /// msTSMaxIdleTime
    /// </summary>
    MSTSMaxIdleTime = 591807,

    /// <summary>
    /// msTSReconnectionAction
    /// </summary>
    MSTSReconnectionAction = 591808,

    /// <summary>
    /// msTSBrokenConnectionAction
    /// </summary>
    MSTSBrokenConnectionAction = 591809,

    /// <summary>
    /// msTSConnectClientDrives
    /// </summary>
    MSTSConnectClientDrives = 591810,

    /// <summary>
    /// msTSConnectPrinterDrives
    /// </summary>
    MSTSConnectPrinterDrives = 591811,

    /// <summary>
    /// msTSDefaultToMainPrinter
    /// </summary>
    MSTSDefaultToMainPrinter = 591812,

    /// <summary>
    /// msTSWorkDirectory
    /// </summary>
    MSTSWorkDirectory = 591813,

    /// <summary>
    /// msTSInitialProgram
    /// </summary>
    MSTSInitialProgram = 591814,

    /// <summary>
    /// msTSProperty01
    /// </summary>
    MSTSProperty01 = 591815,

    /// <summary>
    /// msTSProperty02
    /// </summary>
    MSTSProperty02 = 591816,

    /// <summary>
    /// msTSExpireDate
    /// </summary>
    MSTSExpireDate = 591817,

    /// <summary>
    /// msTSLicenseVersion
    /// </summary>
    MSTSLicenseVersion = 591818,

    /// <summary>
    /// msTSManagingLS
    /// </summary>
    MSTSManagingLS = 591819,

    /// <summary>
    /// msDS-HABSeniorityIndex
    /// </summary>
    MSDSHABSeniorityIndex = 591821,

    /// <summary>
    /// msFVE-VolumeGuid
    /// </summary>
    FVEVolumeGuid = 591822,

    /// <summary>
    /// msFVE-KeyPackage
    /// </summary>
    FVEKeyPackage = 591823,

    /// <summary>
    /// msTSExpireDate2
    /// </summary>
    MSTSExpireDate2 = 591824,

    /// <summary>
    /// msTSLicenseVersion2
    /// </summary>
    MSTSLicenseVersion2 = 591825,

    /// <summary>
    /// msTSManagingLS2
    /// </summary>
    MSTSManagingLS2 = 591826,

    /// <summary>
    /// msTSExpireDate3
    /// </summary>
    MSTSExpireDate3 = 591827,

    /// <summary>
    /// msTSLicenseVersion3
    /// </summary>
    MSTSLicenseVersion3 = 591828,

    /// <summary>
    /// msTSManagingLS3
    /// </summary>
    MSTSManagingLS3 = 591829,

    /// <summary>
    /// msTSExpireDate4
    /// </summary>
    MSTSExpireDate4 = 591830,

    /// <summary>
    /// msTSLicenseVersion4
    /// </summary>
    MSTSLicenseVersion4 = 591831,

    /// <summary>
    /// msTSManagingLS4
    /// </summary>
    MSTSManagingLS4 = 591832,

    /// <summary>
    /// msTSLSProperty01
    /// </summary>
    MSTSLSProperty01 = 591833,

    /// <summary>
    /// msTSLSProperty02
    /// </summary>
    MSTSLSProperty02 = 591834,

    /// <summary>
    /// msDS-MaximumPasswordAge
    /// </summary>
    MSDSMaximumPasswordAge = 591835,

    /// <summary>
    /// msDS-MinimumPasswordAge
    /// </summary>
    MSDSMinimumPasswordAge = 591836,

    /// <summary>
    /// msDS-MinimumPasswordLength
    /// </summary>
    MSDSMinimumPasswordLength = 591837,

    /// <summary>
    /// msDS-PasswordHistoryLength
    /// </summary>
    MSDSPasswordHistoryLength = 591838,

    /// <summary>
    /// msDS-PasswordComplexityEnabled
    /// </summary>
    MSDSPasswordComplexityEnabled = 591839,

    /// <summary>
    /// msDS-PasswordReversibleEncryptionEnabled
    /// </summary>
    MSDSPasswordReversibleEncryptionEnabled = 591840,

    /// <summary>
    /// msDS-LockoutObservationWindow
    /// </summary>
    MSDSLockoutObservationWindow = 591841,

    /// <summary>
    /// msDS-LockoutDuration
    /// </summary>
    MSDSLockoutDuration = 591842,

    /// <summary>
    /// msDS-LockoutThreshold
    /// </summary>
    MSDSLockoutThreshold = 591843,

    /// <summary>
    /// msDS-PasswordSettingsPrecedence
    /// </summary>
    MSDSPasswordSettingsPrecedence = 591847,

    /// <summary>
    /// msDS-NcType
    /// </summary>
    MSDSNCType = 591848,

    /// <summary>
    /// msDFS-SchemaMajorVersion
    /// </summary>
    MSDFSSchemaMajorVersion = 591854,

    /// <summary>
    /// msDFS-SchemaMinorVersion
    /// </summary>
    MSDFSSchemaMinorVersion = 591855,

    /// <summary>
    /// msDFS-GenerationGUIDv2
    /// </summary>
    MSDFSGenerationGUIDv2 = 591856,

    /// <summary>
    /// msDFS-NamespaceIdentityGUIDv2
    /// </summary>
    MSDFSNamespaceIdentityGUIDv2 = 591857,

    /// <summary>
    /// msDFS-LastModifiedv2
    /// </summary>
    MSDFSLastModifiedv2 = 591858,

    /// <summary>
    /// msDFS-Ttlv2
    /// </summary>
    MSDFSTtlv2 = 591859,

    /// <summary>
    /// msDFS-Commentv2
    /// </summary>
    MSDFSCommentv2 = 591860,

    /// <summary>
    /// msDFS-Propertiesv2
    /// </summary>
    MSDFSPropertiesv2 = 591861,

    /// <summary>
    /// msDFS-TargetListv2
    /// </summary>
    MSDFSTargetListv2 = 591862,

    /// <summary>
    /// msDFS-LinkPathv2
    /// </summary>
    MSDFSLinkPathv2 = 591863,

    /// <summary>
    /// msDFS-LinkSecurityDescriptorv2
    /// </summary>
    MSDFSLinkSecurityDescriptorv2 = 591864,

    /// <summary>
    /// msDFS-LinkIdentityGUIDv2
    /// </summary>
    MSDFSLinkIdentityGUIDv2 = 591865,

    /// <summary>
    /// msDFS-ShortNameLinkPathv2
    /// </summary>
    MSDFSShortNameLinkPathv2 = 591866,

    /// <summary>
    /// msImaging-PSPIdentifier
    /// </summary>
    MSImagingPSPIdentifier = 591877,

    /// <summary>
    /// msImaging-PSPString
    /// </summary>
    MSImagingPSPString = 591878,

    /// <summary>
    /// msDS-USNLastSyncSuccess
    /// </summary>
    MSDSUSNLastSyncSuccess = 591879,

    /// <summary>
    /// isRecycled
    /// </summary>
    IsRecycled = 591882,

    /// <summary>
    /// msDS-OptionalFeatureGUID
    /// </summary>
    MSDSOptionalFeatureGUId = 591886,

    /// <summary>
    /// msDS-OptionalFeatureFlags
    /// </summary>
    MSDSOptionalFeatureFlags = 591887,

    /// <summary>
    /// msDS-RequiredDomainBehaviorVersion
    /// </summary>
    MSDSRequiredDomainBehaviorVersion = 591890,

    /// <summary>
    /// msDS-LastKnownRDN
    /// </summary>
    MSDSLastKnownRDN = 591891,

    /// <summary>
    /// msDS-DeletedObjectLifetime
    /// </summary>
    MSDSDeletedObjectLifetime = 591892,

    /// <summary>
    /// msTSEndpointData
    /// </summary>
    MSTSEndpointData = 591894,

    /// <summary>
    /// msTSEndpointType
    /// </summary>
    MSTSEndpointType = 591895,

    /// <summary>
    /// msTSEndpointPlugin
    /// </summary>
    MSTSEndpointPlugin = 591896,

    /// <summary>
    /// msPKI-Enrollment-Servers
    /// </summary>
    MSPKIEnrollmentServers = 591900,

    /// <summary>
    /// msPKI-Site-Name
    /// </summary>
    MSPKISiteName = 591901,

    /// <summary>
    /// msDS-RequiredForestBehaviorVersion
    /// </summary>
    MSDSRequiredForestBehaviorVersion = 591903,

    /// <summary>
    /// msSPP-CSVLKSkuId
    /// </summary>
    MSSPPCSVLKSkuId = 591905,

    /// <summary>
    /// msSPP-KMSIds
    /// </summary>
    MSSPPKMSIds = 591906,

    /// <summary>
    /// msSPP-InstallationId
    /// </summary>
    MSSPPInstallationId = 591907,

    /// <summary>
    /// msSPP-ConfirmationId
    /// </summary>
    MSSPPConfirmationId = 591908,

    /// <summary>
    /// msSPP-OnlineLicense
    /// </summary>
    MSSPPOnlineLicense = 591909,

    /// <summary>
    /// msSPP-PhoneLicense
    /// </summary>
    MSSPPPhoneLicense = 591910,

    /// <summary>
    /// msSPP-ConfigLicense
    /// </summary>
    MSSPPConfigLicense = 591911,

    /// <summary>
    /// msSPP-IssuanceLicense
    /// </summary>
    MSSPPIssuanceLicense = 591912,

    /// <summary>
    /// msDS-IsUsedAsResourceSecurityAttribute
    /// </summary>
    MSDSIsUsedAsResourceSecurityAttribute = 591919,

    /// <summary>
    /// msDS-ClaimPossibleValues
    /// </summary>
    MSDSClaimPossibleValues = 591921,

    /// <summary>
    /// msDS-ClaimValueType
    /// </summary>
    MSDSClaimValueType = 591922,

    /// <summary>
    /// msDS-ClaimAttributeSource
    /// </summary>
    MSDSClaimAttributeSource = 591923,

    /// <summary>
    /// msSPP-CSVLKPid
    /// </summary>
    MSSPPCSVLKPid = 591929,

    /// <summary>
    /// msSPP-CSVLKPartialProductKey
    /// </summary>
    MSSPPCSVLKPartialProductKey = 591930,

    /// <summary>
    /// msTPM-SrkPubThumbprint
    /// </summary>
    MSTPMSrkPubThumbprint = 591931,

    /// <summary>
    /// msTPM-OwnerInformationTemp
    /// </summary>
    MSTPMOwnerInformationTemp = 591932,

    /// <summary>
    /// msDNS-KeymasterZones
    /// </summary>
    MSDNSKeymasterZones = 591952,

    /// <summary>
    /// msDNS-IsSigned
    /// </summary>
    MSDNSIsSigned = 591954,

    /// <summary>
    /// msDNS-SignWithNSEC3
    /// </summary>
    MSDNSSignWithNSEC3 = 591955,

    /// <summary>
    /// msDNS-NSEC3OptOut
    /// </summary>
    MSDNSNSEC3OptOut = 591956,

    /// <summary>
    /// msDNS-MaintainTrustAnchor
    /// </summary>
    MSDNSMaintainTrustAnchor = 591957,

    /// <summary>
    /// msDNS-DSRecordAlgorithms
    /// </summary>
    MSDNSDSRecordAlgorithms = 591958,

    /// <summary>
    /// msDNS-RFC5011KeyRollovers
    /// </summary>
    MSDNSRFC5011KeyRollovers = 591959,

    /// <summary>
    /// msDNS-NSEC3HashAlgorithm
    /// </summary>
    MSDNSNSEC3HashAlgorithm = 591960,

    /// <summary>
    /// msDNS-NSEC3RandomSaltLength
    /// </summary>
    MSDNSNSEC3RandomSaltLength = 591961,

    /// <summary>
    /// msDNS-NSEC3Iterations
    /// </summary>
    MSDNSNSEC3Iterations = 591962,

    /// <summary>
    /// msDNS-DNSKEYRecordSetTTL
    /// </summary>
    MSDNSDNSKEYRecordSetTTL = 591963,

    /// <summary>
    /// msDNS-DSRecordSetTTL
    /// </summary>
    MSDNSDSRecordSetTTL = 591964,

    /// <summary>
    /// msDNS-SignatureInceptionOffset
    /// </summary>
    MSDNSSignatureInceptionOffset = 591965,

    /// <summary>
    /// msDNS-SecureDelegationPollingPeriod
    /// </summary>
    MSDNSSecureDelegationPollingPeriod = 591966,

    /// <summary>
    /// msDNS-SigningKeyDescriptors
    /// </summary>
    MSDNSSigningKeyDescriptors = 591967,

    /// <summary>
    /// msDNS-SigningKeys
    /// </summary>
    MSDNSSigningKeys = 591968,

    /// <summary>
    /// msDNS-DNSKEYRecords
    /// </summary>
    MSDNSDNSKEYRecords = 591969,

    /// <summary>
    /// msDNS-ParentHasSecureDelegation
    /// </summary>
    MSDNSParentHasSecureDelegation = 591970,

    /// <summary>
    /// msDNS-PropagationTime
    /// </summary>
    MSDNSPropagationTime = 591971,

    /// <summary>
    /// msDNS-NSEC3UserSalt
    /// </summary>
    MSDNSNSEC3UserSalt = 591972,

    /// <summary>
    /// msDNS-NSEC3CurrentSalt
    /// </summary>
    MSDNSNSEC3CurrentSalt = 591973,

    /// <summary>
    /// msAuthz-EffectiveSecurityPolicy
    /// </summary>
    MSAuthzEffectiveSecurityPolicy = 591974,

    /// <summary>
    /// msAuthz-ProposedSecurityPolicy
    /// </summary>
    MSAuthzProposedSecurityPolicy = 591975,

    /// <summary>
    /// msAuthz-LastEffectiveSecurityPolicy
    /// </summary>
    MSAuthzLastEffectiveSecurityPolicy = 591976,

    /// <summary>
    /// msAuthz-ResourceCondition
    /// </summary>
    MSAuthzResourceCondition = 591977,

    /// <summary>
    /// msAuthz-CentralAccessPolicyID
    /// </summary>
    MSAuthzCentralAccessPolicyId = 591978,

    /// <summary>
    /// msDS-ClaimSource
    /// </summary>
    MSDSClaimSource = 591981,

    /// <summary>
    /// msDS-ClaimSourceType
    /// </summary>
    MSDSClaimSourceType = 591982,

    /// <summary>
    /// msDS-ClaimIsValueSpaceRestricted
    /// </summary>
    MSDSClaimIsValueSpaceRestricted = 591983,

    /// <summary>
    /// msDS-ClaimIsSingleValued
    /// </summary>
    MSDSClaimIsSingleValued = 591984,

    /// <summary>
    /// msDS-GenerationId
    /// </summary>
    MSDSGenerationId = 591990,

    /// <summary>
    /// msKds-KDFAlgorithmID
    /// </summary>
    KdsKdfAlgorithmId = 591993,

    /// <summary>
    /// msKds-KDFParam
    /// </summary>
    KdsKdfParameters = 591994,

    /// <summary>
    /// msKds-SecretAgreementAlgorithmID
    /// </summary>
    KdsSecretAgreementAlgorithmId = 591995,

    /// <summary>
    /// msKds-SecretAgreementParam
    /// </summary>
    KdsSecretAgreementParameters = 591996,

    /// <summary>
    /// msKds-PublicKeyLength
    /// </summary>
    KdsSecretAgreementPublicKeyLength = 591997,

    /// <summary>
    /// msKds-PrivateKeyLength
    /// </summary>
    KdsSecretAgreementPrivateKeyLength = 591998,

    /// <summary>
    /// msKds-RootKeyData
    /// </summary>
    KdsRootKeyData = 591999,

    /// <summary>
    /// msKds-Version
    /// </summary>
    KdsVersion = 592000,

    /// <summary>
    /// msKds-DomainID
    /// </summary>
    KdsDomainController = 592001,

    /// <summary>
    /// msKds-UseStartTime
    /// </summary>
    KdsEffectiveTime = 592002,

    /// <summary>
    /// msKds-CreateTime
    /// </summary>
    KdsCreationTime = 592003,

    /// <summary>
    /// msImaging-ThumbprintHash
    /// </summary>
    MSImagingThumbprintHash = 592004,

    /// <summary>
    /// msImaging-HashAlgorithm
    /// </summary>
    MSImagingHashAlgorithm = 592005,

    /// <summary>
    /// msDS-AllowedToActOnBehalfOfOtherIdentity
    /// </summary>
    MSDSAllowedToActOnBehalfOfOtherIdentity = 592006,

    /// <summary>
    /// msDS-GeoCoordinatesAltitude
    /// </summary>
    MSDSGeoCoordinatesAltitude = 592007,

    /// <summary>
    /// msDS-GeoCoordinatesLatitude
    /// </summary>
    MSDSGeoCoordinatesLatitude = 592008,

    /// <summary>
    /// msDS-GeoCoordinatesLongitude
    /// </summary>
    MSDSGeoCoordinatesLongitude = 592009,

    /// <summary>
    /// msDS-IsPossibleValuesPresent
    /// </summary>
    MSDSIsPossibleValuesPresent = 592010,

    /// <summary>
    /// msDS-TransformationRules
    /// </summary>
    MSDSTransformationRules = 592013,

    /// <summary>
    /// msDS-TransformationRulesCompiled
    /// </summary>
    MSDSTransformationRulesCompiled = 592014,

    /// <summary>
    /// msDS-AppliesToResourceTypes
    /// </summary>
    MSDSAppliesToResourceTypes = 592019,

    /// <summary>
    /// msDS-ManagedPasswordId
    /// </summary>
    ManagedPasswordId = 592021,

    /// <summary>
    /// msDS-ManagedPasswordPreviousId
    /// </summary>
    ManagedPasswordPreviousId = 592022,

    /// <summary>
    /// msDS-ManagedPasswordInterval
    /// </summary>
    ManagedPasswordInterval = 592023,

    /// <summary>
    /// msDS-GroupMSAMembership
    /// </summary>
    GroupMSAMembership = 592024,

    /// <summary>
    /// msDS-RIDPoolAllocationEnabled
    /// </summary>
    MSDSRIDPoolAllocationEnabled = 592037,

    /// <summary>
    /// msDS-cloudExtensionAttribute1
    /// </summary>
    CloudExtensionAttribute1 = 592038,

    /// <summary>
    /// msDS-cloudExtensionAttribute2
    /// </summary>
    CloudExtensionAttribute2 = 592039,

    /// <summary>
    /// msDS-cloudExtensionAttribute3
    /// </summary>
    CloudExtensionAttribute3 = 592040,

    /// <summary>
    /// msDS-cloudExtensionAttribute4
    /// </summary>
    CloudExtensionAttribute4 = 592041,

    /// <summary>
    /// msDS-cloudExtensionAttribute5
    /// </summary>
    CloudExtensionAttribute5 = 592042,

    /// <summary>
    /// msDS-cloudExtensionAttribute6
    /// </summary>
    CloudExtensionAttribute6 = 592043,

    /// <summary>
    /// msDS-cloudExtensionAttribute7
    /// </summary>
    CloudExtensionAttribute7 = 592044,

    /// <summary>
    /// msDS-cloudExtensionAttribute8
    /// </summary>
    CloudExtensionAttribute8 = 592045,

    /// <summary>
    /// msDS-cloudExtensionAttribute9
    /// </summary>
    CloudExtensionAttribute9 = 592046,

    /// <summary>
    /// msDS-cloudExtensionAttribute10
    /// </summary>
    CloudExtensionAttribute10 = 592047,

    /// <summary>
    /// msDS-cloudExtensionAttribute11
    /// </summary>
    CloudExtensionAttribute11 = 592048,

    /// <summary>
    /// msDS-cloudExtensionAttribute12
    /// </summary>
    CloudExtensionAttribute12 = 592049,

    /// <summary>
    /// msDS-cloudExtensionAttribute13
    /// </summary>
    CloudExtensionAttribute13 = 592050,

    /// <summary>
    /// msDS-cloudExtensionAttribute14
    /// </summary>
    CloudExtensionAttribute14 = 592051,

    /// <summary>
    /// msDS-cloudExtensionAttribute15
    /// </summary>
    CloudExtensionAttribute15 = 592052,

    /// <summary>
    /// msDS-cloudExtensionAttribute16
    /// </summary>
    CloudExtensionAttribute16 = 592053,

    /// <summary>
    /// msDS-cloudExtensionAttribute17
    /// </summary>
    CloudExtensionAttribute17 = 592054,

    /// <summary>
    /// msDS-cloudExtensionAttribute18
    /// </summary>
    CloudExtensionAttribute18 = 592055,

    /// <summary>
    /// msDS-cloudExtensionAttribute19
    /// </summary>
    CloudExtensionAttribute19 = 592056,

    /// <summary>
    /// msDS-cloudExtensionAttribute20
    /// </summary>
    CloudExtensionAttribute20 = 592057,

    /// <summary>
    /// netbootDUID
    /// </summary>
    NetbootDuid = 592058,

    /// <summary>
    /// msDS-IssuerCertificates
    /// </summary>
    IssuerCertificates = 592064,

    /// <summary>
    /// msDS-RegistrationQuota
    /// </summary>
    MSDSRegistrationQuota = 592065,

    /// <summary>
    /// msDS-MaximumRegistrationInactivityPeriod
    /// </summary>
    MSDSMaximumRegistrationInactivityPeriod = 592066,

    /// <summary>
    /// msDS-IsEnabled
    /// </summary>
    MSDSIsEnabled = 592072,

    /// <summary>
    /// msDS-DeviceOSType
    /// </summary>
    DeviceOSType = 592073,

    /// <summary>
    /// msDS-DeviceOSVersion
    /// </summary>
    DeviceOSVersion = 592074,

    /// <summary>
    /// msDS-DevicePhysicalIDs
    /// </summary>
    DevicePhysicalIDs = 592075,

    /// <summary>
    /// msDS-DeviceID
    /// </summary>
    DeviceId = 592076,

    /// <summary>
    /// msDS-DeviceObjectVersion
    /// </summary>
    DeviceObjectVersion = 592081,

    /// <summary>
    /// msDS-RegisteredOwner
    /// </summary>
    RegisteredOwner = 592082,

    /// <summary>
    /// msDS-DeviceLocation
    /// </summary>
    DeviceLocation = 592085,

    /// <summary>
    /// msDS-ApproximateLastLogonTimeStamp
    /// </summary>
    ApproximateLastLogonTimeStamp = 592086,

    /// <summary>
    /// msDS-RegisteredUsers
    /// </summary>
    RegisteredUsers = 592087,

    /// <summary>
    /// msDS-IssuerPublicCertificates
    /// </summary>
    MSDSIssuerPublicCertificates = 592093,

    /// <summary>
    /// msDS-IsManaged
    /// </summary>
    IsManaged = 592094,

    /// <summary>
    /// msDS-CloudIsManaged
    /// </summary>
    CloudIsManaged = 592095,

    /// <summary>
    /// msDS-CloudAnchor
    /// </summary>
    MSDSCloudAnchor = 592097,

    /// <summary>
    /// msDS-CloudIssuerPublicCertificates
    /// </summary>
    MSDSCloudIssuerPublicCertificates = 592098,

    /// <summary>
    /// msDS-CloudIsEnabled
    /// </summary>
    MSDSCloudIsEnabled = 592099,

    /// <summary>
    /// msDS-SyncServerUrl
    /// </summary>
    SyncServerUrl = 592100,

    /// <summary>
    /// msDS-UserAllowedToAuthenticateTo
    /// </summary>
    MSDSUserAllowedToAuthenticateTo = 592101,

    /// <summary>
    /// msDS-UserAllowedToAuthenticateFrom
    /// </summary>
    MSDSUserAllowedToAuthenticateFrom = 592102,

    /// <summary>
    /// msDS-UserTGTLifetime
    /// </summary>
    MSDSUserTGTLifetime = 592103,

    /// <summary>
    /// msDS-ComputerAllowedToAuthenticateTo
    /// </summary>
    MSDSComputerAllowedToAuthenticateTo = 592104,

    /// <summary>
    /// msDS-ComputerTGTLifetime
    /// </summary>
    MSDSComputerTGTLifetime = 592105,

    /// <summary>
    /// msDS-ServiceAllowedToAuthenticateTo
    /// </summary>
    MSDSServiceAllowedToAuthenticateTo = 592106,

    /// <summary>
    /// msDS-ServiceAllowedToAuthenticateFrom
    /// </summary>
    MSDSServiceAllowedToAuthenticateFrom = 592107,

    /// <summary>
    /// msDS-ServiceTGTLifetime
    /// </summary>
    MSDSServiceTGTLifetime = 592108,

    /// <summary>
    /// msDS-AuthNPolicyEnforced
    /// </summary>
    MSDSAuthNPolicyEnforced = 592121,

    /// <summary>
    /// msDS-AuthNPolicySiloEnforced
    /// </summary>
    MSDSAuthNPolicySiloEnforced = 592122,

    /// <summary>
    /// msDS-DeviceMDMStatus
    /// </summary>
    DeviceMDMStatus = 592132,

    /// <summary>
    /// msDS-ExternalDirectoryObjectId
    /// </summary>
    ExternalDirectoryObjectId = 592134,

    /// <summary>
    /// msDS-IsCompliant
    /// </summary>
    MSDSIsCompliant = 592138,

    /// <summary>
    /// msDS-KeyId
    /// </summary>
    KeyId = 592139,

    /// <summary>
    /// msDS-KeyMaterial
    /// </summary>
    KeyMaterial = 592140,

    /// <summary>
    /// msDS-KeyUsage
    /// </summary>
    KeyUsage = 592141,

    /// <summary>
    /// msDS-DeviceDN
    /// </summary>
    DeviceDN = 592144,

    /// <summary>
    /// msDS-ComputerSID
    /// </summary>
    ComputerSId = 592145,

    /// <summary>
    /// msDS-CustomKeyInformation
    /// </summary>
    MSDSCustomKeyInformation = 592146,

    /// <summary>
    /// msDS-KeyApproximateLastLogonTimeStamp
    /// </summary>
    KeyApproximateLastLogonTimeStamp = 592147,

    /// <summary>
    /// msDS-ShadowPrincipalSid
    /// </summary>
    ShadowPrincipalSid = 592148,

    /// <summary>
    /// msDS-DeviceTrustType
    /// </summary>
    DeviceTrustType = 592149,

    /// <summary>
    /// msDS-ExpirePasswordsOnSmartCardOnlyAccounts
    /// </summary>
    MSDSExpirePasswordsOnSmartCardOnlyAccounts = 592168,

    /// <summary>
    /// msDS-UserAllowedNTLMNetworkAuthentication
    /// </summary>
    MSDSUserAllowedNTLMNetworkAuthentication = 592172,

    /// <summary>
    /// msDS-ServiceAllowedNTLMNetworkAuthentication
    /// </summary>
    MSDSServiceAllowedNTLMNetworkAuthentication = 592173,

    /// <summary>
    /// msDS-StrongNTLMPolicy
    /// </summary>
    MSDSStrongNTLMPolicy = 592174,

    /// <summary>
    /// msDS-SourceAnchor
    /// </summary>
    SourceAnchor = 592176,

    /// <summary>
    /// msDS-ObjectSoa
    /// </summary>
    MSDSObjectSOA = 592177,

    /// <summary>
    /// msDS-preferredDataLocation
    /// </summary>
    MSDSPreferredDataLocation = 592190,

    /// <summary>
    /// msDS-SupersededServiceAccountState
    /// </summary>
    MSDSSupersededServiceAccountState = 592195,

    /// <summary>
    /// msDS-DelegatedMSAState
    /// </summary>
    MSDSDelegatedMSAState = 592196,

    /// <summary>
    /// msDS-JetDBPageSize
    /// </summary>
    MSDSJetDBPageSize = 592202,

    /// <summary>
    /// userSMIMECertificate
    /// </summary>
    UserSMIMECertificate = 1310860,

    /// <summary>
    /// uid
    /// </summary>
    Uid = 1376257,

    /// <summary>
    /// textEncodedORAddress
    /// </summary>
    TextEncodedORAddress = 1376258,

    /// <summary>
    /// mail
    /// </summary>
    EmailAddresses = 1376259,

    /// <summary>
    /// drink
    /// </summary>
    Drink = 1376261,

    /// <summary>
    /// roomNumber
    /// </summary>
    RoomNumber = 1376262,

    /// <summary>
    /// photo
    /// </summary>
    Photo = 1376263,

    /// <summary>
    /// userClass
    /// </summary>
    UserClass = 1376264,

    /// <summary>
    /// host
    /// </summary>
    Host = 1376265,

    /// <summary>
    /// documentIdentifier
    /// </summary>
    DocumentIdentifier = 1376267,

    /// <summary>
    /// documentTitle
    /// </summary>
    DocumentTitle = 1376268,

    /// <summary>
    /// documentVersion
    /// </summary>
    DocumentVersion = 1376269,

    /// <summary>
    /// documentAuthor
    /// </summary>
    DocumentAuthor = 1376270,

    /// <summary>
    /// documentLocation
    /// </summary>
    DocumentLocation = 1376271,

    /// <summary>
    /// homePhone
    /// </summary>
    PhoneHomePrimary = 1376276,

    /// <summary>
    /// secretary
    /// </summary>
    Secretary = 1376277,

    /// <summary>
    /// dc
    /// </summary>
    DomainComponent = 1376281,

    /// <summary>
    /// associatedDomain
    /// </summary>
    AssociatedDomain = 1376293,

    /// <summary>
    /// associatedName
    /// </summary>
    AssociatedName = 1376294,

    /// <summary>
    /// mobile
    /// </summary>
    PhoneMobilePrimary = 1376297,

    /// <summary>
    /// pager
    /// </summary>
    PhonePagerPrimary = 1376298,

    /// <summary>
    /// uniqueIdentifier
    /// </summary>
    UniqueIdentifier = 1376300,

    /// <summary>
    /// organizationalStatus
    /// </summary>
    OrganizationalStatus = 1376301,

    /// <summary>
    /// buildingName
    /// </summary>
    BuildingName = 1376304,

    /// <summary>
    /// audio
    /// </summary>
    Audio = 1376311,

    /// <summary>
    /// documentPublisher
    /// </summary>
    DocumentPublisher = 1376312,

    /// <summary>
    /// jpegPhoto
    /// </summary>
    JpegPhoto = 1376316,

    /// <summary>
    /// carLicense
    /// </summary>
    CarLicense = 1441793,

    /// <summary>
    /// departmentNumber
    /// </summary>
    DepartmentNumber = 1441794,

    /// <summary>
    /// middleName
    /// </summary>
    OtherName = 1441826,

    /// <summary>
    /// thumbnailPhoto
    /// </summary>
    Picture = 1441827,

    /// <summary>
    /// thumbnailLogo
    /// </summary>
    Logo = 1441828,

    /// <summary>
    /// preferredLanguage
    /// </summary>
    preferredLanguage = 1441831,

    /// <summary>
    /// userPKCS12
    /// </summary>
    UserPKCS12 = 1442008,

    /// <summary>
    /// labeledURI
    /// </summary>
    LabeledUri = 1900601,

    /// <summary>
    /// unstructuredName
    /// </summary>
    UnstructuredName = 1966082,

    /// <summary>
    /// unstructuredAddress
    /// </summary>
    UnstructuredAddress = 1966088,

    /// <summary>
    /// msSFU30SearchContainer
    /// </summary>
    MSSFU30SearchContainer = 2162988,

    /// <summary>
    /// msSFU30KeyAttributes
    /// </summary>
    MSSFU30KeyAttributes = 2162989,

    /// <summary>
    /// msSFU30FieldSeparator
    /// </summary>
    MSSFU30FieldSeparator = 2162990,

    /// <summary>
    /// msSFU30IntraFieldSeparator
    /// </summary>
    MSSFU30IntraFieldSeparator = 2162991,

    /// <summary>
    /// msSFU30SearchAttributes
    /// </summary>
    MSSFU30SearchAttributes = 2162992,

    /// <summary>
    /// msSFU30ResultAttributes
    /// </summary>
    MSSFU30ResultAttributes = 2162993,

    /// <summary>
    /// msSFU30MapFilter
    /// </summary>
    MSSFU30MapFilter = 2162994,

    /// <summary>
    /// msSFU30MasterServerName
    /// </summary>
    MSSFU30MasterServerName = 2162995,

    /// <summary>
    /// msSFU30OrderNumber
    /// </summary>
    MSSFU30OrderNumber = 2162996,

    /// <summary>
    /// msSFU30Name
    /// </summary>
    MSSFU30Name = 2162997,

    /// <summary>
    /// msSFU30Aliases
    /// </summary>
    MSSFU30Aliases = 2163011,

    /// <summary>
    /// msSFU30KeyValues
    /// </summary>
    MSSFU30KeyValues = 2163012,

    /// <summary>
    /// msSFU30NisDomain
    /// </summary>
    MSSFU30NisDomain = 2163027,

    /// <summary>
    /// msSFU30Domains
    /// </summary>
    MSSFU30Domains = 2163028,

    /// <summary>
    /// msSFU30YpServers
    /// </summary>
    MSSFU30YpServers = 2163029,

    /// <summary>
    /// msSFU30MaxGidNumber
    /// </summary>
    MSSFU30MaxGidNumber = 2163030,

    /// <summary>
    /// msSFU30MaxUidNumber
    /// </summary>
    MSSFU30MaxUidNumber = 2163031,

    /// <summary>
    /// msSFU30NSMAPFieldPosition
    /// </summary>
    MSSFU30NSMAPFieldPosition = 2163033,

    /// <summary>
    /// msSFU30NetgroupHostAtDomain
    /// </summary>
    MSSFU30NetgroupHostAtDomain = 2163036,

    /// <summary>
    /// msSFU30NetgroupUserAtDomain
    /// </summary>
    MSSFU30NetgroupUserAtDomain = 2163037,

    /// <summary>
    /// msSFU30IsValidContainer
    /// </summary>
    MSSFU30IsValidContainer = 2163038,

    /// <summary>
    /// msSFU30CryptMethod
    /// </summary>
    MSSFU30CryptMethod = 2163040,

    /// <summary>
    /// msDFSR-Version
    /// </summary>
    MSDFSRVersion = 2293761,

    /// <summary>
    /// msDFSR-Extension
    /// </summary>
    MSDFSRExtension = 2293762,

    /// <summary>
    /// msDFSR-RootPath
    /// </summary>
    MSDFSRRootPath = 2293763,

    /// <summary>
    /// msDFSR-RootSizeInMb
    /// </summary>
    MSDFSRRootSizeInMb = 2293764,

    /// <summary>
    /// msDFSR-StagingPath
    /// </summary>
    MSDFSRStagingPath = 2293765,

    /// <summary>
    /// msDFSR-StagingSizeInMb
    /// </summary>
    MSDFSRStagingSizeInMb = 2293766,

    /// <summary>
    /// msDFSR-ConflictPath
    /// </summary>
    MSDFSRConflictPath = 2293767,

    /// <summary>
    /// msDFSR-ConflictSizeInMb
    /// </summary>
    MSDFSRConflictSizeInMb = 2293768,

    /// <summary>
    /// msDFSR-Enabled
    /// </summary>
    MSDFSREnabled = 2293769,

    /// <summary>
    /// msDFSR-ReplicationGroupType
    /// </summary>
    MSDFSRReplicationGroupType = 2293770,

    /// <summary>
    /// msDFSR-TombstoneExpiryInMin
    /// </summary>
    MSDFSRTombstoneExpiryInMin = 2293771,

    /// <summary>
    /// msDFSR-FileFilter
    /// </summary>
    MSDFSRFileFilter = 2293772,

    /// <summary>
    /// msDFSR-DirectoryFilter
    /// </summary>
    MSDFSRDirectoryFilter = 2293773,

    /// <summary>
    /// msDFSR-Schedule
    /// </summary>
    MSDFSRSchedule = 2293774,

    /// <summary>
    /// msDFSR-Keywords
    /// </summary>
    MSDFSRKeywords = 2293775,

    /// <summary>
    /// msDFSR-Flags
    /// </summary>
    MSDFSRFlags = 2293776,

    /// <summary>
    /// msDFSR-Options
    /// </summary>
    MSDFSROptions = 2293777,

    /// <summary>
    /// msDFSR-ContentSetGuid
    /// </summary>
    MSDFSRContentSetGuid = 2293778,

    /// <summary>
    /// msDFSR-RdcEnabled
    /// </summary>
    MSDFSRRdcEnabled = 2293779,

    /// <summary>
    /// msDFSR-RdcMinFileSizeInKb
    /// </summary>
    MSDFSRRdcMinFileSizeInKb = 2293780,

    /// <summary>
    /// msDFSR-DfsPath
    /// </summary>
    MSDFSRDfsPath = 2293781,

    /// <summary>
    /// msDFSR-RootFence
    /// </summary>
    MSDFSRRootFence = 2293782,

    /// <summary>
    /// msDFSR-ReplicationGroupGuid
    /// </summary>
    MSDFSRReplicationGroupGuid = 2293783,

    /// <summary>
    /// msDFSR-DfsLinkTarget
    /// </summary>
    MSDFSRDfsLinkTarget = 2293784,

    /// <summary>
    /// msDFSR-Priority
    /// </summary>
    MSDFSRPriority = 2293785,

    /// <summary>
    /// msDFSR-DeletedPath
    /// </summary>
    MSDFSRDeletedPath = 2293786,

    /// <summary>
    /// msDFSR-DeletedSizeInMb
    /// </summary>
    MSDFSRDeletedSizeInMb = 2293787,

    /// <summary>
    /// msDFSR-ReadOnly
    /// </summary>
    MSDFSRReadOnly = 2293788,

    /// <summary>
    /// msDFSR-CachePolicy
    /// </summary>
    MSDFSRCachePolicy = 2293789,

    /// <summary>
    /// msDFSR-MinDurationCacheInMin
    /// </summary>
    MSDFSRMinDurationCacheInMin = 2293790,

    /// <summary>
    /// msDFSR-MaxAgeInCacheInMin
    /// </summary>
    MSDFSRMaxAgeInCacheInMin = 2293791,

    /// <summary>
    /// msDFSR-DisablePacketPrivacy
    /// </summary>
    MSDFSRDisablePacketPrivacy = 2293792,

    /// <summary>
    /// msDFSR-DefaultCompressionExclusionFilter
    /// </summary>
    MSDFSRDefaultCompressionExclusionFilter = 2293794,

    /// <summary>
    /// msDFSR-OnDemandExclusionFileFilter
    /// </summary>
    MSDFSROnDemandExclusionFileFilter = 2293795,

    /// <summary>
    /// msDFSR-OnDemandExclusionDirectoryFilter
    /// </summary>
    MSDFSROnDemandExclusionDirectoryFilter = 2293796,

    /// <summary>
    /// msDFSR-Options2
    /// </summary>
    MSDFSROptions2 = 2293797,

    /// <summary>
    /// msDFSR-CommonStagingPath
    /// </summary>
    MSDFSRCommonStagingPath = 2293798,

    /// <summary>
    /// msDFSR-CommonStagingSizeInMb
    /// </summary>
    MSDFSRCommonStagingSizeInMb = 2293799,

    /// <summary>
    /// msDFSR-StagingCleanupTriggerInPercent
    /// </summary>
    MSDFSRStagingCleanupTriggerInPercent = 2293800,

    /// <summary>
    /// uidNumber
    /// </summary>
    UidNumber = 2424832,

    /// <summary>
    /// gidNumber
    /// </summary>
    GidNumber = 2424833,

    /// <summary>
    /// gecos
    /// </summary>
    Gecos = 2424834,

    /// <summary>
    /// unixHomeDirectory
    /// </summary>
    UnixHomeDirectory = 2424835,

    /// <summary>
    /// loginShell
    /// </summary>
    LoginShell = 2424836,

    /// <summary>
    /// shadowLastChange
    /// </summary>
    ShadowLastChange = 2424837,

    /// <summary>
    /// shadowMin
    /// </summary>
    ShadowMin = 2424838,

    /// <summary>
    /// shadowMax
    /// </summary>
    ShadowMax = 2424839,

    /// <summary>
    /// shadowWarning
    /// </summary>
    ShadowWarning = 2424840,

    /// <summary>
    /// shadowInactive
    /// </summary>
    ShadowInactive = 2424841,

    /// <summary>
    /// shadowExpire
    /// </summary>
    ShadowExpire = 2424842,

    /// <summary>
    /// shadowFlag
    /// </summary>
    ShadowFlag = 2424843,

    /// <summary>
    /// memberUid
    /// </summary>
    MemberUid = 2424844,

    /// <summary>
    /// memberNisNetgroup
    /// </summary>
    MemberNisNetgroup = 2424845,

    /// <summary>
    /// nisNetgroupTriple
    /// </summary>
    NisNetgroupTriple = 2424846,

    /// <summary>
    /// ipServicePort
    /// </summary>
    IpServicePort = 2424847,

    /// <summary>
    /// ipServiceProtocol
    /// </summary>
    IpServiceProtocol = 2424848,

    /// <summary>
    /// ipProtocolNumber
    /// </summary>
    IpProtocolNumber = 2424849,

    /// <summary>
    /// oncRpcNumber
    /// </summary>
    OncRpcNumber = 2424850,

    /// <summary>
    /// ipHostNumber
    /// </summary>
    IpHostNumber = 2424851,

    /// <summary>
    /// ipNetworkNumber
    /// </summary>
    IpNetworkNumber = 2424852,

    /// <summary>
    /// ipNetmaskNumber
    /// </summary>
    IpNetmaskNumber = 2424853,

    /// <summary>
    /// macAddress
    /// </summary>
    MacAddress = 2424854,

    /// <summary>
    /// bootParameter
    /// </summary>
    BootParameter = 2424855,

    /// <summary>
    /// bootFile
    /// </summary>
    BootFile = 2424856,

    /// <summary>
    /// nisMapName
    /// </summary>
    NisMapName = 2424858,

    /// <summary>
    /// nisMapEntry
    /// </summary>
    NisMapEntry = 2424859,

    /// <summary>
    /// member
    /// </summary>
    Member = 31,

    /// <summary>
    /// owner
    /// </summary>
    Owner = 32,

    /// <summary>
    /// hasMasterNCs
    /// </summary>
    HasMasterNCs = 131086,

    /// <summary>
    /// hasPartialReplicaNCs
    /// </summary>
    HasPartialReplicaNCs = 131087,

    /// <summary>
    /// memberOf
    /// </summary>
    IsMemberOfDL = 131174,

    /// <summary>
    /// ownerBL
    /// </summary>
    MSExchOwnerBL = 131176,

    /// <summary>
    /// directReports
    /// </summary>
    Reports = 131508,

    /// <summary>
    /// siteObject
    /// </summary>
    SiteObject = 590336,

    /// <summary>
    /// siteObjectBL
    /// </summary>
    SiteObjectBL = 590337,

    /// <summary>
    /// serverReference
    /// </summary>
    ServerReference = 590339,

    /// <summary>
    /// serverReferenceBL
    /// </summary>
    ServerReferenceBL = 590340,

    /// <summary>
    /// nonSecurityMember
    /// </summary>
    NonSecurityMember = 590354,

    /// <summary>
    /// nonSecurityMemberBL
    /// </summary>
    NonSecurityMemberBL = 590355,

    /// <summary>
    /// queryPolicyObject
    /// </summary>
    QueryPolicyObject = 590431,

    /// <summary>
    /// queryPolicyBL
    /// </summary>
    QueryPolicyBL = 590432,

    /// <summary>
    /// privilegeHolder
    /// </summary>
    PrivilegeHolder = 590461,

    /// <summary>
    /// isPrivilegeHolder
    /// </summary>
    IsPrivilegeHolder = 590462,

    /// <summary>
    /// managedBy
    /// </summary>
    ManagedBy = 590477,

    /// <summary>
    /// managedObjects
    /// </summary>
    ManagedObjects = 590478,

    /// <summary>
    /// syncMembership
    /// </summary>
    SyncMembership = 590489,

    /// <summary>
    /// bridgeheadTransportList
    /// </summary>
    BridgeheadTransportList = 590643,

    /// <summary>
    /// bridgeheadServerListBL
    /// </summary>
    BridgeheadServerListBL = 590644,

    /// <summary>
    /// siteList
    /// </summary>
    SiteList = 590645,

    /// <summary>
    /// siteLinkList
    /// </summary>
    SiteLinkList = 590646,

    /// <summary>
    /// netbootServer
    /// </summary>
    netbootServer = 590684,

    /// <summary>
    /// netbootSCPBL
    /// </summary>
    netbootSCPBL = 590688,

    /// <summary>
    /// frsComputerReference
    /// </summary>
    FrsComputerReference = 590693,

    /// <summary>
    /// frsComputerReferenceBL
    /// </summary>
    FrsComputerReferenceBL = 590694,

    /// <summary>
    /// fRSMemberReference
    /// </summary>
    FrsMemberReference = 590699,

    /// <summary>
    /// fRSMemberReferenceBL
    /// </summary>
    FrsMemberReferenceBL = 590700,

    /// <summary>
    /// fRSPrimaryMember
    /// </summary>
    FrsPrimaryMember = 590702,

    /// <summary>
    /// masteredBy
    /// </summary>
    MasteredBy = 591233,

    /// <summary>
    /// msCOM-PartitionLink
    /// </summary>
    MSCOMPartitionLink = 591247,

    /// <summary>
    /// msCOM-PartitionSetLink
    /// </summary>
    MSCOMPartitionSetLink = 591248,

    /// <summary>
    /// msCOM-UserLink
    /// </summary>
    MSCOMUserLink = 591249,

    /// <summary>
    /// msCOM-UserPartitionSetLink
    /// </summary>
    MSCOMUserPartitionSetLink = 591250,

    /// <summary>
    /// msDS-NC-Replica-Locations
    /// </summary>
    MSDSNCReplicaLocations = 591485,

    /// <summary>
    /// msFRS-Hub-Member
    /// </summary>
    MSFRSHubMember = 591517,

    /// <summary>
    /// msDS-HasInstantiatedNCs
    /// </summary>
    MSDSHasInstantiatedNCs = 591533,

    /// <summary>
    /// msDS-SDReferenceDomain
    /// </summary>
    MSDSSDReferenceDomain = 591535,

    /// <summary>
    /// msDS-NonMembers
    /// </summary>
    MSDSNonMembers = 591617,

    /// <summary>
    /// msDS-NonMembersBL
    /// </summary>
    MSDSNonMembersBL = 591618,

    /// <summary>
    /// msDS-MembersForAzRole
    /// </summary>
    MSDSMembersForAzRole = 591630,

    /// <summary>
    /// msDS-MembersForAzRoleBL
    /// </summary>
    MSDSMembersForAzRoleBL = 591631,

    /// <summary>
    /// msDS-OperationsForAzTask
    /// </summary>
    MSDSOperationsForAzTask = 591632,

    /// <summary>
    /// msDS-OperationsForAzTaskBL
    /// </summary>
    MSDSOperationsForAzTaskBL = 591633,

    /// <summary>
    /// msDS-TasksForAzTask
    /// </summary>
    MSDSTasksForAzTask = 591634,

    /// <summary>
    /// msDS-TasksForAzTaskBL
    /// </summary>
    MSDSTasksForAzTaskBL = 591635,

    /// <summary>
    /// msDS-OperationsForAzRole
    /// </summary>
    MSDSOperationsForAzRole = 591636,

    /// <summary>
    /// msDS-OperationsForAzRoleBL
    /// </summary>
    MSDSOperationsForAzRoleBL = 591637,

    /// <summary>
    /// msDS-TasksForAzRole
    /// </summary>
    MSDSTasksForAzRole = 591638,

    /// <summary>
    /// msDS-TasksForAzRoleBL
    /// </summary>
    MSDSTasksForAzRoleBL = 591639,

    /// <summary>
    /// msDS-HasDomainNCs
    /// </summary>
    DomainNamingContexts = 591644,

    /// <summary>
    /// msDS-hasMasterNCs
    /// </summary>
    MasterNamingContexts = 591660,

    /// <summary>
    /// msDs-masteredBy
    /// </summary>
    MSDSMasteredBy = 591661,

    /// <summary>
    /// msDS-ObjectReference
    /// </summary>
    MSDSObjectReference = 591664,

    /// <summary>
    /// msDS-ObjectReferenceBL
    /// </summary>
    MSDSObjectReferenceBL = 591665,

    /// <summary>
    /// msPKIDPAPIMasterKeys
    /// </summary>
    PKIDPAPIMasterKeys = 591717,

    /// <summary>
    /// msPKIAccountCredentials
    /// </summary>
    PKIAccountCredentials = 591718,

    /// <summary>
    /// msDS-KrbTgtLink
    /// </summary>
    MSDSKrbTgtLink = 591747,

    /// <summary>
    /// msDS-RevealedUsers
    /// </summary>
    MSDSRevealedUsers = 591748,

    /// <summary>
    /// msDS-hasFullReplicaNCs
    /// </summary>
    MSDSHasFullReplicaNCs = 591749,

    /// <summary>
    /// msDS-NeverRevealGroup
    /// </summary>
    MSDSNeverRevealGroup = 591750,

    /// <summary>
    /// msDS-RevealOnDemandGroup
    /// </summary>
    MSDSRevealOnDemandGroup = 591752,

    /// <summary>
    /// msDS-RevealedDSAs
    /// </summary>
    MSDSRevealedDSAs = 591754,

    /// <summary>
    /// msDS-KrbTgtLinkBl
    /// </summary>
    MSDSKrbTgtLinkBL = 591755,

    /// <summary>
    /// msDS-IsFullReplicaFor
    /// </summary>
    MSDSIsFullReplicaFor = 591756,

    /// <summary>
    /// msDS-IsDomainFor
    /// </summary>
    MSDSIsDomainFor = 591757,

    /// <summary>
    /// msDS-IsPartialReplicaFor
    /// </summary>
    MSDSIsPartialReplicaFor = 591758,

    /// <summary>
    /// msDS-AuthenticatedToAccountlist
    /// </summary>
    MSDSAuthenticatedToAccountlist = 591781,

    /// <summary>
    /// msDS-AuthenticatedAtDC
    /// </summary>
    MSDSAuthenticatedAtDC = 591782,

    /// <summary>
    /// msDS-NC-RO-Replica-Locations
    /// </summary>
    MSDSNCROReplicaLocations = 591791,

    /// <summary>
    /// msDS-NC-RO-Replica-Locations-BL
    /// </summary>
    MSDSNCROReplicaLocationsBL = 591792,

    /// <summary>
    /// msDS-PSOAppliesTo
    /// </summary>
    MSDSPSOAppliesTo = 591844,

    /// <summary>
    /// msDS-PSOApplied
    /// </summary>
    MSDSPSOApplied = 591845,

    /// <summary>
    /// addressBookRoots2
    /// </summary>
    AddressBookRoots2 = 591870,

    /// <summary>
    /// globalAddressList2
    /// </summary>
    GlobalAddressList2 = 591871,

    /// <summary>
    /// templateRoots2
    /// </summary>
    TemplateRoots2 = 591872,

    /// <summary>
    /// msDS-BridgeHeadServersUsed
    /// </summary>
    MSDSBridgeHeadServersUsed = 591873,

    /// <summary>
    /// msPKI-CredentialRoamingTokens
    /// </summary>
    PKICredentialRoamingTokens = 591874,

    /// <summary>
    /// msDS-OIDToGroupLink
    /// </summary>
    MSDSOIDToGroupLink = 591875,

    /// <summary>
    /// msDS-OIDToGroupLinkBl
    /// </summary>
    MSDSOIDToGroupLinkBL = 591876,

    /// <summary>
    /// msDS-HostServiceAccount
    /// </summary>
    MSDSHostServiceAccount = 591880,

    /// <summary>
    /// msDS-HostServiceAccountBL
    /// </summary>
    MSDSHostServiceAccountBL = 591881,

    /// <summary>
    /// msDS-EnabledFeature
    /// </summary>
    MSDSEnabledFeature = 591885,

    /// <summary>
    /// msDS-EnabledFeatureBL
    /// </summary>
    MSDSEnabledFeatureBL = 591893,

    /// <summary>
    /// msTSPrimaryDesktop
    /// </summary>
    MSTSPrimaryDesktop = 591897,

    /// <summary>
    /// msTSPrimaryDesktopBL
    /// </summary>
    MSTSPrimaryDesktopBL = 591898,

    /// <summary>
    /// msTSSecondaryDesktops
    /// </summary>
    MSTSSecondaryDesktops = 591899,

    /// <summary>
    /// msTSSecondaryDesktopBL
    /// </summary>
    MSTSSecondaryDesktopBL = 591902,

    /// <summary>
    /// msDS-ClaimTypeAppliesToClass
    /// </summary>
    MSDSClaimTypeAppliesToClass = 591924,

    /// <summary>
    /// msDS-ClaimSharesPossibleValuesWith
    /// </summary>
    MSDSClaimSharesPossibleValuesWith = 591925,

    /// <summary>
    /// msDS-ClaimSharesPossibleValuesWithBL
    /// </summary>
    MSDSClaimSharesPossibleValuesWithBL = 591926,

    /// <summary>
    /// msDS-MembersOfResourcePropertyList
    /// </summary>
    MSDSMembersOfResourcePropertyList = 591927,

    /// <summary>
    /// msDS-MembersOfResourcePropertyListBL
    /// </summary>
    MSDSMembersOfResourcePropertyListBL = 591928,

    /// <summary>
    /// msTPM-TpmInformationForComputer
    /// </summary>
    TPMInformationForComputer = 591933,

    /// <summary>
    /// msTPM-TpmInformationForComputerBL
    /// </summary>
    TPMInformationForComputerBL = 591934,

    /// <summary>
    /// msAuthz-MemberRulesInCentralAccessPolicy
    /// </summary>
    MSAuthzMemberRulesInCentralAccessPolicy = 591979,

    /// <summary>
    /// msAuthz-MemberRulesInCentralAccessPolicyBL
    /// </summary>
    MSAuthzMemberRulesInCentralAccessPolicyBL = 591980,

    /// <summary>
    /// msDS-PrimaryComputer
    /// </summary>
    PrimaryComputer = 591991,

    /// <summary>
    /// msDS-IsPrimaryComputerFor
    /// </summary>
    MSDSIsPrimaryComputerFor = 591992,

    /// <summary>
    /// msDS-ValueTypeReference
    /// </summary>
    MSDSValueTypeReference = 592011,

    /// <summary>
    /// msDS-ValueTypeReferenceBL
    /// </summary>
    MSDSValueTypeReferenceBL = 592012,

    /// <summary>
    /// msDS-IngressClaimsTransformationPolicy
    /// </summary>
    MSDSIngressClaimsTransformationPolicy = 592015,

    /// <summary>
    /// msDS-EgressClaimsTransformationPolicy
    /// </summary>
    MSDSEgressClaimsTransformationPolicy = 592016,

    /// <summary>
    /// msDS-TDOIngressBL
    /// </summary>
    MSDSTDOIngressBL = 592017,

    /// <summary>
    /// msDS-TDOEgressBL
    /// </summary>
    MSDSTDOEgressBL = 592018,

    /// <summary>
    /// msDS-AssignedAuthNPolicySilo
    /// </summary>
    MSDSAssignedAuthNPolicySilo = 592109,

    /// <summary>
    /// msDS-AssignedAuthNPolicySiloBL
    /// </summary>
    MSDSAssignedAuthNPolicySiloBL = 592110,

    /// <summary>
    /// msDS-AuthNPolicySiloMembers
    /// </summary>
    MSDSAuthNPolicySiloMembers = 592111,

    /// <summary>
    /// msDS-AuthNPolicySiloMembersBL
    /// </summary>
    MSDSAuthNPolicySiloMembersBL = 592112,

    /// <summary>
    /// msDS-UserAuthNPolicy
    /// </summary>
    MSDSUserAuthNPolicy = 592113,

    /// <summary>
    /// msDS-UserAuthNPolicyBL
    /// </summary>
    MSDSUserAuthNPolicyBL = 592114,

    /// <summary>
    /// msDS-ComputerAuthNPolicy
    /// </summary>
    MSDSComputerAuthNPolicy = 592115,

    /// <summary>
    /// msDS-ComputerAuthNPolicyBL
    /// </summary>
    MSDSComputerAuthNPolicyBL = 592116,

    /// <summary>
    /// msDS-ServiceAuthNPolicy
    /// </summary>
    MSDSServiceAuthNPolicy = 592117,

    /// <summary>
    /// msDS-ServiceAuthNPolicyBL
    /// </summary>
    MSDSServiceAuthNPolicyBL = 592118,

    /// <summary>
    /// msDS-AssignedAuthNPolicy
    /// </summary>
    MSDSAssignedAuthNPolicy = 592119,

    /// <summary>
    /// msDS-AssignedAuthNPolicyBL
    /// </summary>
    MSDSAssignedAuthNPolicyBL = 592120,

    /// <summary>
    /// msDS-KeyPrincipal
    /// </summary>
    KeyPrincipal = 592142,

    /// <summary>
    /// msDS-KeyPrincipalBL
    /// </summary>
    KeyPrincipalBL = 592143,

    /// <summary>
    /// msDS-KeyCredentialLink
    /// </summary>
    KeyCredentialLink = 592152,

    /// <summary>
    /// msDS-KeyCredentialLink-BL
    /// </summary>
    KeyCredentialLinkBL = 592153,

    /// <summary>
    /// msDS-SupersededManagedAccountLink
    /// </summary>
    MSDSSupersededManagedAccountLink = 592197,

    /// <summary>
    /// msDS-SupersededManagedAccountLinkBL
    /// </summary>
    MSDSSupersededManagedAccountLinkBL = 592198,

    /// <summary>
    /// msDS-ManagedAccountPrecededByLink
    /// </summary>
    MSDSManagedAccountPrecededByLink = 592199,

    /// <summary>
    /// msDS-ManagedAccountPrecededByLinkBL
    /// </summary>
    MSDSManagedAccountPrecededByLinkBL = 592200,

    /// <summary>
    /// manager
    /// </summary>
    Manager = 1376266,

    /// <summary>
    /// msSFU30PosixMember
    /// </summary>
    MSSFU30PosixMember = 2163034,

    /// <summary>
    /// msSFU30PosixMemberOf
    /// </summary>
    MSSFU30PosixMemberOf = 2163035,

    /// <summary>
    /// msDFSR-MemberReference
    /// </summary>
    MSDFSRMemberReference = 2293860,

    /// <summary>
    /// msDFSR-ComputerReference
    /// </summary>
    MSDFSRComputerReference = 2293861,

    /// <summary>
    /// msDFSR-MemberReferenceBL
    /// </summary>
    MSDFSRMemberReferenceBL = 2293862,

    /// <summary>
    /// msDFSR-ComputerReferenceBL
    /// </summary>
    MSDFSRComputerReferenceBL = 2293863,

    // Compressed OID range is [0x00000000..0x7FFFFFFF]
    LastCompressedOid = 0x7FFFFFFF,

    // msDS-IntId attribute value range is [0x80000000..0xBFFFFFFF]
    FirstInternalId = 0x80000000,
    LastInternalId = 0xBFFFFFFF
}
