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
    public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, GetNoteModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetNoteQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<GetNoteModel> Handle(GetNoteQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes
                .Include(n => n.NoteType)
                .SingleAsync(n => n.NoteId == request.NoteId && n.IsDelete == false);

            var model = new GetNoteModel    
            {
                NoteId = entity.NoteId,
                Body = entity.Body,
                CreationDate = entity.CreationDate,
                CustomerId = entity.CustomerId,
                NoteName = entity.NoteName,
                NoteTypeId = entity.NoteType?.Id,
                SupervisorName = entity.SupervisorName,
                SupervisorTitle = entity.SupervisorTitle
            };

            return model;
        }
    }
}