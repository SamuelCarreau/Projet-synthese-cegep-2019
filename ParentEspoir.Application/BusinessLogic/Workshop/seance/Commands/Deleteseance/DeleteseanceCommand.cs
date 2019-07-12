using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteSeanceCommand : IRequest
    {
        public int WorkshopId { get; set; }
        public int SeanceId { get; set; }
    }
}