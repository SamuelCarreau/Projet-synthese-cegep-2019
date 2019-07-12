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
    public class GetHeardOfUsFromListQueryHandler : IRequestHandler<GetProfilOptionQuery<HeardOfUsFrom>, IEnumerable<IProfileOption>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetHeardOfUsFromListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<IEnumerable<IProfileOption>> Handle(GetProfilOptionQuery<HeardOfUsFrom> request, CancellationToken cancellationToken)
        {
            return await request.Handle(_memory, _context);
        }
    }
}