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
    public class UpdateDocumentTypeCommandHandler : IRequestHandler<UpdateProfilOptionCommand<DocumentType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateDocumentTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<DocumentType> request, CancellationToken cancellationToken)
        {
            var documentType = await _context.DocumentTypes.FindAsync(request.Id);

            documentType.Name = request.Name;

            _context.Update(documentType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}