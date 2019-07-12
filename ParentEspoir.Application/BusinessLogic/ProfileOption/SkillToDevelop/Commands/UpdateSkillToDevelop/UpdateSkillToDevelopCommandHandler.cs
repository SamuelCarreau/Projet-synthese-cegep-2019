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
    public class UpdateSkillToDevelopCommandHandler : IRequestHandler<UpdateProfilOptionCommand<SkillToDevelop>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateSkillToDevelopCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProfilOptionCommand<SkillToDevelop> request, CancellationToken cancellationToken)
        {
            var skill = await _context.SkillToDevelops.FindAsync(request.Id);

            skill.Name = request.Name;

            _context.Update(skill);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}