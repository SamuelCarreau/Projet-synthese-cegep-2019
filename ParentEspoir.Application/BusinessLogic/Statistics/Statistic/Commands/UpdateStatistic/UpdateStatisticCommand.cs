using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class UpdateStatisticCommand : IRequest
    {
        public int StatisticId { get; set; }
    }
}