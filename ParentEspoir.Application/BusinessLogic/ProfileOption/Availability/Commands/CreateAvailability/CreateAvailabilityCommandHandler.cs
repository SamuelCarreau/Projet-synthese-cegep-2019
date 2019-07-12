using MediatR;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class CreateAvailabilityCommandHandler : IRequestHandler<CreateProfilOptionCommand<Availability>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateAvailabilityCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<Availability> request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new InvalideNameException($"The name of the new {nameof(Availability)} is empty");
            }
            else if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalideNameException("The name cannot be fill with witespace");
            }
            
            await _context.AddAsync(new Availability() { Name = request.Name });
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}