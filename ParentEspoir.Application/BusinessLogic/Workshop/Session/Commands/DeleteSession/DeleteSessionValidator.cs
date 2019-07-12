using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class DeleteSessionValidator : AbstractValidator<DeleteSessionCommand>
    {
        public DeleteSessionValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.SessionId)
                .Must(id => context.Sessions.Find(id) != null)
                .WithMessage("La session n'existe pas");
            
            RuleFor(s => s.SessionId)
                .Must(id => context.Workshops
                .Any(w => w.SessionId == id && w.IsDelete == false) == false)
                .WithMessage("La session possède des ateliers et ne peut pas être supprimée");
        }
    }
}
