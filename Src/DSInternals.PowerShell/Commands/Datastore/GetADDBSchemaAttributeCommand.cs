namespace DSInternals.PowerShell.Commands
{
    using System.Management.Automation;
    using AutoMapper;

    [Cmdlet(VerbsCommon.Get, "ADDBSchemaAttribute")]
    [OutputType(typeof(DSInternals.PowerShell.SchemaAttribute))]
    public class GetADDBSchemaAttributeCommand : ADDBCommandBase
    {
        private IMapper mapper;

        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("LdapDisplayName,AttributeName,AttrName,Attr")]
        [ValidateNotNullOrEmpty]
        public string[] Name
        {
            get;
            set;
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            this.mapper = new MapperConfiguration(cfg => cfg.CreateMap<DSInternals.DataStore.SchemaAttribute, DSInternals.PowerShell.SchemaAttribute>()).CreateMapper();
        }
        protected override void ProcessRecord()
        {
            if (this.Name == null)
            {
                // No attribute list provided, so return all attributes.
                var attributes = this.DirectoryContext.Schema.FindAllAttributes();
                foreach (var attribute in attributes)
                {
                    var attributeTransfer = this.mapper.Map<DSInternals.PowerShell.SchemaAttribute>(attribute);
                    this.WriteObject(attributeTransfer);
                }
            }
            else
            {
                // Return only requested attributes
                foreach(string attributeName in this.Name)
                {
                    var attribute = this.DirectoryContext.Schema.FindAttribute(attributeName);
                    var attributeTransfer = this.mapper.Map<DSInternals.PowerShell.SchemaAttribute>(attribute);
                    this.WriteObject(attributeTransfer);
                }
            }
        }
    }
}
