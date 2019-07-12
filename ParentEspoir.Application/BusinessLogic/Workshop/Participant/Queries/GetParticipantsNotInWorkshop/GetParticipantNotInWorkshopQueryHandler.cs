using MediatR;
using ParentEspoir.Persistence;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParentEspoir.Application
{
    public class GetParticipantsNotInWorkshopQueryHandler : IRequestHandler<GetParticipantsNotInWorkshopQuery, IEnumerable<ParticipantSelectionModel>>
    {
        private readonly ParentEspoirDbContext _context;

        public GetParticipantsNotInWorkshopQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParticipantSelectionModel>> Handle(GetParticipantsNotInWorkshopQuery request, CancellationToken cancellationToken)
        {
            var customers = _context.Customers.Select(c => 
            new ParticipantSelectionModel
            {
                CustomerId = c.CustomerId,
                Name = c.FullName
            }).ToHashSet();

            foreach (var custo in await GetParticipantInWorkShop(request.WorkshopId))
            {
                customers.Remove(custo);
            }
            List<ParticipantSelectionModel> listParticipantNotInWorkshop = new List<ParticipantSelectionModel>();

            listParticipantNotInWorkshop = customers.ToList<ParticipantSelectionModel>();
            listParticipantNotInWorkshop.Sort();

            return listParticipantNotInWorkshop;
        }

        private async Task<HashSet<ParticipantSelectionModel>> GetParticipantInWorkShop(int workshopId)
        {
            var seances = await _context.Seances
                    .Include(s => s.Participants)
                    .ThenInclude(c => c.Customer)
                    .Where(s => s.IsDelete == false && s.WorkshopId == workshopId)
                    .ToArrayAsync();

            var model = new HashSet<ParticipantSelectionModel>();

            foreach (var seance in seances)
            {
                foreach (var participant in seance.Participants.Where(p => p.IsDelete == false))
                {
                    model.Add(new ParticipantSelectionModel
                    {
                        CustomerId = participant.CustomerId,
                        Name = participant.Customer.FullName
                    });
                }
            }

            return model;
        }
    }
}
