namespace DSInternals.PowerShell.Commands
{
    using System.Management.Automation;
    using AutoMapper;

    [Cmdlet(VerbsCommon.Get, "ADDBDomainController")]
    [OutputType(typeof(DSInternals.PowerShell.DomainController))]
    public class GetADDBDomainControllerCommand : ADDBCommandBase
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            var dc = this.DirectoryContext.DomainController;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DSInternals.DataStore.DomainController, DSInternals.PowerShell.DomainController>()).CreateMapper();
            var dcTransfer = mapper.Map<DSInternals.PowerShell.DomainController>(dc);
            this.WriteObject(dcTransfer);
        }
    }
}
