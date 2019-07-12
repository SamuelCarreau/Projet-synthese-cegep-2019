using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteCitizenStatusCommandValidator : AbstractValidator<DeleteProfilOptionCommand<CitizenStatus>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteCitizenStatusCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(d => d.Id)
                .Must(id => IsLinked(id).Result == false)
                .WithMessage(DeleteProfilOptionCommand<CitizenStatus>.IS_LINKED_ERROR_MESSAGE)
                .OverridePropertyName("Name");
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<CitizenStatus>()
               .Include(a => a.CustomerDescriptions)
               .ThenInclude(cd => cd.Customer)
               .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return entity.CustomerDescriptions.Where(c => c.Customer.IsDelete == false).Count() > 0;
        }
    }
}
