using MediatR;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        public DeleteUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            await _userManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
