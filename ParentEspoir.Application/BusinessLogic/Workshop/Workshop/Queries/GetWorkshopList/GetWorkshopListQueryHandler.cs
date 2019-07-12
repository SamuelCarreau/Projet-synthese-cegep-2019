using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class GetWorkshopListQueryHandler : IRequestHandler<GetWorkshopListQuery, IEnumerable<WorkshopListElementModel>>
    {
        public readonly ParentEspoirDbContext _context;

        public GetWorkshopListQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkshopListElementModel>> Handle(GetWorkshopListQuery request, CancellationToken cancellationToken)
        {
            var workshops = await _context.Workshops
                .Include(w => w.WorkshopType)
                .Where(w => w.SessionId == request.SessionId && w.IsDelete == false)
                .Select(w => new WorkshopListElementModel
                {
                    EndDate = w.EndDate,
                    StartDate = w.StartDate,
                    WorkshopDescription = w.WorkshopDescription,
                    WorkshopId = w.WorkshopId,
                    WorkshopName = w.WorkshopName,
                    WorkshopTypeName = w.WorkshopType.Name,
                    IsOpen = w.IsOpen
                }).ToListAsync();

            workshops.Sort();

            return workshops;
        }
    }
}
