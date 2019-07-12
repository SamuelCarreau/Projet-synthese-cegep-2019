using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ParentEspoir.Application
{
    public class GetWorkshopQueryHandler : IRequestHandler<GetWorkshopQuery, GetWorkshopModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetWorkshopQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<GetWorkshopModel> Handle(GetWorkshopQuery request, CancellationToken cancellationToken)
        {
            var workshop = await _context.Workshops
                .Include(w => w.Seances)
                .Include(w => w.WorkshopType)
                .Where(w => w.WorkshopId == request.WorkshopId).SingleAsync();

            var model = new GetWorkshopModel
            {
                EndDate = workshop.EndDate,
                Seances = workshop.Seances
                                .Where(s => s.IsDelete == false)
                                .Select(s => new SeanceShortModel
                                {
                                    SeanceDate = s.SeanceDate,
                                    SeanceId = s.SeanceId,
                                    SeanceName = s.SeanceName,
                                    SeanceTimeSpan = s.SeanceTimeSpan
                                }).ToList(),
                WorkshopId = workshop.WorkshopId,
                StartDate = workshop.StartDate,
                WorkshopDescription = workshop.WorkshopDescription,
                WorkshopName = workshop.WorkshopName,
                WorkshopTypeId = workshop.WorkshopTypeId,
                WorkshopTypeName = workshop.WorkshopType.Name,
                IsOpen = workshop.IsOpen
            };

            model.Seances.Sort();
            
            return model;
        }
    }
}