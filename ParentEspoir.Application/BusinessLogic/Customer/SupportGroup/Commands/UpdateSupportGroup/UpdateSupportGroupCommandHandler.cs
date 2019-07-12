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
    public class UpdateSupportGroupCommandHandler : IRequestHandler<UpdateSupportGroupCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateSupportGroupCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSupportGroupCommand request, CancellationToken cancellationToken)
        {
            var supportGroup = await _context.SupportGroups.FindAsync(request.SupportGroupId);

            supportGroup.Name = request.Name;
            supportGroup.Description = request.Description;
            supportGroup.Address = request.Address;
            supportGroup.UserId = request.UserId;

            _context.Update(supportGroup);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}