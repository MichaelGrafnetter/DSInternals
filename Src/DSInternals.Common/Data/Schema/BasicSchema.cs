using System;
using System.Collections.Generic;

namespace DSInternals.Common.Data
{
    public class BasicSchema : IDirectorySchema
    {
        private Dictionary<string, ISchemaAttribute> _attributesByName;
        private Dictionary<int, ISchemaAttribute> _attributesById;

        public BasicSchema(IList<ISchemaAttribute> attributes)
        {
            _attributesByName = new Dictionary<string, ISchemaAttribute>();
            _attributesById = new Dictionary<int, ISchemaAttribute>();
            foreach(ISchemaAttribute attribute in attributes)
            {
                _attributesByName.Add(attribute.Name, attribute);
                if(attribute.Id.HasValue)
                {
                    _attributesById.Add(attribute.Id.Value, attribute);
                }
            }
        }

        public ICollection<ISchemaAttribute> FindAllAttributes()
        {
            throw new NotImplementedException();
        }

        public ISchemaAttribute FindAttribute(string attributeName)
        {
            try
            {
                return _attributesByName[attributeName];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception(string.Format("attribute not found: {0}", attributeName));
            }
        }

        public int FindAttributeId(string attributeName)
        {
            ISchemaAttribute iattr = FindAttribute(attributeName);

            // never hit, because of check in FindAttribute()
            if (iattr == null)
            {
                throw new Exception(string.Format("attribute not found: {0}", attributeName));
            }

            return iattr.Id.Value;
        }

        public ISchemaAttribute FindAttribute(int attributeId)
        {
            try
            {
                return _attributesById[attributeId];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception(string.Format("attributeId not found: {0}", attributeId));
            }
        }
    }
}
