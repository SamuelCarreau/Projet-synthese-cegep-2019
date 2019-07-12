using ParentEspoir.Domain.Enums;
using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class SkillToDevelop : IProfileOption
    {
        public SkillToDevelop()
        {
            CustomerSkillToDevelops = new HashSet<CustomerSkillToDevelop>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<CustomerSkillToDevelop> CustomerSkillToDevelops { get; private set; }
    }
}