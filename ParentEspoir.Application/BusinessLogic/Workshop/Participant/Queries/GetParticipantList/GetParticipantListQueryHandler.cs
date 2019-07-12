using MediatR;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class GetParticipantListQueryHandler : IRequestHandler<GetParticipantListQuery, IEnumerable<GetParticipantWorkshopModel>>
    {
        private readonly IMemoryCache _memory;
        private readonly ParentEspoirDbContext _context;

        public GetParticipantListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<IEnumerable<GetParticipantWorkshopModel>> Handle(GetParticipantListQuery request, CancellationToken cancellationToken)
        {
            if (!_memory.TryGetValue(InMemoryKeyConstants.PARTICIPANTS_IN_WORKSHOP + request.WorkshopId, out IEnumerable<GetParticipantWorkshopModel> participantList))
            {
                participantList = await _context.Participants
                .Include(p => p.Customer)
                .Where(p => p.IsDelete == false && p.WorkshopId == request.WorkshopId)
                .Select(p => new GetParticipantWorkshopModel
                {
                    ParticipantId = p.ParticipantId,
                    CustomerId = p.CustomerId,
                    LastName = p.Customer.LastName,
                    Name = p.Customer.FullName,
                    NbHourLate = p.NbHourLate
                }).OrderBy(c => c.Name).ToListAsync();

                var participantSum = new List<GetParticipantWorkshopModel>();

                foreach (var e in participantList)
                {
                    if (participantSum.Any(p => p.CustomerId == e.CustomerId))
                    {
                        participantSum.Where(p => p.CustomerId == e.CustomerId).Single().NbHourLate += e.NbHourLate;
                    }
                    else
                    {
                        participantSum.Add(e);
                    }
                }

                participantList = participantSum;

                _memory.Set(InMemoryKeyConstants.PARTICIPANTS_IN_WORKSHOP + request.WorkshopId, participantList);
            }

            return participantList;
        }
    }
}