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
    public class DeleteSchoolingCommandValidator : AbstractValidator<DeleteProfilOptionCommand<Schooling>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteSchoolingCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(s => s.Id)
                .Must(id => !IsLinked(id).Result)
                .WithMessage(DeleteProfilOptionCommand<Schooling>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<Schooling>()
                  .Include(a => a.CustomerDescriptions)
                  .ThenInclude(cd => cd.Customer)
                  .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return entity.CustomerDescriptions.Where(c => c.Customer.IsDelete == false).Count() > 0;
        }
    }
}
