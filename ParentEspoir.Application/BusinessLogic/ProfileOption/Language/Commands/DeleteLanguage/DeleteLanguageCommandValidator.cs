using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteLanguageCommandValidator : AbstractValidator<DeleteProfilOptionCommand<Language>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteLanguageCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(l => l.Id)
                .Must(id => !IsLinked(id).Result)
                .WithMessage(DeleteProfilOptionCommand<Language>.IS_LINKED_ERROR_MESSAGE);
        }

        public async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<Language>()
                .Include(a => a.CustomerDescriptions)
                .ThenInclude(cd => cd.Customer)
                .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return entity.CustomerDescriptions.Where(c => c.Customer.IsDelete == false).Count() > 0;
        }
    }
}
