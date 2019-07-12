using MediatR;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class CreateSupportGroupCommandHandler : IRequestHandler<CreateSupportGroupCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateSupportGroupCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateSupportGroupCommand request, CancellationToken cancellationToken)
        {
            await _context.SupportGroups.AddAsync(new SupportGroup
            {
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                UserId = request.UserId
            });

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}