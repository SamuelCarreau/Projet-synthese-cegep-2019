using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateYearlyIncomeCommandValidator : AbstractValidator<CreateProfilOptionCommand<YearlyIncome>>
    {
        public CreateYearlyIncomeCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(a => a.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false &&
                context.YearlyIncomes
                .Where(c => c.IsDelete == false)
                .Any(c => n.ToUpper().Trim() == c.Name.ToUpper().Trim())
                == false).WithMessage("Cette option existe déjà");
        }
    }
}