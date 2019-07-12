using MediatR;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Application.Exceptions;
using ParentEspoir.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(new AppUser
            {
                Name = request.Name,
                Email = request.Email,
                UserName = request.Email
            }, request.Password);

            if (result.Succeeded == false)
            {
                throw new UserOperationException("Error creating user");
            }

            return Unit.Value;
        }
    }
}
