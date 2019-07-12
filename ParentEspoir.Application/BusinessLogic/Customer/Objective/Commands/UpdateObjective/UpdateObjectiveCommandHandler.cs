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
    public class UpdateObjectiveCommandHandler : IRequestHandler<UpdateObjectiveCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateObjectiveCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateObjectiveCommand request, CancellationToken cancellationToken)
        {
             return Unit.Value;
        }
    }
}