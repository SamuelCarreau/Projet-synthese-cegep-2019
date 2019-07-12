using MediatR;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Application.Exceptions;
using ParentEspoir.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateUserRolesCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var roles = _roleManager.Roles.Select(r => r.Name);

            foreach (var role in roles)
            {
                if (await _userManager.IsInRoleAsync(user, role))
                {
                    var removeResult = await _userManager.RemoveFromRoleAsync(user, role);

                    if (!removeResult.Succeeded)
                    {
                        throw new UserOperationException("Error removing roles");
                    }
                }
            }

            var result = await _userManager.AddToRolesAsync(user, request.Roles);

            if (!result.Succeeded)
            {
                throw new UserOperationException("Roles assignation dosen't work");
            }

            return Unit.Value;
        }
    }
}
