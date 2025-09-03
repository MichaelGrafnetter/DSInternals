using System;

namespace DSInternals.Common.Schema
{
    public static class CommonDirectoryClasses
    {
        /// <summary>
        /// A Local Security Authority secret that is used for trust relationships and to save service passwords.
        /// </summary>
        public const string Secret = "secret";

        /// <summary>
        /// Stores information about an employee or contractor who works for an organization.
        /// </summary>
        public const string User = "user";

        /// <summary>
        /// Defines a class object in the schema.
        /// </summary>
        public const string ClassSchema = "classSchema";

        /// <summary>
        /// Defines an attribute object in the schema.
        /// </summary>
        public const string AttributeSchema = "attributeSchema";

        /// <summary>
        /// Holds the schema for Active Directory Domain Services (AD DS) and the Active Directory directory service.
        /// </summary>
        /// <remarks>The Lightweight Directory Access Protocol (LDAP) name dMD stands for Directory Management Domain.</remarks>
        public const string Schema = "dMD";

        /// <summary>
        /// Root keys for the Group Key Distribution Service.
        /// </summary>
        public const string KdsRootKey = "msKds-ProvRootKey";

        /// <summary>
        /// The group managed service account class is used to create an account that can be shared by different computers in order to run Windows services.
        /// </summary>
        public const string GroupManagedServiceAccount = "msDS-GroupManagedServiceAccount";

        /// <summary>
        /// The delegated managed service account class is used to create an account which can supersede a legacy service account and can be shared by different computers.
        /// </summary>
        public const string DelegatedManagedServiceAccount = "msDS-DelegatedManagedServiceAccount";

        /// <summary>
        /// Contains a full-volume encryption recovery password with its associated GUID.
        /// </summary>
        public const string FVERecoveryInformation = "msFVE-RecoveryInformation";

        /// <summary>
        /// The container for DNS nodes. This class holds zone metadata.
        /// </summary>
        public const string DnsZone = "dnsZone";

        /// <summary>
        /// Holds the domain name system (DNS) resource records for a single host.
        /// </summary>
        public const string DnsNode = "dnsNode";

        /// <summary>
        /// Represents the AD DS and Active Directory directory service agent (DSA) process on the server.
        /// </summary>
        public const string NtdsSettings = "nTDSDSA";

        /// <summary>
        /// A subclass of the DSA, which is distinguished by its reduced privilege level.
        /// </summary>
        public const string NtdsSettingsRO = "nTDSDSARO";

        /// <summary>
        /// Translate implementation.
        /// </summary>
        public static ClassType? Translate(string ldapDisplayName)
        {
            if (ldapDisplayName == null) throw new ArgumentNullException(nameof(ldapDisplayName));

            return ldapDisplayName switch
            {
                Secret => ClassType.Secret,
                User => ClassType.User,
                ClassSchema => ClassType.ClassSchema,
                AttributeSchema => ClassType.AttributeSchema,
                Schema => ClassType.Schema,
                KdsRootKey => ClassType.KdsRootKey,
                GroupManagedServiceAccount => ClassType.GroupManagedServiceAccount,
                DelegatedManagedServiceAccount => ClassType.DelegatedManagedServiceAccount,
                FVERecoveryInformation => ClassType.FVERecoveryInformation,
                DnsZone => ClassType.DnsZone,
                DnsNode => ClassType.DnsZone,
                NtdsSettings => ClassType.NtdsSettings,
                NtdsSettingsRO => ClassType.NtdsSettingsRO,
                _ => null
            };
        }
    }
}
