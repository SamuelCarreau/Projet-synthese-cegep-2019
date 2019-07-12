using FluentValidation;
using FluentValidation.Validators;

namespace ParentEspoir.Application
{
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {

        public CreateMemberCommandValidator()
        {
        }
    }
}