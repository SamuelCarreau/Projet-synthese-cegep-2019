using FluentValidation;
using System.Linq;
using ParentEspoir.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class CreateParticipantCommandValidator : AbstractValidator<CreateParticipantCommand>
    {

        public CreateParticipantCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.WorkshopId)
             .MustAsync(async (id, cancellation) => await context.Workshops.FindAsync(id) != null)
             .WithMessage("L'atelier n'est pas valide.");

            RuleFor(s => s.CustomerId)
             .MustAsync(async (id, cancellation) => await context.Customers.FindAsync(id) != null)
             .WithMessage("Le client n'est pas valide.");

            // Here we check that the customer is not present in the workshop
            RuleFor(cp => new { cp.CustomerId, cp.WorkshopId })
                .MustAsync(async (x, cancellation) => await context
                            .Participants
                            .Where(p => p.WorkshopId == x.WorkshopId && p.CustomerId == x.CustomerId && p.IsDelete == false)
                            .AnyAsync() == false)
                .WithMessage("Ce client est déjà présent dans cette atelier")
                .OverridePropertyName("CustomerId");
        }
    }
}