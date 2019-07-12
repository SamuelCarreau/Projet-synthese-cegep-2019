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
    public class UpdateIncomeSourceCommandHandler : IRequestHandler<UpdateProfilOptionCommand<IncomeSource>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateIncomeSourceCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<IncomeSource> request, CancellationToken cancellationToken)
        {
            var incomeSource = await _context.IncomeSources.FindAsync(request.Id);

            incomeSource.Name = request.Name;

            _context.Update(incomeSource);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}