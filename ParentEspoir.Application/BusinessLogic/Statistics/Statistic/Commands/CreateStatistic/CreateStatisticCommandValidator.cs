using FluentValidation;
using FluentValidation.Validators;

namespace ParentEspoir.Application
{
    public class CreateStatisticCommandValidator : AbstractValidator<CreateStatisticCommand>
    {

        public CreateStatisticCommandValidator()
        {
        }
    }
}