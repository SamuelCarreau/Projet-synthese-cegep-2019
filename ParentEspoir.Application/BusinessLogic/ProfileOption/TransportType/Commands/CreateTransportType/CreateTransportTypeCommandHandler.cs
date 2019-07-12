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
    public class CreateTransportTypeCommandHandler : IRequestHandler<CreateProfilOptionCommand<TransportType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateTransportTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<TransportType> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new TransportType
            {
                Name = request.Name,
            });

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}