using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.Common.Data
{
    public class BasicSchema : IDirectorySchema
    {
        private Dictionary<string, ISchemaAttribute> attributesByName;
        private Dictionary<int, ISchemaAttribute> attributesById;

        public BasicSchema(IList<ISchemaAttribute> attributes)
        {
            attributesByName = new Dictionary<string, ISchemaAttribute>();
            attributesById = new Dictionary<int, ISchemaAttribute>();
            foreach(var attribute in attributes)
            {
                this.attributesByName.Add(attribute.Name, attribute);
                if(attribute.Id.HasValue)
                {
                    this.attributesById.Add(attribute.Id.Value, attribute);
                }
            }
        }

        public ICollection<ISchemaAttribute> FindAllAttributes()
        {
            throw new NotImplementedException();
        }

        public ISchemaAttribute FindAttribute(string attributeName)
        {
            // TODO: Handle non-existing attributes
            return this.attributesByName[attributeName];
        }

        public int FindAttributeId(string attributeName)
        {
            // TODO: Null result validation
            return this.FindAttribute(attributeName).Id.Value;
        }

        public ISchemaAttribute FindAttribute(int attributeId)
        {
            throw new NotImplementedException();
        }
    }
}
