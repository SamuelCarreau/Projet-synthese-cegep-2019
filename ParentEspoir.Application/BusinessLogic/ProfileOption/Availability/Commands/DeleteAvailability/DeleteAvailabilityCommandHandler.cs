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
    public class DeleteAvailabilityCommandHandler : IRequestHandler<DeleteProfilOptionCommand<Availability>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteAvailabilityCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<Availability> request, CancellationToken cancellationToken)
        {
            await request.Handle(_context, cancellationToken);

            return Unit.Value;
        }
    }
}