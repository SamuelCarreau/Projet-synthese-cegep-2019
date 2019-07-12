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
    public class DeleteMaritalStatusCommandHandler : IRequestHandler<DeleteProfilOptionCommand<MaritalStatus>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteMaritalStatusCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<MaritalStatus> request, CancellationToken cancellationToken)
        {
            var maritalStatus = await _context.MaritalStatuses.FindAsync(request.Id);

            maritalStatus.IsDelete = true;

            _context.Update(maritalStatus);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}