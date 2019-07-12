using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateLanguageCommandValidator : AbstractValidator<UpdateProfilOptionCommand<Language>>
    {
        public UpdateLanguageCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(l => l.Name)
                .NotEmpty()
                .WithMessage("Le nom ne eput pas �tre vide");

            RuleFor(a => a)
                .Must(n => (context.Languages.SingleOrDefault(e => e.Id == n.Id && e.Name == n.Name) != null) ||
                string.IsNullOrWhiteSpace(n.Name) == false &&
                context.Languages
                .Where(a => a.IsDelete == false)
                .Any(acontext => n.Name.ToUpper().Trim() == acontext.Name.ToUpper().Trim())
                == false).WithMessage("Cette option existe d�j�").OverridePropertyName("Name");
        }
    }
}