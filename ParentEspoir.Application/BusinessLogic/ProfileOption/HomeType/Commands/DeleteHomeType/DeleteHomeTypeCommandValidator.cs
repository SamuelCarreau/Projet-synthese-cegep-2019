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
    public class DeleteHomeTypeCommandValidator : AbstractValidator<DeleteProfilOptionCommand<HomeType>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteHomeTypeCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(h => h.Id)
                .Must(id => IsLinked(id).Result == false)
                .WithMessage(DeleteProfilOptionCommand<HomeType>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<HomeType>()
               .Include(a => a.CustomerDescriptions)
               .ThenInclude(cd => cd.Customer)
               .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return entity.CustomerDescriptions.Where(c => c.Customer.IsDelete == false).Count() > 0;
        }
    }
}
