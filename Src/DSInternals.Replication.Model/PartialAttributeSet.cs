using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSInternals.Replication.Model
{
    public class PartialAttributeSet : Dictionary<int, string>
    {
        // TODO: Move parent as member.
        public PartialAttributeSet() : base()
        {

        }

        public void AddAttribute(int id, string name)
        {
            this.Add(id, name);
        }
    }
}
