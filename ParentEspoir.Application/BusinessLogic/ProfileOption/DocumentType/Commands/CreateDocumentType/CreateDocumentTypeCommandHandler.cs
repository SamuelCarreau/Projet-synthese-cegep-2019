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
    public class CreateDocumentTypeCommandHandler : IRequestHandler<CreateProfilOptionCommand<DocumentType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateDocumentTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<DocumentType> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new DocumentType { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}