namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Data;
    using DSInternals.SAM.Interop;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, "LsaBackupKey")]
    [OutputType(typeof(DPAPIBackupKey))]
    public class GetLsaBackupKeyCommand : LsaPolicyCommandBase
    {
        #region Cmdlet Overrides
        protected override void ProcessRecord()
        {
            var keys = this.LsaPolicy.GetDPAPIBackupKeys();
            foreach(var key in keys)
            {
                this.WriteObject(key);
            }
        }

        protected override LsaPolicyAccessMask RequiredAccessMask => LsaPolicyAccessMask.GetPrivateInformation;
        #endregion Cmdlet Overrides
    }
}