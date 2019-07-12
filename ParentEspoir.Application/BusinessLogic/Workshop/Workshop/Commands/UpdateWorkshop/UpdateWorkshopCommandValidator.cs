using FluentValidation;
using FluentValidation.Validators;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application
{
    public class UpdateWorkshopCommandValidator : AbstractValidator<UpdateWorkshopCommand>
    {
        public UpdateWorkshopCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.WorkshopId)
               .Must(id => context.Workshops.Find(id) != null)
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
        }
    }
}