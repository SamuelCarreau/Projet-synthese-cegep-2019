using ParentEspoir.Domain.Enums;
using System;

namespace ParentEspoir.Application
{
    public class ParticipantShortModel
    {
        public int ParticiantId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public ParticipationStatus? ParticipationStatus { get; set; }
        public TimeSpan NbHourLate { get; set; }
        public int SeanceId { get; set; }
    }
}