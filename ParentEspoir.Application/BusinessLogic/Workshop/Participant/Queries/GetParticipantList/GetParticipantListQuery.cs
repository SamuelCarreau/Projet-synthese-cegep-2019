using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetParticipantListQuery : IRequest<IEnumerable<GetParticipantWorkshopModel>>
    {
        public int WorkshopId { get; set; }
    }
}