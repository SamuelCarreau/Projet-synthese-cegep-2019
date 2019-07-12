using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class UpdateObjectiveCommand : IRequest
    {
        public int ObjectiveId { get; set; }
    }
}