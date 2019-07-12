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
    public class GetPersonnalFollowUpQueryHandler : IRequestHandler<GetPersonnalFollowUpQuery, GetPersonnalFollowUpModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetPersonnalFollowUpQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<GetPersonnalFollowUpModel> Handle(GetPersonnalFollowUpQuery request, CancellationToken cancellationToken)
        {
             return new GetPersonnalFollowUpModel();
        }
    }
}