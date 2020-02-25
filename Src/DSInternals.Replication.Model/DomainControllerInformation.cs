namespace DSInternals.Replication.Model
{
    /// <summary>
    /// Contains miscellaneous data for a server.
    /// </summary>
    public class DomainControllerInformation
    {
        /// <summary>
        /// Name of the account object for the domain controller (DC).
        /// </summary>
        public string ServerReference { get; set; }

        /// <summary>
        /// DNS host name of the DC.
        /// </summary>
        public string DNSHostName { get; set; }

        /// <summary>
        /// NTDS Settings object for the domain controller (DC).
        /// </summary>
        public string DsaObject { get; set; }
    }
}
