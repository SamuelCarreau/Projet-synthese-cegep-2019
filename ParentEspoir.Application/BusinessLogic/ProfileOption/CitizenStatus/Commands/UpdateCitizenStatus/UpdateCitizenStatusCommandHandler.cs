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
    public class UpdateCitizenStatusCommandHandler : IRequestHandler<UpdateProfilOptionCommand<CitizenStatus>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateCitizenStatusCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<CitizenStatus> request, CancellationToken cancellationToken)
        {
            var citizenStatus = await _context.Set<CitizenStatus>().FindAsync(request.Id);

            citizenStatus.Name = request.Name;

            _context.Update(citizenStatus);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}