using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ParentEspoir.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class UpdateParticipantCommandHandler : IRequestHandler<UpdateParticipantCommand, Unit>
    {
        private readonly IMemoryCache _memory;
        private readonly ParentEspoirDbContext _context;

        public UpdateParticipantCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(UpdateParticipantCommand request, CancellationToken cancellationToken)
        {
            var seance = await _context.Seances.FindAsync(request.SeanceId);

            foreach (var participation in request.ParticipantsAttendance)
            {
                var participationEntity = await _context.Participants.FindAsync(participation.ParticipantId);

                if (participation.ParticipationStatus == Domain.Enums.ParticipationStatus.Absent)
                {
                    participationEntity.NbHourLate = seance.SeanceTimeSpan;
                }
                else if (participation.ParticipationStatus == Domain.Enums.ParticipationStatus.Present)
                {
                    participationEntity.NbHourLate = new TimeSpan(0,0,0);
                }
                else
                {
                    participationEntity.NbHourLate = new TimeSpan(participation.NbHourLate, participation.NbMinuteLate, 0);
                }
                participationEntity.Status = participation.ParticipationStatus;

                await _context.SaveChangesAsync(cancellationToken);
            }

            _memory.Remove(InMemoryKeyConstants.PARTICIPANTS_IN_WORKSHOP + request.WorkshopId);


            return Unit.Value;
        }
    }
}