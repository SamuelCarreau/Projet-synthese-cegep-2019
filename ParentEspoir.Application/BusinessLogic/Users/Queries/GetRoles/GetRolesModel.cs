using Microsoft.AspNetCore.Identity;
using ParentEspoir.Domain.Entities;
using System.Collections.Generic;

namespace ParentEspoir.Application
{
    public class GetRolesModel
    {
        public AppUser User { get; set; }
        public IList<string> UserRoles { get; set; }
        public IdentityRole[] IdentityRoles { get; set; }
    }
}