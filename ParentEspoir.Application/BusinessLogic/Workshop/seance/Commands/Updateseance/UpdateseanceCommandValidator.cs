using FluentValidation;
using FluentValidation.Validators;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application
{
    public class UpdateSeanceCommandValidator : AbstractValidator<UpdateSeanceCommand>
    {
        public UpdateSeanceCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.WorkshopId)
               .Must(id => context.Workshops.Find(id) != null)
               .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

            When(x => (context.Workshops.Find(x.WorkshopId) != null), () =>
            {
                RuleFor(s => s.SeanceName)
                .Must(sn => !string.IsNullOrWhiteSpace(sn))
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

                RuleFor(s => s.SeanceDate)
                    .Must(sd => sd != null)
                    .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

                RuleFor(s => new { s.SeanceDate, workshop = context.Workshops.Find(s.WorkshopId) })
                    .Must(x => x.SeanceDate == null || (x.SeanceDate >= x.workshop.StartDate && x.SeanceDate <= x.workshop.EndDate))
                    .WithMessage($@"La date de la séance doit être comprise entre les dates de l'atelier")
                    .OverridePropertyName("SeanceDate");

                RuleFor(s => s.SeanceTimeSpan)
                    .Must(st => st != null)
                    .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

                RuleFor(s => s.SeanceDescription)
                    .Must(sd => !string.IsNullOrWhiteSpace(sd))
                    .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);
            });
        }
    }
}