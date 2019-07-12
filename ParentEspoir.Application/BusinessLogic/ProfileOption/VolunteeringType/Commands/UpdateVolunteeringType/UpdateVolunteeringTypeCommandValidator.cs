using FluentValidation;
using FluentValidation.Validators;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateVolunteeringTypeCommandValidator : AbstractValidator<UpdateProfilOptionCommand<VolunteeringType>>
    {

        public UpdateVolunteeringTypeCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(v => v)
                .Must(v => Normalize(context.VolunteeringTypes.Find(v.Id).Name) == Normalize(v.Name) || 
                           context.VolunteeringTypes.Where(vt => vt.IsDelete == false)
                           .Any(vv => Normalize(vv.Name) == Normalize(v.Name)) == false)
                           .WithMessage("Cette option existe déjà").OverridePropertyName("Name"); ;
        }

        private static string Normalize(string name)
        {
            return StringNormalizer.Normalize(name);
        }
    }
}