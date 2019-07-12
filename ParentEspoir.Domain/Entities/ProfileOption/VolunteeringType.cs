using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class VolunteeringType : IProfileOption
    {
        public VolunteeringType()
        {
            Volunteerings = new HashSet<Volunteering>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<Volunteering> Volunteerings { get; set; }
    }
}
