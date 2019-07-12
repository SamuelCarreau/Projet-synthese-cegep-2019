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
    public class GetVolunteeringQueryHandler : IRequestHandler<GetVolunteeringQuery, GetVolunteeringModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetVolunteeringQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<GetVolunteeringModel> Handle(GetVolunteeringQuery request, CancellationToken cancellationToken)
        {
            var volunteering = await _context.Volunteerings
                .Include(v => v.Type)
                .Where(v => v.VolunteeringId == request.VolunteeringId && v.IsDelete == false)
                .SingleAsync();

            var volunteeringModel = new GetVolunteeringModel
            {
                Acknowledgment = volunteering.Acknowledgment,
                Amount = volunteering.Amount,
                CustomerId = volunteering.CustomerId,
                Date = volunteering.Date,
                Details = volunteering.Details,
                HourCount = volunteering.HourCount,
                Title = volunteering.Title,
                VolunteeringTypeName = volunteering.Type?.Name,
                VolunteeringId = volunteering.VolunteeringId,
                VolunteeringTypeId = volunteering.Type?.Id
            };
                
             return volunteeringModel;
        }
    }
}