using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParentEspoir.Application
{
    public class SeanceListModel
    {
        public IEnumerable<SeanceShortModel> Seances { get; set; }
        public TimeSpan SeancesDuration { get => new TimeSpan(Seances.Sum(s => s.SeanceTimeSpan.Ticks)); }
    }
}
