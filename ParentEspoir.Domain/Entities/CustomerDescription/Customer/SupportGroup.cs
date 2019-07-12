using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class SupportGroup
    {

        public SupportGroup()
        {
            Customers = new HashSet<Customer>();
        }

        public int SupportGroupId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<Customer> Customers { get; private set; }
    }
}