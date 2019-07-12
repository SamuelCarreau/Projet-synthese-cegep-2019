using MediatR;
using System.Collections.Generic;

namespace ParentEspoir.Application
{
    public class GetParticipantsNotInWorkshopQuery : IRequest<IEnumerable<ParticipantSelectionModel>>
    {
        public int WorkshopId { get; set; }
    }
}
