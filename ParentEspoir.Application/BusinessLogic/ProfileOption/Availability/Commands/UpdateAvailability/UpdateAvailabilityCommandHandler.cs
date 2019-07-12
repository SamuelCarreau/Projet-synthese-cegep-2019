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
    public class UpdateAvailabilityCommandHandler : IRequestHandler<UpdateProfilOptionCommand<Availability>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateAvailabilityCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<Availability> request, CancellationToken cancellationToken)
        {
            var availability = await _context.Set<Availability>().FindAsync(request.Id);

            availability.Name = request.Name;

            _context.Update(availability);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}