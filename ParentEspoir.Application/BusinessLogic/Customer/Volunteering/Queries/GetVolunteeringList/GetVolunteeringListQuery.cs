using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetVolunteeringListQuery : IRequest<IEnumerable<Volunteering>>
    {
        public int CustomerId { get; set; }
    }
}