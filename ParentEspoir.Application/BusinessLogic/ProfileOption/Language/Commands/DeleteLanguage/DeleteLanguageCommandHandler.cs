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
    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteProfilOptionCommand<Language>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteLanguageCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProfilOptionCommand<Language> request, CancellationToken cancellationToken)
        {
            var language = await _context.Languages.FindAsync(request.Id);

            language.IsDelete = true;

            _context.Update(language);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}