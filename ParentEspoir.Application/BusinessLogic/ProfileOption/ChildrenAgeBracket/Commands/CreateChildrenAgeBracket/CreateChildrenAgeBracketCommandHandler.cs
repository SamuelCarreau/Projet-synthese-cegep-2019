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
    public class CreateChildrenAgeBracketCommandHandler : IRequestHandler<CreateProfilOptionCommand<ChildrenAgeBracket>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateChildrenAgeBracketCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<ChildrenAgeBracket> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new ChildrenAgeBracket { Name = request.Name });

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}