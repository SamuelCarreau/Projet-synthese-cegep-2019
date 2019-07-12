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
    public class DeleteSocialServiceCommandHandler : IRequestHandler<DeleteProfilOptionCommand<SocialService>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteSocialServiceCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<SocialService> request, CancellationToken cancellationToken)
        {
            var socialService = await _context.SocialServices.FindAsync(request.Id);

            socialService.IsDelete = true;

            _context.Update(socialService);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}