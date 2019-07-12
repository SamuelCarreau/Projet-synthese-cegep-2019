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
    public class DeleteSkillToDevelopCommandHandler : IRequestHandler<DeleteProfilOptionCommand<SkillToDevelop>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteSkillToDevelopCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<SkillToDevelop> request, CancellationToken cancellationToken)
        {
            var skill = await _context.SkillToDevelops.FindAsync(request.Id);

            skill.IsDelete = true;

            _context.Update(skill);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}