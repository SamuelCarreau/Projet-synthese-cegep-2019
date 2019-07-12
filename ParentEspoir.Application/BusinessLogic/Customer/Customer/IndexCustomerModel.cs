using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class IndexCustomerModel
    {
        public int Id { get; set; }
        public int FileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NormalizedName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}
