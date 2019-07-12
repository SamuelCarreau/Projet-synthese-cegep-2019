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
    public class GetObjectiveQueryHandler : IRequestHandler<GetObjectiveQuery, ObjectiveModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetObjectiveQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<ObjectiveModel> Handle(GetObjectiveQuery request, CancellationToken cancellationToken)
        {
             return new ObjectiveModel();
        }
    }
}