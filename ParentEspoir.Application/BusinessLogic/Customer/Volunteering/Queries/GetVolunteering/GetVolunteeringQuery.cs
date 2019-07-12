using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetVolunteeringQuery : IRequest<GetVolunteeringModel>
    {
        public int VolunteeringId { get; set; }
    }
}