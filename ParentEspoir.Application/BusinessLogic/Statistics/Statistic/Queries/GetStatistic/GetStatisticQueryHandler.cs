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
    public class GetStatisticQueryHandler : IRequestHandler<GetStatisticQuery, GetStatisticModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetStatisticQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<GetStatisticModel> Handle(GetStatisticQuery request, CancellationToken cancellationToken)
        {
             return new GetStatisticModel();
        }
    }
}