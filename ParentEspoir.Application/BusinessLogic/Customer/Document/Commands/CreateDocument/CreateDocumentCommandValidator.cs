using FluentValidation;
using FluentValidation.Validators;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
    {
        public CreateDocumentCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(d => d.CustomerId)
                .Must(id => context.Customers.Any(c => c.CustomerId == id && c.IsDelete == false))
                .WithMessage("L'id du costomer doit être dans la base de donnée");

            RuleFor(d => d.DocumentName)
                    .MaximumLength(DocumentConstant.NAME_MAX_LENGHT)
                    .NotEmpty()
                    .WithMessage($"Le nom du document ne doit pas être vide et conenir moins que {DocumentConstant.NAME_MAX_LENGHT} caractère");

            RuleFor(d => d.Description)
                .MaximumLength(DocumentConstant.DESCRIPTION_MAX_LENGHT)
                .WithMessage($"La description doit être plus courte que {DocumentConstant.DESCRIPTION_MAX_LENGHT} caractères");

        }
    }
}