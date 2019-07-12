using FluentValidation;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class GetWorkshopValidator : AbstractValidator<GetWorkshopQuery>
    {
        public GetWorkshopValidator(ParentEspoirDbContext context)
        {
            RuleFor(q => q.WorkshopId)
                .Must(i => context.Workshops.Find(i) != null && context.Workshops.Find(i).IsDelete == false)
                .WithMessage("L'atelier n'est pas dans le système");
        }
    }
}
