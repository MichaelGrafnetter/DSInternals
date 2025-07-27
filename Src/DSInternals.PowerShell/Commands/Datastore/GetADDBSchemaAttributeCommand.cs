namespace DSInternals.PowerShell.Commands
{
    using System.Management.Automation;
    using DSInternals.Common.Exceptions;
    using DSInternals.Common.Schema;

    [Cmdlet(VerbsCommon.Get, "ADDBSchemaAttribute")]
    [OutputType(typeof(AttributeSchema))]
    public class GetADDBSchemaAttributeCommand : ADDBCommandBase
    {

        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("LdapDisplayName,AttributeName,AttrName,Attr")]
        [ValidateNotNullOrEmpty]
        public string[] Name
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            if (this.Name == null)
            {
                // No attribute list provided, so return all attributes.
                var attributes = this.DirectoryContext.Schema.FindAllAttributes();
                foreach (var attribute in attributes)
                {
                    this.WriteObject(attribute);
                }
            }
            else
            {
                // Return only requested attributes
                foreach(string attributeName in this.Name)
                {
                    var attribute = this.DirectoryContext.Schema.FindAttribute(attributeName);

                    if (attribute != null)
                    {
                        this.WriteObject(attribute);
                        
                    }
                    else
                    {
                        // Write non-terminating error.
                        var exception = new SchemaAttributeNotFoundException(attributeName);
                        var error = new ErrorRecord(exception, "ADDBSchemaAttribute_NotFound", ErrorCategory.ObjectNotFound, attributeName);
                        this.WriteError(error);
                    }
                }
            }
        }
    }
}
