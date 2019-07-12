using FluentValidation;
using FluentValidation.Validators;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateVolunteeringCommandValidator : AbstractValidator<UpdateVolunteeringCommand>
    {

        public UpdateVolunteeringCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(v => v.CustomerId).Must(x => context.Customers.Where(c => c.CustomerId == x && c.IsDelete == false).Any());
            RuleFor(v => v.VolunteeringId).Must(x => context.Volunteerings.Where(c => c.VolunteeringId == x && c.IsDelete == false).Any());

            RuleFor(v => v.VolunteeringTypeId).Must(x => x == null || (context.Volunteerings.Where(c => c.VolonteeringTypeId == x).Any()));

            RuleFor(v => v.Title)
                .Must(t => string.IsNullOrWhiteSpace(t) == false).WithMessage("Le titre ne peut �tre une cha�ne vide")
                .MaximumLength(VolunteeringConstant.MAXTITLELENGHT).WithMessage($"Le titre ne peut exc�der plus de {VolunteeringConstant.MAXTITLELENGHT} caract�res");

            RuleFor(v => v.Amount)
                .Must(a => DecimalParser.CanParse(a) && DecimalParser.Parse(a) >= 0.0m)
                .WithMessage("La montant doit �tre au format valide et sup�rieur ou �gal � z�ro");

            RuleFor(v => v.HourCount).GreaterThanOrEqualTo(0)
                .WithMessage("Le nombre d'heure ne peut �tre n�gatif");
        }
    }
}