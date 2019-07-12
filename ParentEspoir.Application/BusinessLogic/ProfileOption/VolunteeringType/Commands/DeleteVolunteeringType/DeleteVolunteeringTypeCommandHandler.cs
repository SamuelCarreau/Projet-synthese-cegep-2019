using MediatR;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteVolunteeringTypeCommandHandler : IRequestHandler<DeleteProfilOptionCommand<VolunteeringType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteVolunteeringTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<VolunteeringType> request, CancellationToken cancellationToken)
        {
            var volonteeringType = await _context.VolunteeringTypes.FindAsync(request.Id);

            volonteeringType.IsDelete = true;

            _context.Update(volonteeringType);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}