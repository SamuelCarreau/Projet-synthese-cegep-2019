using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateDocumentTypeCommandValidator : AbstractValidator<UpdateProfilOptionCommand<DocumentType>>
    {
        public UpdateDocumentTypeCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(d => d)
                .Must(entity =>
                string.IsNullOrWhiteSpace(entity.Name) == false &&
                (context.DocumentTypes
                .Where(dt => dt.Id == entity.Id && dt.Name == entity.Name)
                .Any() ||
                context.DocumentTypes
                .Where(dt => dt.IsDelete == false)
                .Any(dt => StringNormalizer.Normalize(dt.Name) == StringNormalizer.Normalize(entity.Name)) == false))
                .OverridePropertyName("Name");

        }
    }
}