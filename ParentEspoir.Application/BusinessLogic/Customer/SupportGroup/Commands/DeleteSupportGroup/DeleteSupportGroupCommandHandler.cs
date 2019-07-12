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
    public class DeleteSupportGroupCommandHandler : IRequestHandler<DeleteSupportGroupCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteSupportGroupCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSupportGroupCommand request, CancellationToken cancellationToken)
        {
            var supportGroup = await _context.SupportGroups.FindAsync(request.SupportGroupId);

            supportGroup.IsDelete = true;

            _context.Update(supportGroup);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}