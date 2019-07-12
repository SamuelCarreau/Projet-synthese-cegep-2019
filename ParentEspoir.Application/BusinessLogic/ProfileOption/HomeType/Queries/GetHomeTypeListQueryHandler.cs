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
    public class GetHomeTypeListQueryHandler : IRequestHandler<GetProfilOptionQuery<HomeType>, IEnumerable<IProfileOption>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetHomeTypeListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<IEnumerable<IProfileOption>> Handle(GetProfilOptionQuery<HomeType> request, CancellationToken cancellationToken)
        {
            return await request.Handle(_memory, _context);
        }
    }
}