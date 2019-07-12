using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateDocumentTypeCommandValidator : AbstractValidator<CreateProfilOptionCommand<DocumentType>>
    {

        public CreateDocumentTypeCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(d => d.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false &&
                context.DocumentTypes.Where(dt => dt.IsDelete == false)
                .Any(d => StringNormalizer.Normalize(d.Name) == StringNormalizer.Normalize(n)) == false)
                .WithMessage("Cette option existe déjà");

        }
    }
}