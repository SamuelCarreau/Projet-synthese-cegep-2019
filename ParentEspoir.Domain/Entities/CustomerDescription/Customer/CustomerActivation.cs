using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class CustomerActivation
    {
        public int CustomerActivationId { get; set; }
        public int CustomerId { get; set; }
        public DateTime IsActiveSince { get; set; }
        public DateTime? IsInactiveSince { get; set; }
        public bool IsActive { get; set; }
    }
}
