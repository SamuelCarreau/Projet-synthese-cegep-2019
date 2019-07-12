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
    public class CreateMaritalStatusCommandHandler : IRequestHandler<CreateProfilOptionCommand<MaritalStatus>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateMaritalStatusCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<MaritalStatus> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new MaritalStatus { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}