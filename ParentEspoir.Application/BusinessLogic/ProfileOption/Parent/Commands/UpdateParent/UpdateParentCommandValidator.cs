using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateParentCommandValidator : AbstractValidator<UpdateProfilOptionCommand<Parent>>
    {
        public UpdateParentCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Le nomne peut pas être vide");

            RuleFor(a => a)
                .Must(n => (context.Parents.SingleOrDefault(e => e.Id == n.Id && e.Name == n.Name) != null) ||
                string.IsNullOrWhiteSpace(n.Name) == false &&
                context.Parents
                .Where(a => a.IsDelete == false)
                .Any(acontext => n.Name.ToUpper().Trim() == acontext.Name.ToUpper().Trim())
                == false).WithMessage("Cette option existe déjà").OverridePropertyName("Name"); ;
        }
    }
}