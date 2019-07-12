using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateChildrenAgeBracketCommandValidator : AbstractValidator<UpdateProfilOptionCommand<ChildrenAgeBracket>>
    {

        public UpdateChildrenAgeBracketCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(a => a.Name)
                .Must(name => string.IsNullOrWhiteSpace(name) == false)
                .WithMessage("Le nom ne peut pas être remplis d'espace");

            RuleFor(a => a)
                .Must(n => (context.ChildrenAgeBrackets.SingleOrDefault(e => e.Id == n.Id && e.Name == n.Name) != null) ||
                string.IsNullOrWhiteSpace(n.Name) == false &&
                context.ChildrenAgeBrackets
                .Where(a => a.IsDelete == false)
                .Any(c => n.Name.ToUpper().Trim() == c.Name.ToUpper().Trim())
                == false)
                .WithMessage("Cette option exite déjà")
                .OverridePropertyName("Name");
        }
    }
}