using MediatR;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using ParentEspoir.Domain.Enums;

namespace ParentEspoir.Application
{
    public class GetObjectiveListQueryHandler : IRequestHandler<GetObjectiveListQuery, ObjectiveIndexViewModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetObjectiveListQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<ObjectiveIndexViewModel> Handle(GetObjectiveListQuery request, CancellationToken cancellationToken)
        {
            var objectives = await _context
                .Objectives
                .Include(o => o.WorkshopType)
                .Include(o => o.Customer)
                .Where(o => o.CustomerId == request.CustomerId)
                .ToListAsync();

            var dictio = new ObjectiveIndexViewModel();

            dictio.CustomerId = request.CustomerId;
            dictio.CustomerName = _context.Customers.Find(request.CustomerId).FullName;

            foreach (var obj in objectives)
            {
                if (dictio.ContainsKey(obj.WorkshopType.Name))
                {
                    dictio[obj.WorkshopType.Name].Add(new ObjectiveModel
                    {
                        Code = obj.Code,
                        Comment = obj.Description,
                        CustomerId = obj.CustomerId,
                        HourCount = TimeSpan.FromHours(0),
                        Id = obj.Id,
                        ObjectiveState = ObjectifStateInFrench(obj.State),
                        StartDate = obj.StartDate
                    });
                }
                else
                {
                    dictio.Add(obj.WorkshopType.Name, new List<ObjectiveModel>());

                    dictio[obj.WorkshopType.Name].Add(new ObjectiveModel
                    {
                        Code = obj.Code,
                        Comment = obj.Description,
                        CustomerId = obj.CustomerId,
                        HourCount = TimeSpan.FromHours(0),
                        Id = obj.Id,
                        ObjectiveState = ObjectifStateInFrench(obj.State),
                        StartDate = obj.StartDate
                    });
                }
            }

            foreach (var habilities in dictio.Keys)
            {
                dictio.HabilitiesHourToReach.Add(habilities, TimeSpan.FromHours(0));

                dictio.HabilitiesHourToReach[habilities] = TimeSpan.FromHours(dictio[habilities].Sum(h => h.HourCount.TotalHours));
            }

            return dictio;
        }

        private string ObjectifStateInFrench(ObjectiveState state)
        {
            switch (state)
            {
                case ObjectiveState.ABANDON:
                    return "Abandont";
                case ObjectiveState.FAILLURE:
                    return "Échec";
                case ObjectiveState.IN_PURSUIT:
                    return "En poursuite";
                case ObjectiveState.SUCCESS:
                    return "Succès";
            }
            throw new InvalidOperationException($"Cant translate the ObjectiveState {state}");
        }
    }
}