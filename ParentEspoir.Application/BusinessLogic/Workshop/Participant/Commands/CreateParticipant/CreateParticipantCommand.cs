using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class CreateParticipantCommand : IRequest
    {
        public int CustomerId { get; set; }
        public int WorkshopId { get; set; }
    }
}