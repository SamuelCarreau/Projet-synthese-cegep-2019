using MediatR;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application.Users.Queries.GetUserEdit
{
    public class GetUserEditQueryHandler : IRequestHandler<GetUserEditQuery, UpdateUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserEditQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UpdateUserCommand> Handle(GetUserEditQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            UpdateUserCommand model = null;

            if (user != null)
            {
                model = new UpdateUserCommand
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserName = user.UserName,
                    Email = user.Email,
                    PasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user),
                    NewPassword = ""
                };
            }

            return model;
        }
    }
}
