using FluentValidation;
using FluentValidation.Validators;

namespace ParentEspoir.Application
{
    public class CreatePersonnalFollowUpCommandValidator : AbstractValidator<CreatePersonnalFollowUpCommand>
    {

        public CreatePersonnalFollowUpCommandValidator()
        {
        }
    }
}