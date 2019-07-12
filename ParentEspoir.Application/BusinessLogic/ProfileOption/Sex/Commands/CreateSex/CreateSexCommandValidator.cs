using FluentValidation;
using ParentEspoir.Domain.Constants;
using FluentValidation.Validators;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateSexCommandValidator : AbstractValidator<CreateProfilOptionCommand<Sex>>
    {
        public CreateSexCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas �tre vide");

            RuleFor(a => a.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false &&
                context.Sexs
                .Where(a => a.IsDelete == false)
                .Any(acontext => n.ToUpper().Trim() == acontext.Name.ToUpper().Trim())
                == false).WithMessage("Cette option existe d�j�");
        }
    }
}