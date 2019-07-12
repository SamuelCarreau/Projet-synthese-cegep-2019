using ParentEspoir.Domain.Enums;
using System;

namespace ParentEspoir.Domain.Entities
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int SeanceId { get; set; }
        public Seance Seance { get; set; }
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public ParticipationStatus? Status { get; set; }
        public TimeSpan NbHourLate { get; set; }
        public bool IsDelete { get; set; }
    }
}