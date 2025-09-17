namespace DSInternals.PowerShell
{
    using System.Security.Principal;
    using DSInternals.SAM;

    /// <summary>
    /// Contains comprehensive Local Security Authority (LSA) policy information including domain details and security identifiers.
    /// </summary>
    public sealed class LsaPolicyInformation
    {
        /// <summary>
        /// Gets or sets the DNS domain information from LSA policy.
        /// </summary>
        public LsaDnsDomainInformation DnsDomain { get; set; }
        /// <summary>
        /// Gets or sets the primary domain information from LSA policy.
        /// </summary>
        public LsaDomainInformation Domain { get; set; }
        /// <summary>
        /// Gets or sets the local domain information from LSA policy.
        /// </summary>
        public LsaDomainInformation LocalDomain { get; set; }
        /// <summary>
        /// Gets or sets the security identifier of the machine account.
        /// </summary>
        public SecurityIdentifier MachineAccountSid { get; set; }
    }
}