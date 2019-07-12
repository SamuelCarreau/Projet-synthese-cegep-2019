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
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteMemberCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
             return Unit.Value;
        }
    }
}