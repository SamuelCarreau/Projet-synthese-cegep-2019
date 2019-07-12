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
    public class GetDocumentListQueryHandler : IRequestHandler<GetDocumentListQuery, IEnumerable<Document>>
    {
        private readonly ParentEspoirDbContext _context;

        public GetDocumentListQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> Handle(GetDocumentListQuery request, CancellationToken cancellationToken)
        {
            var documents = await _context.Documents
                .Include(d => d.DocumentType)
                .Where(d => d.IsDelete == false && d.CustomerId == request.CustomerId)
                .ToArrayAsync();

            return documents;
        }
    }
}