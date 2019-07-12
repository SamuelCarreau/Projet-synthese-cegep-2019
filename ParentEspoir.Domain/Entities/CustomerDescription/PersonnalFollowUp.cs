using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class PersonnalFollowUp
    {
        public CustomerDescription CustomerDescription { get; set; }
        public int PersonnalFollowUpId { get; set; }
        public int MeetingCount { get; set; }
        public bool IsDelete { get; set; }
    }
}