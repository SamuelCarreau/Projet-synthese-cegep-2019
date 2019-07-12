using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<AppUser>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUsersQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUser>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.Users.ToArrayAsync();
        }
    }
}
