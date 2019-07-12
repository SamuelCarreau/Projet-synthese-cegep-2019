using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateWorkshopCommandHandler : IRequestHandler<UpdateWorkshopCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateWorkshopCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateWorkshopCommand request, CancellationToken cancellationToken)
        {
            var workshop = await _context.Workshops.FindAsync(request.WorkshopId);

            workshop.EndDate = request.EndDate.Value;
            workshop.StartDate = request.StartDate.Value;
            workshop.WorkshopDescription = request.WorkshopDescription;
            workshop.WorkshopName = request.WorkshopName;
            workshop.WorkshopTypeId = request.WorkshopTypeId.Value;
            workshop.IsOpen = request.IsOpen.Value;

            _context.Update(workshop);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}