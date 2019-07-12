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
    public class GetChildrenAgeBracketListQueryHandler : IRequestHandler<GetProfilOptionQuery<ChildrenAgeBracket>, System.Collections.Generic.IEnumerable<IProfileOption>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetChildrenAgeBracketListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<System.Collections.Generic.IEnumerable<IProfileOption>> Handle(GetProfilOptionQuery<ChildrenAgeBracket> request, CancellationToken cancellationToken)
        {
            return await request.Handle(_memory, _context);
        }
    }
}