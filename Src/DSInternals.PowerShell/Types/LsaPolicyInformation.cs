namespace DSInternals.PowerShell
{
    using System.Security.Principal;
    using DSInternals.SAM;

    public sealed class LsaPolicyInformation
    {
        /// <summary>
        /// Gets or sets the DnsDomain.
        /// </summary>
        public LsaDnsDomainInformation DnsDomain { get; set; }
        /// <summary>
        /// Gets or sets the Domain.
        /// </summary>
        public LsaDomainInformation Domain { get; set; }
        /// <summary>
        /// Gets or sets the LocalDomain.
        /// </summary>
        public LsaDomainInformation LocalDomain { get; set; }
        /// <summary>
        /// Gets or sets the MachineAccountSid.
        /// </summary>
        public SecurityIdentifier MachineAccountSid { get; set; }
    }
}