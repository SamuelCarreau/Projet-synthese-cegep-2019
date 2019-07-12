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
    public class UpdateVolunteeringCommandHandler : IRequestHandler<UpdateVolunteeringCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateVolunteeringCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateVolunteeringCommand request, CancellationToken cancellationToken)
        {

            var volunteering = await _context.Volunteerings
                .Include(v => v.Type)
                .Include(v => v.Customer)
                .SingleAsync(v => v.VolunteeringId == request.VolunteeringId);

            volunteering.VolunteeringId = request.VolunteeringId;
            volunteering.Acknowledgment = request.Acknowledgment;
            volunteering.Amount = DecimalParser.Parse(request.Amount);
            volunteering.Date = request.Date;
            volunteering.Details = request.Details;
            volunteering.VolonteeringTypeId = request.VolunteeringTypeId;
            volunteering.Title = request.Title; 
            volunteering.HourCount = request.HourCount;
                 
            _context.Update(volunteering);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}