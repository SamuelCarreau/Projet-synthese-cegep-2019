using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ParentEspoir.Application
{
    public class GetStatisticListQueryHandler : IRequestHandler<GetStatisticListQuery, GetStatisticListModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetStatisticListQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<GetStatisticListModel> Handle(GetStatisticListQuery request, CancellationToken cancellationToken)
        {
             return new GetStatisticListModel();
        }
    }
}