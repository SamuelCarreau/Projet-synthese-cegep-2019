using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class Log
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }
        public string CommandName { get; set; }
        public string CommandJSON { get; set; }
        public string Information { get; set; }
    }
}
