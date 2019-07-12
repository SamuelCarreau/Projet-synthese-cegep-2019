using FluentValidation;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class GetSeanceValidator : AbstractValidator<GetSeanceQuery>
    {
        public GetSeanceValidator(ParentEspoirDbContext context)
        {
            RuleFor(s => s.SeanceId).Must(i => context.Seances.Find(i) != null && context.Seances.Find(i).IsDelete == false);
        }
    }
}
