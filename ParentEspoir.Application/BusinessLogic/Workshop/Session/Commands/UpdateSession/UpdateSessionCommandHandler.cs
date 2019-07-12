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
    public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public UpdateSessionCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions.FindAsync(request.SessionId);

            session.Year = request.Year.Value;
            session.Season = request.Season.Value;

            _context.Update(session);
            await _context.SaveChangesAsync(cancellationToken);

            _memory.Remove("SESSIONLIST");

            return Unit.Value;
        }
    }
}