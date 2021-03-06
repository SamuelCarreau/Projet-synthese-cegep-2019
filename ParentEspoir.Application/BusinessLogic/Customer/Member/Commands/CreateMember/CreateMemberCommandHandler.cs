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
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateMemberCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
             return Unit.Value;
        }
    }
}