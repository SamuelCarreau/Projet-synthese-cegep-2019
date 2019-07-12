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
    public class CreateNoteTypeCommandHandler : IRequestHandler<CreateProfilOptionCommand<NoteType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateNoteTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<NoteType> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new NoteType { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}