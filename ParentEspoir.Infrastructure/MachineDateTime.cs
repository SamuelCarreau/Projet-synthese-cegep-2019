using ParentEspoir.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Infrastructure
{
    /// <summary>
    /// This is the implamentation of the IDateTime interface in the common project.
    /// This class is use in the application layer to get the current time.
    /// The reason for this separation of implamentation id that it makes it very easy to test the application
    /// with differents dates. The tests layer has its own implamentation (DateTimeForTest class) which you can change before
    /// caling the right mediator module in differents tests.
    /// </summary>
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
