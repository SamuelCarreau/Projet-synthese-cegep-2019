using FluentValidation;
using System.Linq;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application
{
    public class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.Season).NotNull().WithMessage("La saison est requise").OverridePropertyName("Name");
            RuleFor(s => s.Year).NotNull().WithMessage("Le champs année est requis").OverridePropertyName("Name");

            RuleFor(s => s).Must(s => context.Sessions
            .Any(dbsession => dbsession.IsDelete == false && 
                              s.Season == dbsession.Season && 
                              s.Year == dbsession.Year) == false)
                              .WithMessage("Cette session existe déjà")
                              .OverridePropertyName("Name");

            RuleFor(s => s.Season)
                .Must(s => s.HasValue && (int)s.Value > 0 && (int)s.Value <= 4)
                .WithMessage("The value of the season is not valide")
                .OverridePropertyName("Name");

            RuleFor(s => s.Year)
                .Must(s => s.HasValue)
                .OverridePropertyName("Name");
        }
    }
}