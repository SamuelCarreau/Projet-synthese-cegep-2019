using FluentValidation;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application
{
    public class CreateWorkshopCommandValidator : AbstractValidator<CreateWorkshopCommand>
    {
        public CreateWorkshopCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.SessionId)
                .Must(id => context.Sessions.Find(id) != null)
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

            RuleFor(s => s.WorkshopName)
                .Must(wn => !string.IsNullOrWhiteSpace(wn))
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

            RuleFor(s => s.WorkshopTypeId)
                .Must(wn => wn != null)
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

            RuleFor(s => s.StartDate)
                .Must(sd => sd != null)
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

            RuleFor(s => s.EndDate)
                .Must(ed => ed != null)
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

            RuleFor(s => s.IsOpen)
                .Must(io => io != null)
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

            // if startDate is not null then it must be equal or after the startDate of session
            RuleFor(s => new { s.StartDate, s.SessionId })
                .Must(sd => sd.StartDate == null || sd.StartDate >= (context.Sessions.Find(sd.SessionId).StartDate))
                .WithMessage("La date de début ne peut être avant la date de début de la session")
                .OverridePropertyName("StartDate");

            // if startDate and endDate is not null, then startDate must be after endDate
            RuleFor(s => new { s.StartDate, s.EndDate })
                .Must(sd => (sd.StartDate == null || sd.EndDate == null) || sd.StartDate < sd.EndDate)
                .WithMessage("La date de début ne peut pas être après la date de fin.")
                .OverridePropertyName("EndDate");

            RuleFor(s => s.WorkshopDescription)
                .Must(wd => !string.IsNullOrWhiteSpace(wd))
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE);

            RuleFor(s => s.SeanceCount)
                .Must(sc => sc == null || sc >= 0)
                .WithMessage("Le nombre de séance ne peut pas être négatif.");

            // if SeanceCount is null, then SeanceLenght must not be null
            RuleFor(s => new { s.SeanceCount, s.SeanceLenght })
                .Must(s => s.SeanceCount == null || s.SeanceLenght != null)
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE)
                .OverridePropertyName("SeanceLenght");

            // if SeanceCount is null, then DateTimeFirstSeance must not be null
            RuleFor(s => new { s.SeanceCount, s.DateTimeFirstSeance })
                .Must(s => s.SeanceCount == null || s.DateTimeFirstSeance != null)
                .WithMessage(ValidationConstants.REQUIRED_FIELD_MESSAGE)
                .OverridePropertyName("DateTimeFirstSeance");

            // if SeanceCount is null, then DateTimeFirstSeance must be equal or after StartDate
            RuleFor(s => new { s.SeanceCount, s.DateTimeFirstSeance, s.StartDate })
                .Must(s => s.SeanceCount == null || s.DateTimeFirstSeance >= s.StartDate)
                .WithMessage("La date de la première séance doit être la même ou après la date de début de l'Atelier.")
                .OverridePropertyName("DateTimeFirstSeance");
        }
    }
}