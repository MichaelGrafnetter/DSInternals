namespace DSInternals.SAM
{
    using DSInternals.Common;
    using DSInternals.Common.Interop;
    using DSInternals.SAM.Interop;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Security.Principal;

    public sealed class SamServer : SamObject
    {
        public const string BuiltinDomainName = "Builtin";
        private const uint PreferedMaximumBufferLength = 1000;
        private const uint InitialEnumerationContext = 0;

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

        public string[] EnumerateDomains()
        {
            uint enumerationContext = InitialEnumerationContext;
            uint countReturned;
            var domains = new List<string>();
            NtStatus lastResult;
            
            do
            {
                SafeSamEnumerationBufferPointer buffer;
                lastResult = NativeMethods.SamEnumerateDomainsInSamServer(this.Handle, ref enumerationContext, out buffer, PreferedMaximumBufferLength, out countReturned);
                Validator.AssertSuccess(lastResult);
                domains.AddRange(buffer.ToArray(countReturned).Select(item => item.Name.Buffer));
            } while (lastResult == NtStatus.MoreEntries);

            return domains.ToArray();
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
