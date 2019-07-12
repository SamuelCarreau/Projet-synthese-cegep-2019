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
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateDocumentCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _context.Documents
                .Include(d => d.DocumentType)
                .Where(d => d.DocumentId == request.DocumentId)
                .SingleAsync();

            document.DocumentName = request.DocumentName;
            document.Description = request.Description;
            document.DocumentType = await _context.DocumentTypes.FindAsync(request.DocumentTypeId);
            
            _context.Update(document);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}