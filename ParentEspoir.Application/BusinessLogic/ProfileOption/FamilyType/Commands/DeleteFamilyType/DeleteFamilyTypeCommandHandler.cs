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
    public class DeleteFamilyTypeCommandHandler : IRequestHandler<DeleteProfilOptionCommand<FamilyType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteFamilyTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<FamilyType> request, CancellationToken cancellationToken)
        {
            var familyType = await _context.FamilyTypes.FindAsync(request.Id);

            familyType.IsDelete = true;

            _context.Update(familyType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}