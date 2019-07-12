using FluentValidation;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateSupportGroupCommandValidator : AbstractValidator<CreateSupportGroupCommand>
    {

        public CreateSupportGroupCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.Name)
                .MaximumLength(SupportGroupConstant.NAME_MAX_LENGHT)
                .WithMessage($"Le nom doit avoir une longeur inférieur à {SupportGroupConstant.NAME_MAX_LENGHT} caractères")
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide.")
                .Must(n => string.IsNullOrWhiteSpace(n) == false)
                .WithMessage("La chaine ne peut pas être vide ou remplis d'espace");

            RuleFor(s => s.Name)
                .Must(n => context.SupportGroups
                .Any(s => StringNormalizer.Normalize(s.Name) == StringNormalizer.Normalize(n) 
                && s.IsDelete == false)
                == false)
                .WithMessage("Ce groupe de soutien existe déjà");
        }
    }
}