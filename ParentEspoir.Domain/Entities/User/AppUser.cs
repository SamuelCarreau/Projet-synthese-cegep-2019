using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            SupportGroup = new HashSet<SupportGroup>();
        }

        public string Name { get; set ; }
        public ICollection<SupportGroup> SupportGroup { get; private set; }
    }
}
