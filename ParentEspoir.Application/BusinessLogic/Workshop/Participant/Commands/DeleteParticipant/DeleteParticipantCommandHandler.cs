using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class DeleteParticipantCommandHandler : IRequestHandler<DeleteParticipantCommand, Unit>
    {
        private readonly IMemoryCache _memory;
        private readonly ParentEspoirDbContext _context;

        public DeleteParticipantCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(DeleteParticipantCommand request, CancellationToken cancellationToken)
        {
            var seances = await _context.Seances.Include(s => s.Participants)
                .Where(s => s.WorkshopId == request.WorkshopId)
                .ToArrayAsync();

            foreach (var seance in seances)
            {
                if (seance.Participants.SingleOrDefault(p => p.CustomerId == request.CustomerId) != null)
                {
                    var parti = seance.Participants.Where(c => c.CustomerId == request.CustomerId).First();
                    var index = seance.Participants.Remove(parti);
                }
            }

            await _context.SaveChangesAsync();
           
            _memory.Remove(InMemoryKeyConstants.PARTICIPANTS_IN_WORKSHOP + request.WorkshopId);

            return Unit.Value;
        }
    }
}