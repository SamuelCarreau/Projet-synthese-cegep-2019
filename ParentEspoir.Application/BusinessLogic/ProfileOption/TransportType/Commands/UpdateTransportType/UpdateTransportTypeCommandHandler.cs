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
    public class UpdateTransportTypeCommandHandler : IRequestHandler<UpdateProfilOptionCommand<TransportType>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateTransportTypeCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<TransportType> request, CancellationToken cancellationToken)
        {
            var transportType = await _context.TransportTypes
                .Where(t => t.IsDelete == false && t.Id == request.Id)
                .SingleAsync();

            transportType.Name = request.Name;

            _context.Update(transportType);
            await _context.SaveChangesAsync();

             return Unit.Value;
        }
    }
}