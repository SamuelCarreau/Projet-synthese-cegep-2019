using System;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetSeanceModel
    {
        public int SeanceId { get; set; }
        public string SeanceName { get; set; }
        public DateTime SeanceDate { get; set; }
        public TimeSpan SeanceTimeSpan { get; set; }
        public string SeanceDescription { get; set; }
        public int WorkshopId { get; set; }

        public IEnumerable<ParticipantShortModel> Participants { get; set; }
    }
}