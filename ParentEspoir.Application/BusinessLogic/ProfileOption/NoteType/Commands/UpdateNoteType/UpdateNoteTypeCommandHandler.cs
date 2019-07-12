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
    public class UpdateNoteTypeCommandHandler : IRequestHandler<UpdateProfilOptionCommand<NoteType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateNoteTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<NoteType> request, CancellationToken cancellationToken)
        {
            var noteType = await _context.NoteTypes.FindAsync(request.Id);

            noteType.Name = request.Name;

            _context.Update(noteType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}