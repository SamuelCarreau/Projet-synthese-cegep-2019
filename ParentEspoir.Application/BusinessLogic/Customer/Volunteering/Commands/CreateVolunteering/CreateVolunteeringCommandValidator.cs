using FluentValidation;
using FluentValidation.Validators;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateVolunteeringCommandValidator : AbstractValidator<CreateVolunteeringCommand>
    {

        public CreateVolunteeringCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(v => v.CustomerId)
                .Must(x => context.Customers.Where(c => c.CustomerId == x && c.IsDelete == false).Any());

            RuleFor(v => v.Title)
                .NotEmpty()
                .WithMessage("Le titre est requis");

            RuleFor(v => v.Title)
                .Must(t => string.IsNullOrWhiteSpace(t) == false).WithMessage("Le titre ne peut être une chaîne vide")
                .MaximumLength(VolunteeringConstant.MAXTITLELENGHT).WithMessage($"Le titre ne peut excéder plus de {VolunteeringConstant.MAXTITLELENGHT} caractères");

            RuleFor(v => v.Amount)
                .Must(a => DecimalParser.CanParse(a) && DecimalParser.Parse(a) >= 0.0m)
                .WithMessage("La montant doit être au format valide et supérieur ou égal à zéro");

            RuleFor(v => v.HourCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Le nombre d'heure ne peut être négatif");

            RuleFor(c => c.VolunteeringTypeId).Must(x => x == null || (context.VolunteeringTypes.Where(c => c.Id == x && c.IsDelete == false).Any()));
        }
    }
}