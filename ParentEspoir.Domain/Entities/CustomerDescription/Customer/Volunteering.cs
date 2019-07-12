using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class Volunteering
    {
        public int VolunteeringId { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public int? VolonteeringTypeId { get; set; }
        public VolunteeringType Type { get; set; }
        public int HourCount { get; set; }
        public string Details { get; set; }
        public string Acknowledgment { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsDelete { get; set; }

    }
}
