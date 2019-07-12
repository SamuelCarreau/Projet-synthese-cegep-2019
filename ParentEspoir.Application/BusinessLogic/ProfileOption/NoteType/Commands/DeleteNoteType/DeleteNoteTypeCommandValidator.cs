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
    public class DeleteNoteTypeCommandValidator : AbstractValidator<DeleteProfilOptionCommand<NoteType>>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteNoteTypeCommandValidator(ParentEspoirDbContext context)
        {
            _context = context;

            RuleFor(n => n.Id)
                .Must(id => !IsLinked(id).Result)
                .WithMessage(DeleteProfilOptionCommand<NoteType>.IS_LINKED_ERROR_MESSAGE);
        }

        private async Task<bool> IsLinked(int id)
        {
            var entity = await _context.Set<NoteType>()
                .Include(a => a.Notes)
                .ThenInclude(n => n.NoteType)
                .SingleAsync(a => a.Id == id && a.IsDelete == false);

            return entity.Notes.Where(d => d.IsDelete == false).Count() > 0;
        }
    }
}
