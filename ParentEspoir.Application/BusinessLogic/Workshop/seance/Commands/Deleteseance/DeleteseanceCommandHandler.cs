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
    public class DeleteSeanceCommandHandler : IRequestHandler<DeleteSeanceCommand, Unit>
    {
        private readonly IMemoryCache _memory;
        private readonly ParentEspoirDbContext _context;

        public DeleteSeanceCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(DeleteSeanceCommand request, CancellationToken cancellationToken)
        {
            var seance = await _context.Seances.FindAsync(request.SeanceId);

            seance.IsDelete = true;

            _context.Update(seance);
            await _context.SaveChangesAsync(cancellationToken);

            _memory.Remove(InMemoryKeyConstants.SEANCES_IN_WORKSHOP + request.WorkshopId);
            _memory.Remove(InMemoryKeyConstants.GET_SEANCE + request.SeanceId);

            return Unit.Value;
        }
    }
}