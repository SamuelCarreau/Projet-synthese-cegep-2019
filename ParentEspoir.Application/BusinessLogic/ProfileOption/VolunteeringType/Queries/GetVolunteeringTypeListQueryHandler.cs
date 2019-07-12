using MediatR;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class GetProfilOptionListQueryHandler : IRequestHandler<GetProfilOptionQuery<VolunteeringType>, System.Collections.Generic.IEnumerable<IProfileOption>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetProfilOptionListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<System.Collections.Generic.IEnumerable<IProfileOption>> Handle(GetProfilOptionQuery<VolunteeringType> request, CancellationToken cancellationToken)
        {
            return await request.Handle(_memory, _context);
        }
    }
}
