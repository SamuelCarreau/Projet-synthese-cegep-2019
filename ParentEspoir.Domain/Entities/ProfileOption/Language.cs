using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class Language : IProfileOption
    {
        public Language()
        {
            CustomerDescriptions = new HashSet<CustomerDescription>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<CustomerDescription> CustomerDescriptions { get; private set; }
    }
}