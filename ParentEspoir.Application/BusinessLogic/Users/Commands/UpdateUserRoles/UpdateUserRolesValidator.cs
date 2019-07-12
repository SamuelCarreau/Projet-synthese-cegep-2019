using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class UpdateUserRolesValidator : AbstractValidator<UpdateUserRolesCommand>
    {
        public UpdateUserRolesValidator(UserManager<AppUser> userManager)
        {
            RuleFor(u => u.UserId)
                .Must(id => userManager.FindByIdAsync(id).Result != null);
        }
    }
}
