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
    public class GetWorkshopTypeListQueryHandler : IRequestHandler<GetWorkshopTypeListQuery, IEnumerable<WorkshopTypeModel>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _memory;

        public GetWorkshopTypeListQueryHandler(ParentEspoirDbContext context, IMemoryCache memory)
        {
            _context = context;
            _memory = memory;
        }

        public async Task<IEnumerable<WorkshopTypeModel>> Handle(GetWorkshopTypeListQuery request, CancellationToken cancellationToken)
        {
            if (!_memory.TryGetValue("WorkshopTypeList", out IEnumerable<WorkshopTypeModel> model))
            {
                model = await _context.WorkshopTypes.Where(w => w.IsDelete == false).Select(w => new WorkshopTypeModel { Code = w.Code, Id = w.Id, Name = w.Name }).ToArrayAsync();

                _memory.Set("WorkshopTypeList", model);
            }

            return model;
        }
    }
}