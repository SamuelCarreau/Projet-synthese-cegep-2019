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
    public class GetSupportGroupListQueryHandler : IRequestHandler<GetSupportGroupListQuery, IEnumerable<SupportGrougModel>>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public GetSupportGroupListQueryHandler(ParentEspoirDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<SupportGrougModel>> Handle(GetSupportGroupListQuery request, CancellationToken cancellationToken)
        {
            var groups = await _context.SupportGroups
                .Include(sg => sg.User)
                .Where(sg => sg.IsDelete == false)
                .Select(sg => new SupportGrougModel
                {
                    Address = sg.Address,
                    Description = sg.Description,
                    Name = sg.Name,
                    SupportGroupId = sg.SupportGroupId,
                    UserId = sg.UserId,
                    UserName = sg.User != null ? string.IsNullOrWhiteSpace(sg.User.Name) ? sg.User.UserName : sg.User.Name : "Aucun responsable"
                })
                .ToArrayAsync();

            return groups;
        }
    }
}