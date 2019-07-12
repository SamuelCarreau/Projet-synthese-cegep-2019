using MediatR;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, AppUser>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByIdAsync(request.Id);
        }
    }
}
