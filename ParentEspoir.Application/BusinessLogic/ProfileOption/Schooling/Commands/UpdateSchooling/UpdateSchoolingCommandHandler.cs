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
    public class UpdateSchoolingCommandHandler : IRequestHandler<UpdateProfilOptionCommand<Schooling>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateSchoolingCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<Schooling> request, CancellationToken cancellationToken)
        {
            var schooling = await _context.Schoolings.FindAsync(request.Id);

            schooling.Name = request.Name;

            _context.Update(schooling);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}