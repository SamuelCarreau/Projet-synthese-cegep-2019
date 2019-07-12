using System;

namespace ParentEspoir.Application
{
    public class SeanceShortModel : IComparable<SeanceShortModel>
    {
        public int SeanceId { get; set; }
        public DateTime SeanceDate { get; set; }
        public string SeanceName { get; set; }
        public TimeSpan SeanceTimeSpan { get; set; }
        public string SeanceDescription { get; set; }

        public int CompareTo(SeanceShortModel other)
        {
            return SeanceDate.CompareTo(other.SeanceDate);
        }
    }
}
