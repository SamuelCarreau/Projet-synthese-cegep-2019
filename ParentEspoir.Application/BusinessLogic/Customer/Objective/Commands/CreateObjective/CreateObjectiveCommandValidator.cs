using FluentValidation;
using System.Linq;
using ParentEspoir.Persistence;
using System;

namespace ParentEspoir.Application
{
    public class CreateObjectiveCommandValidator : AbstractValidator<CreateObjectiveCommand>
    {
        public CreateObjectiveCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(o => o.Code)
                .NotEmpty();

            RuleFor(o => o.CustomerId)
                .Must(id => context.Customers.Where(c => c.IsDelete == false && c.CustomerId == id)
                .SingleOrDefault() != null).WithMessage("L'id du client n'est pas valide");

            RuleFor(o => o.Name)
                .NotEmpty();

            RuleFor(o => o.NbHourDue)
                .GreaterThan(0.0);

            RuleFor(o => o.StartDate)
                .GreaterThanOrEqualTo(new DateTime(2000, 01, 01));

            RuleFor(o => o.WorkshopTypeId)
                .Must(id => context.WorkshopTypes
                .Where(c => c.IsDelete == false && c.Id == id)
                .SingleOrDefault() != null)
                .WithMessage("Vous devez choisir un volet");
        }
    }
}