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
    public class CreatePersonnalFollowUpCommandHandler : IRequestHandler<CreatePersonnalFollowUpCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreatePersonnalFollowUpCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreatePersonnalFollowUpCommand request, CancellationToken cancellationToken)
        {
             return Unit.Value;
        }
    }
}