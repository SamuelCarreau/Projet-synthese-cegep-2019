using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteParticipantCommand : IRequest
    {
        public int CustomerId { get; set; }
        public int WorkshopId { get; set; }
    }
}