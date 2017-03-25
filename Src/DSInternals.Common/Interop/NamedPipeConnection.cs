
namespace DSInternals.Common.Interop
{
    using DSInternals.Common;
    using System;
    using System.Net;
    using System.Runtime.ConstrainedExecution;
    using System.Security;

    public class NamedPipeConnection : CriticalFinalizerObject, IDisposable
    {
        private const string IPCShareFormat = @"\\{0}\IPC$";

        public string Server
        {
            get;
            private set;
        }

        private string ShareName
        {
            get
            {
                if(this.Server != null)
                {
                    return string.Format(IPCShareFormat, this.Server);
                }
                else
                {
                    return null;
                }
            }
        }

        public NamedPipeConnection(string server, NetworkCredential credential)
        {
            // Argument validation:
            Validator.AssertNotNullOrWhiteSpace(server, "server");
            Validator.AssertNotNull(credential, "credential");
            
            this.Server = server;
            // Disconnect from the IPC share first in case of a preexisting connection. Ignore any errors.
            this.DoDisconnect();
            // Connect
            NetResource resource = new NetResource(this.ShareName);
            Win32ErrorCode result = NativeMethods.WNetAddConnection2(ref resource, credential.SecurePassword, credential.GetLogonName(), NetConnectOptions.Temporary);
            Validator.AssertSuccess(result);
        }

        private Win32ErrorCode DoDisconnect()
        {
            Win32ErrorCode result = NativeMethods.WNetCancelConnection2(this.ShareName, NetCancelOptions.NoUpdate, true);
            return result;
        }


        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Release native resources:
            this.DoDisconnect();
        }

        ~NamedPipeConnection()
        {
            Dispose(false);
        }
    }
}
