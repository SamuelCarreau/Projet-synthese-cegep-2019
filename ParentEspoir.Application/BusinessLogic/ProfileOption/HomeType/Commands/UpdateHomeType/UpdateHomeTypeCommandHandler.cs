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
    public class UpdateHomeTypeCommandHandler : IRequestHandler<UpdateProfilOptionCommand<HomeType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateHomeTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<HomeType> request, CancellationToken cancellationToken)
        {
            var homeType = await _context.HomeTypes.FindAsync(request.Id);

            homeType.Name = request.Name;

            _context.Update(homeType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}