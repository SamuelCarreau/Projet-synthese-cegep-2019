using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class CustomerSkillToDevelop
    {
        public int CustomerId { get; set; }
        public CustomerDescription Customer { get; set; }
        public int SkillId { get; set; }
        public SkillToDevelop Skill { get; set; }
        public bool IsDelete { get; set; }
    }
}
