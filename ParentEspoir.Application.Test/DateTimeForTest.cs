using ParentEspoir.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application.Test
{
    public class DateTimeForTest : IDateTime
    {
        public DateTime Now { get; set; } = DateTime.Now;
    }
}
    