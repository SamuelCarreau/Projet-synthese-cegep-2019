using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateHomeTypeCommandValidator : AbstractValidator<UpdateProfilOptionCommand<HomeType>>
    {
        public UpdateHomeTypeCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(a => a)
                .Must(n => (context.HomeTypes.SingleOrDefault(e => e.Id == n.Id && e.Name == n.Name) != null) ||
                string.IsNullOrWhiteSpace(n.Name) == false &&
                context.HomeTypes
                .Where(a => a.IsDelete == false)
                .Any(acontext => n.Name.ToUpper().Trim() == acontext.Name.ToUpper().Trim())
                == false).WithMessage("Cette option existe déjà").OverridePropertyName("Name"); ;
        }
    }
}