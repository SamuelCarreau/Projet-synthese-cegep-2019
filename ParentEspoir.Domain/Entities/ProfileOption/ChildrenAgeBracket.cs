using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class ChildrenAgeBracket : IProfileOption
    {
        public ChildrenAgeBracket()
        {
            CustomerChildrenAgeBrackets = new HashSet<CustomerChildrenAgeBracket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<CustomerChildrenAgeBracket> CustomerChildrenAgeBrackets { get; private set; }
    }
}
