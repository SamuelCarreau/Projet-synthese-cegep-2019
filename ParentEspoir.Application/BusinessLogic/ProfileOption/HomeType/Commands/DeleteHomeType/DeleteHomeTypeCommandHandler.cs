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
    public class DeleteHomeTypeCommandHandler : IRequestHandler<DeleteProfilOptionCommand<HomeType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteHomeTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<HomeType> request, CancellationToken cancellationToken)
        {
            var homeType = await _context.HomeTypes.FindAsync(request.Id);

            homeType.IsDelete = true;

            _context.Update(homeType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}