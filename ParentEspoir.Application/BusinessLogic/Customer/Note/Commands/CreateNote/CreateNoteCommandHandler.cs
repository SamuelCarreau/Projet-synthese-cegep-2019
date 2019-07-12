using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ParentEspoir.Application
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateNoteCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                Body = request.Body,
                CreationDate = DateTime.Now,
                CustomerId = request.CustomerId,
                NoteName = request.NoteName,
                SupervisorName = request.SupervisorName,
                SupervisorTitle = request.SupervisorTitle,
                NoteTypeId = request.NoteTypeId,
            };

            await _context.AddAsync(note);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}