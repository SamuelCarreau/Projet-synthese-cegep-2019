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
    public class DeleteCitizenStatusCommandHandler : IRequestHandler<DeleteProfilOptionCommand<CitizenStatus>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteCitizenStatusCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<CitizenStatus> request, CancellationToken cancellationToken)
        {
            var citizenStatus = await _context.Set<CitizenStatus>().FindAsync(request.Id);

            citizenStatus.IsDelete = true;

            _context.Update(citizenStatus);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}