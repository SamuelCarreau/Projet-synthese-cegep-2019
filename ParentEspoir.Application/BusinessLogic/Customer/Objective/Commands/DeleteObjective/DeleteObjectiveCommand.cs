using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteObjectiveCommand : IRequest
    {
        public int ObjectiveId { get; set; }
    }
}