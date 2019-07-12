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
    public class CreateWorkshopTypeCommandHandler : IRequestHandler<CreateWorkshopTypeCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public CreateWorkshopTypeCommandHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<Unit> Handle(CreateWorkshopTypeCommand request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new WorkshopType { Name = request.Name, Code = request.Code });

            await _context.SaveChangesAsync(cancellationToken);

            _memory.Remove("WorkshopTypeList");

            return Unit.Value;
        }
    }
}