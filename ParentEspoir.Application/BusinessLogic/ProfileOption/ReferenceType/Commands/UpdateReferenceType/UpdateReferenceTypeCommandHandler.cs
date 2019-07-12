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
    public class UpdateReferenceTypeCommandHandler : IRequestHandler<UpdateProfilOptionCommand<ReferenceType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateReferenceTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<ReferenceType> request, CancellationToken cancellationToken)
        {
            var referenceType = await _context.ReferenceTypes.FindAsync(request.Id);

            referenceType.Name = request.Name;

            _context.Update(referenceType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}