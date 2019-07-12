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
    public class DeleteReferenceTypeCommandHandler : IRequestHandler<DeleteProfilOptionCommand<ReferenceType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteReferenceTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<ReferenceType> request, CancellationToken cancellationToken)
        {
            var referenceType = await _context.ReferenceTypes.FindAsync(request.Id);

            referenceType.IsDelete = true;

            _context.Update(referenceType);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}