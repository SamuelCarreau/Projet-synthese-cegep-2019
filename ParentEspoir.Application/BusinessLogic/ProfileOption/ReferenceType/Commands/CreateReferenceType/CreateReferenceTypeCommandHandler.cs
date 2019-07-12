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
    public class CreateReferenceTypeCommandHandler : IRequestHandler<CreateProfilOptionCommand<ReferenceType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateReferenceTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<ReferenceType> request, CancellationToken cancellationToken)
        {
            await _context.ReferenceTypes.AddAsync(new ReferenceType { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}