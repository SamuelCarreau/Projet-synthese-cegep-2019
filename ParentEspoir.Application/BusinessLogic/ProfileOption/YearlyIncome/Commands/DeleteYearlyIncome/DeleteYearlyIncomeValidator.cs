using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application
{
    public class DeleteYearlyIncomeValidator : AbstractValidator<DeleteProfilOptionCommand<YearlyIncome>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteYearlyIncomeValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(y => y.Id)
                .Must(id => !IsLinked(id).Result)
                .WithMessage(DeleteProfilOptionCommand<YearlyIncome>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<YearlyIncome>()
                .Include(a => a.CustomerDescriptions)
                .ThenInclude(cd => cd.Customer)
                .SingleAsync(a => a.Id == id);

            return entity.CustomerDescriptions.Where(cd => cd.Customer.IsDelete == false).Count() > 0;
        }
    }
}
