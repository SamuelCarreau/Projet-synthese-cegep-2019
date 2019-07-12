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
    public class DeleteParentCommandHandler : IRequestHandler<DeleteProfilOptionCommand<Parent>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteParentCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<Parent> request, CancellationToken cancellationToken)
        {
            var parent = await _context.Parents.FindAsync(request.Id);

            parent.IsDelete = true;

            _context.Update(parent);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}