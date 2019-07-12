using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class PreferedDay
    {
        public DayOfWeek Day { get; set; }
        public int CustomerDescriptionID { get; set; }
        public CustomerDescription CustomerDescription { get; set; }
        public bool IsDelete { get; set; }
    }
}
