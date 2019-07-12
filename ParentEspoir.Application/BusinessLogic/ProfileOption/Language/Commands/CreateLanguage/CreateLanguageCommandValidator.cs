using FluentValidation;
using System.Linq;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application
{
    public class CreateLanguageCommandValidator : AbstractValidator<CreateProfilOptionCommand<Language>>
    {

        public CreateLanguageCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(a => a.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false &&
                context.Languages
                .Where(a => a.IsDelete == false)
                .Any(acontext => StringNormalizer.Normalize(n) == StringNormalizer.Normalize(acontext.Name))
                == false)
                .WithMessage("Cette option existe déjà");
        }
    }
}