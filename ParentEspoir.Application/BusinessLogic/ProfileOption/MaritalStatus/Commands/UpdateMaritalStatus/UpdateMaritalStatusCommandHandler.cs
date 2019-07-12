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
    public class UpdateMaritalStatusCommandHandler : IRequestHandler<UpdateProfilOptionCommand<MaritalStatus>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateMaritalStatusCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<MaritalStatus> request, CancellationToken cancellationToken)
        {
            var maritalStatus = await _context.MaritalStatuses.FindAsync(request.Id);

            maritalStatus.Name = request.Name;

            _context.Update(maritalStatus);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}