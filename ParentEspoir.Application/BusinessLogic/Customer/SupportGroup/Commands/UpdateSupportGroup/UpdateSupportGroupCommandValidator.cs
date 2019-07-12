using FluentValidation;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateSupportGroupCommandValidator : AbstractValidator<UpdateSupportGroupCommand>
    {
        public UpdateSupportGroupCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.Name)
                .MaximumLength(SupportGroupConstant.NAME_MAX_LENGHT)
                .WithMessage($"Le nom doit avoir une longeur inférieur à {SupportGroupConstant.NAME_MAX_LENGHT} caractères")
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide.");

            RuleFor(s => s)
                .Must(n => context.SupportGroups
                .Any(sg => sg.SupportGroupId == n.SupportGroupId &&
                StringNormalizer.Normalize(sg.Name) == StringNormalizer.Normalize(n.Name))
                ||
                context.SupportGroups
                .Any(sg => StringNormalizer.Normalize(sg.Name) == StringNormalizer.Normalize(n.Name))
                == false)
                .WithMessage("Ce groupe de soutien existe déjà");
        }
    }
}