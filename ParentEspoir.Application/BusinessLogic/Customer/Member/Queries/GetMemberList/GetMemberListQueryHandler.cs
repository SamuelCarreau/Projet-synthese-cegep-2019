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
    public class GetMemberListQueryHandler : IRequestHandler<GetMemberListQuery, System.Collections.Generic.IEnumerable<Member>>
    {
        private readonly ParentEspoirDbContext _context;

        public GetMemberListQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<System.Collections.Generic.IEnumerable<Member>> Handle(GetMemberListQuery request, CancellationToken cancellationToken)
        {
             return null;
        }
    }
}