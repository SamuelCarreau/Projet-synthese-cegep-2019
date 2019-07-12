using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateChildrenAgeBracketCommandValidator : AbstractValidator<CreateProfilOptionCommand<ChildrenAgeBracket>>
    {
        public CreateChildrenAgeBracketCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas �tre vide");

            RuleFor(a => a.Name)
                .Must(name => string.IsNullOrWhiteSpace(name) == false)
                .WithMessage("Le nom ne peut pas �tre remplis d'espace");

            RuleFor(a => a.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false &&
                context.ChildrenAgeBrackets
                .Where(c => c.IsDelete == false)
                .Any(c => n.ToUpper().Trim() == c.Name.ToUpper().Trim())
                == false).WithMessage("Cette option existe d�j�");
        }
    }
}