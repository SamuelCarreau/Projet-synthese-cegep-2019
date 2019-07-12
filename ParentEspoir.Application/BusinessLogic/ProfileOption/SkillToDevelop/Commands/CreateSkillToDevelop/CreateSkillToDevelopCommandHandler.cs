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
    public class CreateSkillToDevelopCommandHandler : IRequestHandler<CreateProfilOptionCommand<SkillToDevelop>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateSkillToDevelopCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<SkillToDevelop> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new SkillToDevelop { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}