using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetObjectiveListQuery : IRequest<ObjectiveIndexViewModel>
    {
        public int CustomerId { get; set; }
    }
}