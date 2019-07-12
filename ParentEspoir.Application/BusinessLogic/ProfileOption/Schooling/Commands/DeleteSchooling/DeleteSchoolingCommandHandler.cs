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
    public class DeleteSchoolingCommandHandler : IRequestHandler<DeleteProfilOptionCommand<Schooling>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteSchoolingCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<Schooling> request, CancellationToken cancellationToken)
        {
            var schooling = await _context.Schoolings.FindAsync(request.Id);

            schooling.IsDelete = true;

            _context.Update(schooling);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}