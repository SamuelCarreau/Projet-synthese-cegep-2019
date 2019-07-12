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
    public class UpdateStatisticCommandHandler : IRequestHandler<UpdateStatisticCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateStatisticCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateStatisticCommand request, CancellationToken cancellationToken)
        {
             return Unit.Value;
        }
    }
}