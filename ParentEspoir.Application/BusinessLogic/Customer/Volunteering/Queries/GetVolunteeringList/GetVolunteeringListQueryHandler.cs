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
    public class GetVolunteeringListQueryHandler : IRequestHandler<GetVolunteeringListQuery, System.Collections.Generic.IEnumerable<Volunteering>>
    {
        private readonly ParentEspoirDbContext _context;

        public GetVolunteeringListQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<System.Collections.Generic.IEnumerable<Volunteering>> Handle(GetVolunteeringListQuery request, CancellationToken cancellationToken)
        {
             return await _context.Volunteerings
                .Include(v => v.Type)
                .Where(v => v.IsDelete == false && v.CustomerId == request.CustomerId)
                .ToArrayAsync();
        }
    }
}