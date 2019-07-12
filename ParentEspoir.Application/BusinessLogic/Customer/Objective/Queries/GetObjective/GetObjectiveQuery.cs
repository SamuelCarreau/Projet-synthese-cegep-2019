using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetObjectiveQuery : IRequest<ObjectiveModel>
    {
        public int ObjectiveId { get; set; }
    }
}