using ParentEspoir.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class Session : IComparable<Session>
    {
        public Session()
        {
            Workshops = new HashSet<Workshop>();
        }

        public int SessionId { get; set; }
        public int Year { get; set; }
        public Season Season { get; set; }
        public bool IsDelete { get; set; }
        public DateTime StartDate { get; set; }

        public ICollection<Workshop> Workshops { get; private set; }

        public int CompareTo(Session other)
        {
            var ct = Year.CompareTo(other.Year)*-1;

            if (ct == 0)
            {
                ct = Season.CompareTo(other.Season);
            }

            return ct;
        }
    }
}