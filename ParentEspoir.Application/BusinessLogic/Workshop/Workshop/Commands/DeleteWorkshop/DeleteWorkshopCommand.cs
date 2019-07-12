using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteWorkshopCommand : IRequest
    {
        public int WorkshopId { get; set; }
    }
}