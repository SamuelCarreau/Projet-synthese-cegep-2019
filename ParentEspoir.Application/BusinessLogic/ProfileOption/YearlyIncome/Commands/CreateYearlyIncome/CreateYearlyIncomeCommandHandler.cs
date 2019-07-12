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
    public class CreateYearlyIncomeCommandHandler : IRequestHandler<CreateProfilOptionCommand<YearlyIncome>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateYearlyIncomeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<YearlyIncome> request, CancellationToken cancellationToken)
        {
            _context.Add(new YearlyIncome { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}