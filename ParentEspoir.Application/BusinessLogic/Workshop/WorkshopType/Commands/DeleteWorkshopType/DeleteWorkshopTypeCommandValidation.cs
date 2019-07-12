using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class DeleteWorkshopTypeCommandValidation : AbstractValidator<DeleteWorkshopTypeCommand>
    {
        public DeleteWorkshopTypeCommandValidation(ParentEspoirDbContext context)
        {
            RuleFor(d => d.Id)
                .Must(id => context.WorkshopTypes.Find(id) != null &&
                context.WorkshopTypes.Include(w => w.Workshops).Single(w => w.Id == id)
                .Workshops.Where(w => w.IsDelete == false).Any() == false)
                .WithMessage("Impossible de supprimer un volet relier à des ateliers");
        }
    }
}
