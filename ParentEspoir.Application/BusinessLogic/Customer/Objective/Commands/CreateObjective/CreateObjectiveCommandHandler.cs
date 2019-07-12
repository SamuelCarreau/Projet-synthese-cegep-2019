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

namespace ParentEspoir.Application
{
    public class CreateObjectiveCommandHandler : IRequestHandler<CreateObjectiveCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateObjectiveCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateObjectiveCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}