using MediatR;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteDocumentTypeCommandHandler : IRequestHandler<DeleteProfilOptionCommand<DocumentType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteDocumentTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<DocumentType> request, CancellationToken cancellationToken)
        {
            var documentType = await _context.DocumentTypes.FindAsync(request.Id);

            documentType.IsDelete = true;

            _context.Update(documentType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}