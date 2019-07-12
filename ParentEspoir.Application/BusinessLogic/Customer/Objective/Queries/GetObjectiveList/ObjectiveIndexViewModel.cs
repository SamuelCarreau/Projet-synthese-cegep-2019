using System;
using System.Collections.Generic;

namespace ParentEspoir.Application
{
    public class ObjectiveIndexViewModel : Dictionary<string, List<ObjectiveModel>>
    {
        public ObjectiveIndexViewModel() : base()
        {
            HabilitiesHourToReach = new Dictionary<string, TimeSpan>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Dictionary<string, TimeSpan> HabilitiesHourToReach { get; private set; }
    }
}
