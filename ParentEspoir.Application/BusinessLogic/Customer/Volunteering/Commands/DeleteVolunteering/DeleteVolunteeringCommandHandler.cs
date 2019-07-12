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
    public class DeleteVolunteeringCommandHandler : IRequestHandler<DeleteVolunteeringCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteVolunteeringCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteVolunteeringCommand request, CancellationToken cancellationToken)
        {
            var volunteering = await _context.Volunteerings.FindAsync(request.VolunteeringId);

            volunteering.IsDelete = true;

            _context.Update(volunteering);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}