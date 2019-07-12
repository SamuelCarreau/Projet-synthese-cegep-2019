using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class GetTransportTypeListQueryHandler : IRequestHandler<GetProfilOptionQuery<TransportType>, System.Collections.Generic.IEnumerable<IProfileOption>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetTransportTypeListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<System.Collections.Generic.IEnumerable<IProfileOption>> Handle(GetProfilOptionQuery<TransportType> request, CancellationToken cancellationToken)
        {
            return await request.Handle(_memory, _context);
        }
    }
}
