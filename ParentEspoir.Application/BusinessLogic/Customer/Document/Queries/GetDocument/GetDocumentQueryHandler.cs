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
    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, GetDocumentModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetDocumentQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<GetDocumentModel> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            var doc = await _context.Documents.Include(d => d.DocumentType).Where(d => d.IsDelete == false && d.DocumentId == request.DocumentId).SingleAsync();

            var model = new GetDocumentModel
            {
                DocumentId = doc.DocumentId,
                Name = doc.DocumentName,
                FileUrl = doc.Path,
                Description = doc.Description,
                DocumentTypeId = doc.DocumentType?.Id,
                DocumentTypeName = doc.DocumentType != null ? doc.DocumentType.Name : "Aucun",
                CustomerId = doc.CustomerId
            };

            return model;
        }
    }
}