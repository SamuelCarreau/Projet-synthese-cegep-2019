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
    public class DeleteSexCommandHandler : IRequestHandler<DeleteProfilOptionCommand<Sex>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteSexCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<Sex> request, CancellationToken cancellationToken)
        {
            var sexToDelete = await _context.Sexs.FindAsync(request.Id);
            sexToDelete.IsDelete = true;
            _context.Update(sexToDelete);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}