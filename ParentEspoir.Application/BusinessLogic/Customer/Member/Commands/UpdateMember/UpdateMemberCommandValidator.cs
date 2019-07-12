using FluentValidation;
using FluentValidation.Validators;

namespace ParentEspoir.Application
{
    public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
    {

        public UpdateMemberCommandValidator()
        {
        }
    }
}