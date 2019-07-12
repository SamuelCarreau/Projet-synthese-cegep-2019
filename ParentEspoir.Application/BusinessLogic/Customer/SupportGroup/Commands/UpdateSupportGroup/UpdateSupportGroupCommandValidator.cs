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
                .WithMessage($"Le nom doit avoir une longeur inf�rieur � {SupportGroupConstant.NAME_MAX_LENGHT} caract�res")
                .NotEmpty()
                .WithMessage("Le nom ne peut pas �tre vide.");

            RuleFor(s => s)
                .Must(n => context.SupportGroups
                .Any(sg => sg.SupportGroupId == n.SupportGroupId &&
                StringNormalizer.Normalize(sg.Name) == StringNormalizer.Normalize(n.Name))
                ||
                context.SupportGroups
                .Any(sg => StringNormalizer.Normalize(sg.Name) == StringNormalizer.Normalize(n.Name))
                == false)
                .WithMessage("Ce groupe de soutien existe d�j�");
        }
    }
}