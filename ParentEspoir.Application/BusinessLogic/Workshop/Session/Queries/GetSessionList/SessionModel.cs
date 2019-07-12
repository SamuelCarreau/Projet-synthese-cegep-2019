using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class SessionModel
    {
        public int SessionId { get; set; }
        public string SeasonName { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
    }
}
