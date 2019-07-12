using FluentValidation;
using FluentValidation.Validators;
using ParentEspoir.Domain.Constants;

namespace ParentEspoir.Application
{
    public class UpdateDocumentCommandValidator : AbstractValidator<UpdateDocumentCommand>
    {
        public UpdateDocumentCommandValidator()
        {
            RuleFor(d => d.Description).MaximumLength(DocumentConstant.DESCRIPTION_MAX_LENGHT);
            RuleFor(d => d.DocumentName).MaximumLength(DocumentConstant.NAME_MAX_LENGHT).NotEmpty();
        }
    }
}