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
    public class DeleteChildrenAgeBracketCommandValidator : AbstractValidator<DeleteProfilOptionCommand<ChildrenAgeBracket>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteChildrenAgeBracketCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(c => c.Id)
                .Must(id => !IsLinked(id).Result)
                .WithMessage(DeleteProfilOptionCommand<ChildrenAgeBracket>.IS_LINKED_ERROR_MESSAGE)
                .OverridePropertyName("Name");
        }

        public async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<ChildrenAgeBracket>()
                .Include(a => a.CustomerChildrenAgeBrackets)
                .ThenInclude(ccab => ccab.Customer)
                .ThenInclude(cd => cd.Customer)
                .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return entity.CustomerChildrenAgeBrackets.Any(c => c.Customer.Customer.IsDelete == false);
        }
    }
}
