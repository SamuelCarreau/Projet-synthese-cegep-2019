using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteDocumentTypeCommandValidator : AbstractValidator<DeleteProfilOptionCommand<DocumentType>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteDocumentTypeCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(d => d.Id)
                .Must(id => IsLinked(id).Result == false)
                .WithMessage(DeleteProfilOptionCommand<DocumentType>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var type = await _context.DocumentTypes
               .Include(a => a.Documents)
               .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return type.Documents.Where(d => d.IsDelete == false).Count() > 0;
        }
    }
}
