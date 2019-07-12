using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteStatisticCommand : IRequest
    {
        public int StatisticId { get; set; }
    }
}