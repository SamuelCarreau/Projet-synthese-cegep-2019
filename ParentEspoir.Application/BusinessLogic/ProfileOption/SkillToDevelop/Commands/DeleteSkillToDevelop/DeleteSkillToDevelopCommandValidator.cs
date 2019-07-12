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
    public class DeleteSkillToDevelopCommandValidator : AbstractValidator<DeleteProfilOptionCommand<SkillToDevelop>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteSkillToDevelopCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(s => s.Id)
                .Must(id => !IsLinked(id).Result)
                .WithMessage(DeleteProfilOptionCommand<SkillToDevelop>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<SkillToDevelop>()
                .Include(a => a.CustomerSkillToDevelops)
                .ThenInclude(cstd => cstd.Customer)
                .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return entity.CustomerSkillToDevelops.Where(c => c.Customer.Customer.IsDelete == false).Count() > 0;
        }
    }
}
