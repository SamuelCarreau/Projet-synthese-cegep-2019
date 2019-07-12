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
    public class CreateSocialServiceCommandHandler : IRequestHandler<CreateProfilOptionCommand<SocialService>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateSocialServiceCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<SocialService> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new SocialService { Name = request.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}