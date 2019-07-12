using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class CustomerSocialService
    {
        public int CustomerId { get; set; }
        public CustomerDescription Customer { get; set; }
        public int SocialServiceId { get; set; }
        public SocialService SocialService { get; set; }
        public bool IsDelete { get; set; }
    }
}
