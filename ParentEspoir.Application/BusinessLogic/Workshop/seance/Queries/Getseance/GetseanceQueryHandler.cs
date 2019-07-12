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
    public class GetSeanceQueryHandler : IRequestHandler<GetSeanceQuery, GetSeanceModel>
    {
        private readonly IMemoryCache _memory;
        private readonly ParentEspoirDbContext _context;

        public GetSeanceQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<GetSeanceModel> Handle(GetSeanceQuery request, CancellationToken cancellationToken)
        {
            _memory.TryGetValue(InMemoryKeyConstants.GET_SEANCE + request.SeanceId, out GetSeanceModel model);

            var entity = await _context.Seances
            .Include(s => s.Participants)
            .ThenInclude(s => s.Customer)
            .SingleAsync(s => s.SeanceId == request.SeanceId);

            var participants = entity.Participants
                    .Where(p => p.IsDelete == false)
                    .Select(p => new ParticipantShortModel
                    {
                        CustomerId = p.CustomerId,
                        CustomerName = p.Customer.FullName,
                        NbHourLate = p.NbHourLate,
                        ParticiantId = p.ParticipantId,
                        ParticipationStatus = p.Status,
                        SeanceId = p.SeanceId
                    }).ToList();

            participants.OrderBy(c => c.CustomerName).ToList();

            model = new GetSeanceModel
            {
                SeanceId = entity.SeanceId,
                Participants = participants,
                SeanceDate = entity.SeanceDate,
                SeanceDescription = entity.SeanceDescription,
                SeanceName = entity.SeanceName,
                SeanceTimeSpan = entity.SeanceTimeSpan,
                WorkshopId = entity.WorkshopId
            };

            _memory.Set(InMemoryKeyConstants.GET_SEANCE + request.SeanceId, model);
            return model;
        }
    }
}