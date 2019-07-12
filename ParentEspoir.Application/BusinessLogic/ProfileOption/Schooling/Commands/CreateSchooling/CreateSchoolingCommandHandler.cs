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
    public class CreateSchoolingCommandHandler : IRequestHandler<CreateProfilOptionCommand<Schooling>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateSchoolingCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<Schooling> request, CancellationToken cancellationToken)
        {
            await _context.Schoolings.AddAsync(new Schooling { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}