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
    public class CreateVolunteeringTypeCommandHandler : IRequestHandler<CreateProfilOptionCommand<VolunteeringType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateVolunteeringTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<VolunteeringType> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new VolunteeringType
            {
                Name = request.Name
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}