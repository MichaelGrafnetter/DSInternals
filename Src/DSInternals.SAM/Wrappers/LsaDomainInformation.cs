namespace DSInternals.SAM
{
    using DSInternals.SAM.Interop;
    using System;
    using System.Security.Principal;

    /// <summary>
    /// Represents domain information retrieved from the Local Security Authority (LSA) containing domain name and security identifier.
    /// </summary>
    public class LsaDomainInformation
    {
        public LsaDomainInformation() { }

        internal LsaDomainInformation(LsaDomainInformationNative nativeInfo)
        {

            this.Name = nativeInfo.DomainName.Buffer;
            this.Sid = nativeInfo.DomainSid != IntPtr.Zero ? new SecurityIdentifier(nativeInfo.DomainSid) : null;
        }

        /// <summary>
        /// Name of the account domain.
        /// </summary>
        public string Name;

        /// <summary>
        /// SID of the account domain.
        /// </summary>
        public SecurityIdentifier Sid;
    }
}