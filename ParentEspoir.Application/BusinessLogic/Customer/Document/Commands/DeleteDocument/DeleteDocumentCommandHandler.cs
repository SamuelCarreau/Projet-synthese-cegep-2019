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
    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteDocumentCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _context.FindAsync<Document>(request.DocumentId);

            document.IsDelete = true;

            _context.Update(document);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}