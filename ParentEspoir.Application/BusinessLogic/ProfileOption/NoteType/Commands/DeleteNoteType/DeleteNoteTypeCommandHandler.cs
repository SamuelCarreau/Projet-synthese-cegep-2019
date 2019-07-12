using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ParentEspoir.Application
{
    public class DeleteNoteTypeCommandHandler : IRequestHandler<DeleteProfilOptionCommand<NoteType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteNoteTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<NoteType> request, CancellationToken cancellationToken)
        {
            var noteType = await _context.NoteTypes.FindAsync(request.Id);

            noteType.IsDelete = true;

            _context.Update(noteType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}