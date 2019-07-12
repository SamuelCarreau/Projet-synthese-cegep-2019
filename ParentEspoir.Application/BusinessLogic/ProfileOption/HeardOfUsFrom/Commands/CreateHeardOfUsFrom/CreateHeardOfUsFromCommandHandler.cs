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
    public class CreateHeardOfUsFromCommandHandler : IRequestHandler<CreateProfilOptionCommand<HeardOfUsFrom>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateHeardOfUsFromCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<HeardOfUsFrom> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new HeardOfUsFrom { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}