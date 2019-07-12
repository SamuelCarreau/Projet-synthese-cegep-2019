using System;
using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class Seance
    {
        public Seance()
        {
            Participants = new HashSet<Participant>();
        }

        public int SeanceId { get; set; }
        public string SeanceName { get; set; }
        public DateTime SeanceDate { get; set; }
        public TimeSpan SeanceTimeSpan { get; set; }
        public string SeanceDescription { get; set; }
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<Participant> Participants { get; private set; }
    }
}
