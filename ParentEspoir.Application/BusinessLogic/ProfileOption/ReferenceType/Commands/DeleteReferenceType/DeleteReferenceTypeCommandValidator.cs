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
    public class DeleteReferenceTypeCommandValidator : AbstractValidator<DeleteProfilOptionCommand<ReferenceType>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteReferenceTypeCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(r => r.Id)
                .Must(id => !IsLinked(id).Result)
                .WithMessage(DeleteProfilOptionCommand<ReferenceType>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<ReferenceType>()
                 .Include(a => a.Customers)
                 .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return entity.Customers.Where(c => c.IsDelete == false).Count() > 0;
        }
    }
}
