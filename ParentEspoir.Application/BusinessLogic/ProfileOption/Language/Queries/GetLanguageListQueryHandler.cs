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
    public class GetLanguageListQueryHandler : IRequestHandler<GetProfilOptionQuery<Language>, IEnumerable<IProfileOption>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetLanguageListQueryHandler(ParentEspoirDbContext context, IMemoryCache cache)
        {
            _context = context;
            _memory = cache;
        }

        public async Task<IEnumerable<IProfileOption>> Handle(GetProfilOptionQuery<Language> request, CancellationToken cancellationToken)
        {
            return await request.Handle(_memory, _context);
        }
    }
}