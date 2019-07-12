using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ParentEspoir.Application
{
    public class GetSupportGroupQueryHandler : IRequestHandler<GetSupportGroupQuery, SupportGroup>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public GetSupportGroupQueryHandler(ParentEspoirDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<SupportGroup> Handle(GetSupportGroupQuery request, CancellationToken cancellationToken)
        {
            var supportGroup = await _context.SupportGroups
                .Include(sg => sg.Customers)
                .Include(sg => sg.User)
                .Where(sg => sg.IsDelete == false && sg.SupportGroupId == request.SupportGroupId)
                .SingleAsync();
            
            return supportGroup;
        }
    }
}