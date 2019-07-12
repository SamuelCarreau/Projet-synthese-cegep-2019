using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application
{
    public class DeleteVolonteeringTypeValidator : AbstractValidator<DeleteProfilOptionCommand<VolunteeringType>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteVolonteeringTypeValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(v => v.Id)
                .Must(id => !IsLinked(id).Result)
                .WithMessage(DeleteProfilOptionCommand<VolunteeringType>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<VolunteeringType>()
                .Include(a => a.Volunteerings)
                .SingleAsync(a => a.Id == id);

            return entity.Volunteerings.Where(v => v.IsDelete == false).Count() > 0;
        }
    }
}
