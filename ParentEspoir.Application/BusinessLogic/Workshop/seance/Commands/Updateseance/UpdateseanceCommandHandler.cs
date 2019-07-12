using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class UpdateSeanceCommandHandler : IRequestHandler<UpdateSeanceCommand, Unit>
    {
        private readonly IMemoryCache _memory;
        private readonly ParentEspoirDbContext _context;

        public UpdateSeanceCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(UpdateSeanceCommand request, CancellationToken cancellationToken)
        {
            var seance = await _context.Seances.FindAsync(request.SeanceId);

            seance.SeanceDate = request.SeanceDate.Value;
            seance.SeanceDescription = request.SeanceDescription;
            seance.SeanceName = request.SeanceName;
            seance.SeanceTimeSpan = request.SeanceTimeSpan.Value;

            _context.Update(seance);
            await _context.SaveChangesAsync(cancellationToken);

            _memory.Remove(InMemoryKeyConstants.SEANCES_IN_WORKSHOP + request.WorkshopId);
            _memory.Remove(InMemoryKeyConstants.GET_SEANCE + request.SeanceId);

            return Unit.Value;
        }
    }
}