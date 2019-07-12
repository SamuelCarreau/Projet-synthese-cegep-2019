using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ParentEspoir.Domain.Enums;
using System;
using ParentEspoir.Domain.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public CreateSessionCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            DateTime startDate = new DateTime();
            switch (request.Season)
            {
                case (Season.Winter):
                    startDate = new DateTime(request.Year.Value, SessionConstant.WINTER_START_MONTH, SessionConstant.WINTER_START_DAY);
                    break;
                case (Season.Spring):
                    startDate = new DateTime(request.Year.Value, SessionConstant.SPRING_START_MONTH, SessionConstant.SPRING_START_DAY);
                    break;
                case (Season.Summer):
                    startDate = new DateTime(request.Year.Value, SessionConstant.SUMMER_START_MONTH, SessionConstant.SUMMER_START_DAY);
                    break;
                case (Season.Fall):
                    startDate = new DateTime(request.Year.Value, SessionConstant.FALL_START_MONTH, SessionConstant.FALL_START_DAY);
                    break;
            }

            await _context.AddAsync(new Session
            {
                Season = request.Season.Value,
                Year = request.Year.Value,
                StartDate = startDate
            }); 

            await _context.SaveChangesAsync(cancellationToken);

            _memory.Remove("SESSIONLIST");

            return Unit.Value;
        }
    }
}