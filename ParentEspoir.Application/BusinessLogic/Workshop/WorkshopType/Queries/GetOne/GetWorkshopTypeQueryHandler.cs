using MediatR;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class GetWorkshopTypeQueryHandler : IRequestHandler<GetWorkshopTypeQuery, WorkshopTypeModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetWorkshopTypeQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<WorkshopTypeModel> Handle(GetWorkshopTypeQuery request, CancellationToken cancellationToken)
        {
            return (WorkshopTypeModel)
                await _context.WorkshopTypes
                .FindAsync(request.Id);
        }
    }
}
