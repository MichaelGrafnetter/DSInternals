namespace DSInternals.PowerShell.Commands
{
    using System.Management.Automation;
    using AutoMapper;

    [Cmdlet(VerbsCommon.Get, "ADDBSchemaAttribute")]
    [OutputType(typeof(DSInternals.PowerShell.SchemaAttribute))]
    public class GetADDBSchemaAttributeCommand : ADDBCommandBase
    {
        [Parameter(
            Position = 0,
            HelpMessage = "TODO",
            ValueFromPipeline = true
        )]
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
            Mapper.CreateMap<DSInternals.DataStore.SchemaAttribute, DSInternals.PowerShell.SchemaAttribute>();
        }
        protected override void ProcessRecord()
        {
            if (this.Name == null)
            {
                // No attribute list provided, so return all attributes.
                var attributes = this.DirectoryContext.Schema.FindAllAttributes();
                foreach (var attribute in attributes)
                {
                    var attributeTransfer = Mapper.Map<DSInternals.PowerShell.SchemaAttribute>(attribute);
                    this.WriteObject(attributeTransfer);
                }
            }
            else
            {
                // Return only requested attributes
                foreach(string attributeName in this.Name)
                {
                    var attribute = this.DirectoryContext.Schema.FindAttribute(attributeName);
                    var attributeTransfer = Mapper.Map<DSInternals.PowerShell.SchemaAttribute>(attribute);
                    this.WriteObject(attributeTransfer);
                }
            }
        }
    }
}