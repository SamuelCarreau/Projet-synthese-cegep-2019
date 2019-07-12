using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class DeleteWorkshopCommandValidator : AbstractValidator<DeleteWorkshopCommand>
    {
        public DeleteWorkshopCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(w => w.WorkshopId)
                .Must(id => context.Workshops.Find(id) != null &&
                context.Workshops.Include(w => w.Seances)
                .Where(w => w.WorkshopId == id).Single()
                .Seances.Where(s => s.IsDelete == false).Any() == false)
                .WithMessage("Impossible de supprimer un atelier possédant des séances");
        }
    }
}
