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
    public class CreateSexCommandHandler : IRequestHandler<CreateProfilOptionCommand<Sex>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateSexCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<Sex> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Sex() { Name = request.Name });
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}