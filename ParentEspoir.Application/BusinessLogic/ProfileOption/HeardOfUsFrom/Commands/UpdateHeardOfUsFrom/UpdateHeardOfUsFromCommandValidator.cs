using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateHeardOfUsFromCommandValidator : AbstractValidator<UpdateProfilOptionCommand<HeardOfUsFrom>>
    {
        public UpdateHeardOfUsFromCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas �tre vide");

            RuleFor(a => a)
                .Must(n => (context.HeardOfUsFroms.SingleOrDefault(e => e.Id == n.Id && e.Name == n.Name) != null) ||
                string.IsNullOrWhiteSpace(n.Name) == false &&
                context.HeardOfUsFroms
                .Where(a => a.IsDelete == false)
                .Any(acontext => n.Name.ToUpper().Trim() == acontext.Name.ToUpper().Trim())
                == false).WithMessage("Cette option existe d�j�").OverridePropertyName("Name");
        }
    }
}