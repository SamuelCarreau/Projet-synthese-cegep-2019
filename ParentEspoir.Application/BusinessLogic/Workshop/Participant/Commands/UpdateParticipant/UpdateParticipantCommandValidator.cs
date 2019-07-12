using FluentValidation;
using FluentValidation.Validators;

namespace ParentEspoir.Application
{
    public class UpdateParticipantCommandValidator : AbstractValidator<UpdateParticipantCommand>
    {

        public UpdateParticipantCommandValidator()
        {
        }
    }
}