using MediatR;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ParentEspoir.Application
{
    public class CreateWorkshopCommandHandler : IRequestHandler<CreateWorkshopCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateWorkshopCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateWorkshopCommand request, CancellationToken cancellationToken)
        {
            var workshop = await _context.AddAsync(new Workshop
            {
                EndDate = (DateTime)request.EndDate,
                SessionId = request.SessionId,
                StartDate = (DateTime)request.StartDate,
                WorkshopDescription = request.WorkshopDescription,
                WorkshopName = request.WorkshopName,
                WorkshopTypeId = (int)request.WorkshopTypeId,
                IsOpen = request.IsOpen.Value
            });

            if (request.SeanceCount != null && request.SeanceCount > 0)
            {
                DateTime dateIncrement = request.DateTimeFirstSeance.Value;

                for (int i = 0; i < request.SeanceCount; i++)
                {
                    workshop.Entity.Seances.Add(new Seance
                    {
                        SeanceDate = dateIncrement,
                        SeanceName = $"Seance {i+1}",
                        SeanceTimeSpan = request.SeanceLenght.Value
                    });

                    dateIncrement += TimeSpan.FromDays(request.IntervalNbDays.Value);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}