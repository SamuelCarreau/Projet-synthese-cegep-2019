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
    public class CreateHomeTypeCommandHandler : IRequestHandler<CreateProfilOptionCommand<HomeType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateHomeTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<HomeType> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new HomeType { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}