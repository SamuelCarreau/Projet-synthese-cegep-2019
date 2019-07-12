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
    public class DeleteSocialServiceCommandValidator : AbstractValidator<DeleteProfilOptionCommand<SocialService>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteSocialServiceCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(s => s.Id)
                .Must(id => !IsLinked(id).Result)
                .WithMessage(DeleteProfilOptionCommand<SocialService>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<SocialService>()
                .Include(a => a.CustomerSocialServices)
                .ThenInclude(css => css.Customer)
                .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return entity.CustomerSocialServices.Where(c => c.Customer.Customer.IsDelete == false).Count() > 0;
        }
    }
}
