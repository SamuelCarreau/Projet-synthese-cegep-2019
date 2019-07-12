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
    public class DeleteChildrenAgeBracketCommandHandler : IRequestHandler<DeleteProfilOptionCommand<ChildrenAgeBracket>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteChildrenAgeBracketCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<ChildrenAgeBracket> request, CancellationToken cancellationToken)
        {
            await request.Handle(_context, cancellationToken);

            return Unit.Value;
        }
    }
}