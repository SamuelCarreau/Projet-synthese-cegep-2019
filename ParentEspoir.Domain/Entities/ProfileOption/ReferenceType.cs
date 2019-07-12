using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class ReferenceType : IProfileOption
    {
        public ReferenceType()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<Customer> Customers { get; private set; }
    }
}
