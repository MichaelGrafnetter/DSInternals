namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using AutoMapper;

    [Cmdlet(VerbsCommon.Get, "ADDBDomainController")]
    [OutputType(typeof(DSInternals.PowerShell.DomainController))]
    public class GetADDBDomainControllerCommand : ADDBCommandBase
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            Mapper.CreateMap<DSInternals.DataStore.DomainController, DSInternals.PowerShell.DomainController>();
            var dc = this.DirectoryContext.DomainController;
            var dcTransfer = Mapper.Map<DSInternals.PowerShell.DomainController>(dc);
            this.WriteObject(dcTransfer);
        }
    }
}