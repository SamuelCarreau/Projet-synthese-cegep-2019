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
    public class UpdateYearlyIncomeCommandHandler : IRequestHandler<UpdateProfilOptionCommand<YearlyIncome>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateYearlyIncomeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<YearlyIncome> request, CancellationToken cancellationToken)
        {
            var yearlyIncome = await _context.FindAsync<YearlyIncome>(request.Id);

            yearlyIncome.Name = request.Name;

            _context.Update(yearlyIncome);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}