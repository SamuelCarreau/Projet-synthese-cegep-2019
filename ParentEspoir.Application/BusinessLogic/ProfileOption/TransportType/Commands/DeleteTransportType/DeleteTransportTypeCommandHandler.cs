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
    public class DeleteTransportTypeCommandHandler : IRequestHandler<DeleteProfilOptionCommand<TransportType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteTransportTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<TransportType> request, CancellationToken cancellationToken)
        {
            var transportType = await _context.TransportTypes.FindAsync(request.Id);

            transportType.IsDelete = true;

            _context.Update(transportType);
            await _context.SaveChangesAsync();

             return Unit.Value;
        }
    }
}