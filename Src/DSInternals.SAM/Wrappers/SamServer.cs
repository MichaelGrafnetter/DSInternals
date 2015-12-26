namespace DSInternals.SAM
{
    using DSInternals.Common;
    using DSInternals.Common.Interop;
    using DSInternals.SAM.Interop;
    using System.Net;
    using System.Security.Principal;

    public sealed class SamServer : SamObject
    {
        private NamedPipeConnection IPCConnection
        {
            get;
            set;
        }

        public string Name
        {
            get;
            private set;
        }

        public SamServer(string serverName, NetworkCredential credential, SamServerAccessMask accessMask) : base(null)
        {
            this.Connect(serverName, accessMask, credential);
        }

        public SamServer(string serverName, SamServerAccessMask accessMask) : base(null)
        {
            this.Connect(serverName, accessMask);
        }

        public SecurityIdentifier LookupDomain(string domainName)
        {
            SecurityIdentifier domainSid;
            NtStatus result = NativeMethods.SamLookupDomainInSamServer(this.Handle, domainName, out domainSid);
            Validator.AssertSuccess(result);
            return domainSid;
        }

        public SamDomain OpenDomain(string domainName, SamDomainAccessMask accessMask)
        {
            SecurityIdentifier domainSid = this.LookupDomain(domainName);
            return this.OpenDomain(domainSid, accessMask);
        }
        public SamDomain OpenDomain(SecurityIdentifier domainSid, SamDomainAccessMask accessMask)
        {
            SafeSamHandle domainHandle;
            NtStatus result = NativeMethods.SamOpenDomain(this.Handle, accessMask, domainSid, out domainHandle);
            Validator.AssertSuccess(result);
            return new SamDomain(domainHandle);
        }

        protected override void Dispose(bool disposing)
        {
            // Disconnect first...
            base.Dispose(disposing);

            // ... then free the identity handle
            if(disposing && this.IPCConnection != null)
            {
                this.IPCConnection.Dispose();
                this.IPCConnection = null;
            }
        }

        private void Connect(string serverName, SamServerAccessMask accessMask, NetworkCredential credential = null)
        {
            Validator.AssertNotNullOrWhiteSpace(serverName, "serverName");
            this.Name = serverName;

            SafeSamHandle serverHandle;
            NtStatus connectResult;
            if(credential != null )
            {
                // Connect named pipe
                // TODO: Use SamConnectWithCreds instead
                this.IPCConnection = new NamedPipeConnection(serverName, credential);
            }
            connectResult = NativeMethods.SamConnect(this.Name, out serverHandle, accessMask);
            Validator.AssertSuccess(connectResult);
            this.Handle = serverHandle;
        }
    }
}
