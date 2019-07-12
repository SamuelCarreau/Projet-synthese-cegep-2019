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
    public class CreateIncomeSourceCommandHandler : IRequestHandler<CreateProfilOptionCommand<IncomeSource>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateIncomeSourceCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<IncomeSource> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new IncomeSource { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}