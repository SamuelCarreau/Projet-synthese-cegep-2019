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
    public class CreateParentCommandHandler : IRequestHandler<CreateProfilOptionCommand<Parent>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateParentCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<Parent> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Parent { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}