using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class GetSeanceListQueryHandler : IRequestHandler<GetSeanceListQuery, SeanceListModel>
    {
        private readonly IMemoryCache _memory;
        public readonly ParentEspoirDbContext _context;

        public GetSeanceListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<SeanceListModel> Handle(GetSeanceListQuery request, CancellationToken cancellationToken)
        {
            if (!_memory.TryGetValue(InMemoryKeyConstants.SEANCES_IN_WORKSHOP + request.WorkshopId, out SeanceListModel model))
            {
                var seances = await _context
                .Seances
                .Where(s => s.IsDelete == false && s.WorkshopId == request.WorkshopId)
                .Select(x => new SeanceShortModel
                {
                    SeanceDate = x.SeanceDate,
                    SeanceId = x.SeanceId,
                    SeanceName = x.SeanceName,
                    SeanceTimeSpan = x.SeanceTimeSpan,
                    SeanceDescription = x.SeanceDescription
                }).ToListAsync();

                model = new SeanceListModel { Seances = seances };

                _memory.Set(InMemoryKeyConstants.SEANCES_IN_WORKSHOP + request.WorkshopId, model);
            }

            return model;
        }
    }
}
