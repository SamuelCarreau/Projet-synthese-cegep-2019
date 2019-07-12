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
    public class UpdateFamilyTypeCommandHandler : IRequestHandler<UpdateProfilOptionCommand<FamilyType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateFamilyTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<FamilyType> request, CancellationToken cancellationToken)
        {
            var familyType = await _context.FamilyTypes.FindAsync(request.Id);

            familyType.Name = request.Name;

            _context.Update(familyType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}