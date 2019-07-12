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
    public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, IEnumerable<Note>>
    {
        private readonly ParentEspoirDbContext _context;

        public GetNoteListQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Notes
                .Include(n => n.NoteType)
                .Where(n => n.CustomerId == request.CustomerId && n.IsDelete == false)
                .ToArrayAsync();
        }
    }
}