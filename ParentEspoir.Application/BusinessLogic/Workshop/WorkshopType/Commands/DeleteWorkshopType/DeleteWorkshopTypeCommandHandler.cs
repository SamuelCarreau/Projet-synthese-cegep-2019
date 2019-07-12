using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class DeleteWorkshopTypeCommandHandler : IRequestHandler<DeleteWorkshopTypeCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public DeleteWorkshopTypeCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(DeleteWorkshopTypeCommand request, CancellationToken cancellationToken)
        {
            var workshopType = await _context.WorkshopTypes.FindAsync(request.Id);

            workshopType.IsDelete = true;

            _context.Update(workshopType);
            await _context.SaveChangesAsync(cancellationToken);

            _memory.Remove("WorkshopTypeList");

            return Unit.Value;
        }
    }
}