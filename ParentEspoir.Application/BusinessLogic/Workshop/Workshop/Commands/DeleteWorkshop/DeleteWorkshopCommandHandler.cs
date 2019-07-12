using MediatR;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteWorkshopCommandHandler : IRequestHandler<DeleteWorkshopCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteWorkshopCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteWorkshopCommand request, CancellationToken cancellationToken)
        {
            var workshop = await _context.Workshops.FindAsync(request.WorkshopId);

            workshop.IsDelete = true;

            _context.Update(workshop);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}