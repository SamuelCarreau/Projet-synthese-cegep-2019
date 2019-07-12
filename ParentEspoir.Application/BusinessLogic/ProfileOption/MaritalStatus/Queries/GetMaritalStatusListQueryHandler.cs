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
    public class GetMaritalStatusListQueryHandler : IRequestHandler<GetProfilOptionQuery<MaritalStatus>, IEnumerable<IProfileOption>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetMaritalStatusListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<IEnumerable<IProfileOption>> Handle(GetProfilOptionQuery<MaritalStatus> request, CancellationToken cancellationToken)
        {
            return await request.Handle(_memory, _context);
        }
    }
}