using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetStatisticQuery : IRequest<GetStatisticModel>
    {
        public int StatisticId { get; set; }
    }
}