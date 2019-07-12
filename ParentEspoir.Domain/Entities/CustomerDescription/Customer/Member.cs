using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class Member
    {
        public int MemberId { get; set; }
        public int CustomerId {get; set;}

        public Customer Customer { get; set; }
        public int VolunteeringHourCountByMonth { get; set; }
        public decimal AmountByMonth { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
