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
    public class DeleteIncomeSourceCommandHandler : IRequestHandler<DeleteProfilOptionCommand<IncomeSource>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteIncomeSourceCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<IncomeSource> request, CancellationToken cancellationToken)
        {
            var incomeSource = await _context.IncomeSources.FindAsync(request.Id);

            incomeSource.IsDelete = true;

            _context.Update(incomeSource);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}