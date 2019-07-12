using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class SocialService : IProfileOption
    {
        public SocialService()
        {
            CustomerSocialServices = new HashSet<CustomerSocialService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<CustomerSocialService> CustomerSocialServices { get; private set; }
    }
}
