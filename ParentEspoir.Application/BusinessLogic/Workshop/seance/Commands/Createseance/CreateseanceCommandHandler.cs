using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class CreateSeanceCommandHandler : IRequestHandler<CreateSeanceCommand, Unit>
    {
        private readonly IMemoryCache _memory;
        private readonly ParentEspoirDbContext _context;

        public CreateSeanceCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(CreateSeanceCommand request, CancellationToken cancellationToken)
        {
            var seance = await _context.AddAsync(new Seance
            {
                SeanceDate = request.SeanceDate.Value,
                SeanceName = request.SeanceName,
                SeanceTimeSpan = request.SeanceTimeSpan.Value,
                WorkshopId = request.WorkshopId.Value,
                SeanceDescription = request.SeanceDescription
            });

            await _context.SaveChangesAsync(cancellationToken);

            var participants = _context.Participants
                .Where(p => p.WorkshopId == request.WorkshopId.Value)
                .Select(p => p.CustomerId);

            foreach (var customerId in participants)
            {
                if (!seance.Entity.Participants.Any(p => p.CustomerId == customerId))
                {
                    seance.Entity.Participants.Add(new Participant
                    {
                        WorkshopId = request.WorkshopId.Value,
                        CustomerId = customerId,
                        SeanceId = seance.Entity.SeanceId
                    });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            _memory.Remove(InMemoryKeyConstants.SEANCES_IN_WORKSHOP + request.WorkshopId);

            return Unit.Value;
        }
    }
}