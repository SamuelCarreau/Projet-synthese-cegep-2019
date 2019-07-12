using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class CitizenStatus : IProfileOption
    {
        public CitizenStatus()
        {
            CustomerDescriptions = new HashSet<CustomerDescription>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<CustomerDescription> CustomerDescriptions { get; private set; }
    }
}
