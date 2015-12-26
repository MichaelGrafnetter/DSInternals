using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.Common.Data
{
    public interface IDirectorySchema
    {
        // TODO: AttributeCollection class?
        ICollection<ISchemaAttribute> FindAllAttributes();
        ISchemaAttribute FindAttribute(string attributeName);
        ISchemaAttribute FindAttribute(int attributeId);
    }
}
