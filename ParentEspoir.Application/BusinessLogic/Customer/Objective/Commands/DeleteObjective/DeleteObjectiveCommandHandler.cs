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
    public class DeleteObjectiveCommandHandler : IRequestHandler<DeleteObjectiveCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteObjectiveCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteObjectiveCommand request, CancellationToken cancellationToken)
        {
             return Unit.Value;
        }
    }
}