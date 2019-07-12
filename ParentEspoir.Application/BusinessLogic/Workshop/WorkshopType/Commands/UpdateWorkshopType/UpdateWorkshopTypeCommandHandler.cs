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
    public class UpdateWorkshopTypeCommandHandler : IRequestHandler<UpdateWorkshopTypeCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public UpdateWorkshopTypeCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(UpdateWorkshopTypeCommand request, CancellationToken cancellationToken)
        {
            var workshopType = await _context.WorkshopTypes.FindAsync(request.Id);

            workshopType.Name = request.Name;
            workshopType.Code = request.Code;

            _context.Update(workshopType);
            await _context.SaveChangesAsync(cancellationToken);

            _memory.Remove("WorkshopTypeList");

            return Unit.Value;
        }
    }
}