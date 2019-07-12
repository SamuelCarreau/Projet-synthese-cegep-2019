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
    public class UpdateVolunteeringTypeCommandHandler : IRequestHandler<UpdateProfilOptionCommand<VolunteeringType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateVolunteeringTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<VolunteeringType> request, CancellationToken cancellationToken)
        {
            var volonteeringType = _context.VolunteeringTypes.Find(request.Id);

            volonteeringType.Name = request.Name;

            _context.Update(volonteeringType);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}