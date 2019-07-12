using MediatR;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, GetRolesModel>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRolesQueryHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<GetRolesModel> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var model = new GetRolesModel
            {
                User = user,
                UserRoles = await _userManager.GetRolesAsync(user),
                IdentityRoles = _roleManager.Roles.ToArray()
            };

            return model;
        }
    }
}
