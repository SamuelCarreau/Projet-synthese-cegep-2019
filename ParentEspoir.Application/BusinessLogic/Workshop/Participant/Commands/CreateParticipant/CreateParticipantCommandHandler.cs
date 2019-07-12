using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class CreateParticipantCommandHandler : IRequestHandler<CreateParticipantCommand, Unit>
    {
        private readonly IMemoryCache _memory;
        private readonly ParentEspoirDbContext _context;

        public CreateParticipantCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
        {
            var seances = await _context.Seances.Include(s => s.Participants)
                .Where(s => s.WorkshopId == request.WorkshopId)
                .ToArrayAsync();

            foreach (var seance in seances)
            {
                seance.Participants.Add(new Participant
                {
                    CustomerId = request.CustomerId,
                    SeanceId = seance.SeanceId,
                    WorkshopId = request.WorkshopId
                });
            }

            await _context.SaveChangesAsync();

            _memory.Remove(InMemoryKeyConstants.PARTICIPANTS_IN_WORKSHOP + request.WorkshopId);


            return Unit.Value;
        }
    }
}