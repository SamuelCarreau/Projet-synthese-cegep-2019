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
    public class GetPersonnalFollowUpListQueryHandler : IRequestHandler<GetPersonnalFollowUpListQuery, GetPersonnalFollowUpListModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetPersonnalFollowUpListQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<GetPersonnalFollowUpListModel> Handle(GetPersonnalFollowUpListQuery request, CancellationToken cancellationToken)
        {
             return new GetPersonnalFollowUpListModel();
        }
    }
}