using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class SessionListViewModel
    {
        public bool UserCanManageSession { get; set; }

        public IEnumerable<SessionModel> Sessions { get; set; }
    }
}
