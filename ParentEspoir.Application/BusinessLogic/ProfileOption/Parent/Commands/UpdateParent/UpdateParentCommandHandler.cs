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
    public class UpdateParentCommandHandler : IRequestHandler<UpdateProfilOptionCommand<Parent>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateParentCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<Parent> request, CancellationToken cancellationToken)
        {
            var parent = await _context.Parents.FindAsync(request.Id);

            parent.Name = request.Name;

            _context.Update(parent);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}