using System;
using System.Collections.Generic;
using System.Linq;

namespace ParentEspoir.Application
{
    public class GetWorkshopModel
    {
        public bool IsOpen { get; set; }
        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WorkshopDescription { get; set; }
        public int WorkshopTypeId { get; set; }
        public string WorkshopTypeName { get; set; }
        public TimeSpan SeancesDuration { get => new TimeSpan(Seances.Sum(s => s.SeanceTimeSpan.Ticks)); }

        public List<SeanceShortModel> Seances { get; set; }
    }
}