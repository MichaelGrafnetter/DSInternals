namespace DSInternals.Common.Data
{
    /// <summary>
    /// Account type values are associated with accounts and indicate the type of account.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/cc245527.aspx</see>
    public enum SamAccountType : int
    {
        /// <summary>
        /// Represents a domain object.
        /// </summary>
        Domain = 0x0,
        /// <summary>
        /// Represents a group object.
        /// </summary>
        SecurityGroup = 0x10000000,
        /// <summary>
        /// Represents a group object that is not used for authorization context generation.
        /// </summary>
        DistributuionGroup = 0x10000001,
        /// <summary>
        /// Represents an alias object or a domain local group object.
        /// </summary>
        Alias = 0x20000000,
        /// <summary>
        /// Represents an alias object that is not used for authorization context generation.
        /// </summary>
        NonSecurityAlias = 0x20000001,
        /// <summary>
        /// Represents a user object.
        /// </summary>
        User = 0x30000000,
        /// <summary>
        /// Represents a computer object.
        /// </summary>
        Computer = 0x30000001,
        /// <summary>
        /// Represents a user object that is used for domain trusts.
        /// </summary>
        Trust = 0x30000002,
        /// <summary>
        /// Represents an application-defined group.
        /// </summary>
        ApplicationBasicGroup = 0x40000000,
        /// <summary>
        /// Represents an application-defined group whose members are determined by the results of a query.
        /// </summary>
        ApplicationQueryGroup = 0x40000001
    }
}
