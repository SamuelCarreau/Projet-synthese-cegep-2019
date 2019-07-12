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
    public class UpdateHeardOfUsFromCommandHandler : IRequestHandler<UpdateProfilOptionCommand<HeardOfUsFrom>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateHeardOfUsFromCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<HeardOfUsFrom> request, CancellationToken cancellationToken)
        {
            var heardOfUs = await _context.HeardOfUsFroms.FindAsync(request.Id);

            heardOfUs.Name = request.Name;

            _context.Update(heardOfUs);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}