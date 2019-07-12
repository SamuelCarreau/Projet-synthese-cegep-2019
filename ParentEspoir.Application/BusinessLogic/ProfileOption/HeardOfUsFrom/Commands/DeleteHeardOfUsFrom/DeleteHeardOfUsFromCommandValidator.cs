using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteHeardOfUsFromCommandValidator : AbstractValidator<DeleteProfilOptionCommand<HeardOfUsFrom>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteHeardOfUsFromCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(h => h.Id)
                .Must(id => IsLinked(id).Result == false)
                .WithMessage(DeleteProfilOptionCommand<HeardOfUsFrom>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<HeardOfUsFrom>()
               .Include(hof => hof.Customers)
               .SingleAsync(hof => hof.Id == id && hof.IsDelete == false);

            return entity.Customers.Where(c => c.IsDelete == false).Count() > 0;
        }
    }
}
