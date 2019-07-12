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
    public class CreateVolunteeringCommandHandler : IRequestHandler<CreateVolunteeringCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateVolunteeringCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateVolunteeringCommand request, CancellationToken cancellationToken)
        {
            var volunteering = new Volunteering
            {
                Customer = await _context.Customers.FindAsync(request.CustomerId),
                Type = await _context.VolunteeringTypes.FindAsync(request.VolunteeringTypeId),

                Acknowledgment = request.Acknowledgment,
                Amount = DecimalParser.Parse(request.Amount),
                Date = request.Date,
                Details = request.Details,
                HourCount = request.HourCount,
                Title = request.Title
            };

            await _context.AddAsync(volunteering);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}