using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.Common.Data
{
    public class BasicSchemaAttribute : ISchemaAttribute
    {
        public BasicSchemaAttribute(int id, string name, AttributeSyntax syntax)
        {
            this.Id = id;
            this.Name = name;
            this.Syntax = syntax;
        }
        public int? Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public AttributeSyntax Syntax
        {
            get;
            private set;
        }
    }
}
