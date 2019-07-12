using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteVolunteeringCommand : IRequest
    {
        public int VolunteeringId { get; set; }
    }
}