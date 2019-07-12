using MediatR;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Application.Exceptions;
using ParentEspoir.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            user.Name = request.Name;
            user.Email = request.Email;
            user.UserName = request.UserName;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                throw new ErrorSavingChangesException("Cannot save changes for the user");
            }

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var result = await _userManager.ResetPasswordAsync(user, request.PasswordToken, request.NewPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidPasswordException("Bad password");
                }
            }

            return Unit.Value;
        }
    }
}
