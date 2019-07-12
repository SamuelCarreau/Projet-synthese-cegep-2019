using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class HeardOfUsFrom : IProfileOption
    {
        public HeardOfUsFrom()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<Customer> Customers { get; private set; }
    }
}
