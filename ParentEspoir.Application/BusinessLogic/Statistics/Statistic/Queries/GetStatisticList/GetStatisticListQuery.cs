using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetStatisticListQuery : IRequest<GetStatisticListModel>
    {
        public int StatisticId { get; set; }
    }
}