using System;
using System.Text.RegularExpressions;

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
        /// Represents a computer account in the domain.
        /// </summary>
        public const string Computer = "computer";

        /// <summary>
        /// Stores a list of user names. Used to apply security principals on resources.
        /// </summary>
        public const string Group = "group";

        /// <summary>
        /// A container for storing users, computers, and other account objects.
        /// </summary>
        public const string OrganizationalUnit = "organizationalUnit";

        /// <summary>
        /// This class is used to hold other classes.
        /// </summary>
        public const string Container = "container";

        /// <summary>
        /// Contains information about a person or company that you may need to contact on a regular basis.
        /// </summary>
        public const string Contact = "contact";

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
        /// This container holds the configuration information for a domain.
        /// </summary>
        public const string Configuration = "configuration";

        /// <summary>
        /// The top level class from which all classes are derived.
        /// </summary>
        public const string Top = "top";

        public static ClassType? Translate(string ldapDisplayName)
        {
            if (ldapDisplayName == null) throw new ArgumentNullException(nameof(ldapDisplayName));

            return ldapDisplayName switch
            {
                Secret => ClassType.Secret,
                User => ClassType.User,
                Group => ClassType.Group,
                Computer => ClassType.Computer,
                Contact => ClassType.Contact,
                Container => ClassType.Container,
                OrganizationalUnit => ClassType.OrganizationalUnit,
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
                Configuration => ClassType.Configuration,
                Top => ClassType.Top,
                _ => null
            };
        }
    }
}
