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
    public class CreateCitizenStatusCommandHandler : IRequestHandler<CreateProfilOptionCommand<CitizenStatus>, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateCitizenStatusCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProfilOptionCommand<CitizenStatus> request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new CitizenStatus { Name = request.Name });

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}