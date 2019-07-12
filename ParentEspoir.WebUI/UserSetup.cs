using Microsoft.AspNetCore.Identity;
using ParentEspoir.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ParentEspoir.WebUI
{
    public class UserSetup
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserSetup(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Do()
        {
            CreateRole("Administrateur").Wait();
            CreateRole("Intervenant Sociaux").Wait();
            CreateRole("Animateur").Wait();
            CreateRole("Superviseur groupe de soutien").Wait();
            CreateAdmin("admin@admin.com", "?1234Soleil").Wait();
        }

        private async Task CreateRole(string name)
        {
            var roleExist = await _roleManager.RoleExistsAsync(name);

            if (!roleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole { Name = name });

                if (!result.Succeeded)
                {
                    throw new Exception("Error creating required role");
                }
            }
        }

        private async Task CreateAdmin(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(new AppUser
                {
                    Email = email,
                    UserName = email
                }, password);

                user = await _userManager.FindByNameAsync(email);

                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, "Administrateur");

                    if (!result.Succeeded)
                    {
                        throw new Exception("Error adding admin to admin role");
                    }
                }
                else
                {
                    throw new Exception("creating the admin");
                }
            }
        }
    }
}
