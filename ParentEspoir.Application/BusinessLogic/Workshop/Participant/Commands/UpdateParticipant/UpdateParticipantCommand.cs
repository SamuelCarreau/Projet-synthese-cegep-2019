using MediatR;
using ParentEspoir.Domain.Enums;
using System.Collections.Generic;

namespace ParentEspoir.Application
{
    public class UpdateParticipantCommand : IRequest
    {
        public int SeanceId { get; set; }
        public int WorkshopId { get; set; }
        public ICollection<ParticipantAttendance> ParticipantsAttendance { get; set; }
    }

    public class ParticipantAttendance
    {
        public int ParticipantId { get; set; }
        public ParticipationStatus? ParticipationStatus { get; set; }
        public int NbHourLate { get; set; }
        public int NbMinuteLate { get; set; }
    }
}