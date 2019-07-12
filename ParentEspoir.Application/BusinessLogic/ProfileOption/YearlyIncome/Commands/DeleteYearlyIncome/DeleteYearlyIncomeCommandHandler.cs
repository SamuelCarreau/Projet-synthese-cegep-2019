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
    public class DeleteYearlyIncomeCommandHandler : IRequestHandler<DeleteProfilOptionCommand<YearlyIncome>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteYearlyIncomeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<YearlyIncome> request, CancellationToken cancellationToken)
        {
            var yearlyIncome = await _context.FindAsync<YearlyIncome>(request.Id);

            yearlyIncome.IsDelete = true;

            _context.Update(yearlyIncome);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}