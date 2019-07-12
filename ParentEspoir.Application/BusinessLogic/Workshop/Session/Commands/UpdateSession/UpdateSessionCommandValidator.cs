using FluentValidation;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
    {
        public UpdateSessionCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.SessionId).Must(i => context.Sessions.Find(i) != null);

            RuleFor(s => s.Season).NotNull().WithMessage("Le champs est requis");
            RuleFor(s => s.Year).NotNull().WithMessage("Le champs est requis");

            RuleFor(s => s).Must(s => context.Sessions
            .Any(dbsession => dbsession.IsDelete == false && s.Season == dbsession.Season && s.Year == dbsession.Year && dbsession.SessionId != s.SessionId) == false);

            RuleFor(s => s.Season).Must(s => s.HasValue);
            RuleFor(s => s.Year).Must(s => s.HasValue);
        }
    }
}