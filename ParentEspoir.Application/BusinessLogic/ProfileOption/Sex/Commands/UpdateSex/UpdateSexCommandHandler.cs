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
    public class UpdateSexCommandHandler : IRequestHandler<UpdateProfilOptionCommand<Sex>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateSexCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<Sex> request, CancellationToken cancellationToken)
        {
            var sex = await _context.FindAsync<Sex>(request.Id);

            sex.Name = request.Name;

            _context.Update(sex);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}