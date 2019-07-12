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
    public class UpdateChildrenAgeBracketCommandHandler : IRequestHandler<UpdateProfilOptionCommand<ChildrenAgeBracket>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateChildrenAgeBracketCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<ChildrenAgeBracket> request, CancellationToken cancellationToken)
        {
            var childrenAgeBracked = await _context.FindAsync<ChildrenAgeBracket>(request.Id);

            childrenAgeBracked.Name = request.Name;

            _context.Update(childrenAgeBracked);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}