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
    public class UpdateLegalCustodyCommandHandler : IRequestHandler<UpdateProfilOptionCommand<LegalCustody>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateLegalCustodyCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<LegalCustody> request, CancellationToken cancellationToken)
        {
            var legalCustody = await _context.LegalCustodies.FindAsync(request.Id);

            legalCustody.Name = request.Name;

            _context.Update(legalCustody);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}