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
    public class DeleteLegalCustodyCommandHandler : IRequestHandler<DeleteProfilOptionCommand<LegalCustody>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteLegalCustodyCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<LegalCustody> request, CancellationToken cancellationToken)
        {
            var legalCustodies = await _context.LegalCustodies.FindAsync(request.Id);

            legalCustodies.IsDelete = true;

            _context.Update(legalCustodies);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}