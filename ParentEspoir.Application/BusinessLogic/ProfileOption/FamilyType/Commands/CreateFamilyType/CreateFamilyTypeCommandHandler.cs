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
    public class CreateFamilyTypeCommandHandler : IRequestHandler<CreateProfilOptionCommand<FamilyType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateFamilyTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<FamilyType> request, CancellationToken cancellationToken)
        {
            var familyType = new FamilyType { Name = request.Name };
            _context.FamilyTypes.Add(familyType);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}