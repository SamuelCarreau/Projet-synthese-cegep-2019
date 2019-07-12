using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class WorkshopType 
    {
        public WorkshopType()
        {
            Workshops = new HashSet<Workshop>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<Workshop> Workshops { get; private set; }
    }
}
