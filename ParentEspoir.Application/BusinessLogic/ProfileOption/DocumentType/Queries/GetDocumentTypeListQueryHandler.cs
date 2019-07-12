using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class GetDocumentTypeListQueryHandler : IRequestHandler<GetProfilOptionQuery<DocumentType>, IEnumerable<IProfileOption>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetDocumentTypeListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<IEnumerable<IProfileOption>> Handle(GetProfilOptionQuery<DocumentType> request, CancellationToken cancellationToken)
        {
            return await request.Handle(_memory, _context);
        }
    }
}