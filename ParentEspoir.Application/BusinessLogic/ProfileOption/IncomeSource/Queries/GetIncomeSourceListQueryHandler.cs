using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class GetIncomeSourceListQueryHandler : IRequestHandler<GetProfilOptionQuery<IncomeSource>, IEnumerable<IProfileOption>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetIncomeSourceListQueryHandler(ParentEspoirDbContext context, IMemoryCache cache)
        {
            _context = context;
            _memory = cache;
        }

        public async Task<IEnumerable<IProfileOption>> Handle(GetProfilOptionQuery<IncomeSource> request, CancellationToken cancellationToken)
        {
            return await request.Handle(_memory, _context);
        }
    }
}