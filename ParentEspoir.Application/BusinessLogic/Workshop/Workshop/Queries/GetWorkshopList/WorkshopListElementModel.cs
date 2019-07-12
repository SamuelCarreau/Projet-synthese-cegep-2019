using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class WorkshopListElementModel : IComparable<WorkshopListElementModel>
    {
        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WorkshopDescription { get; set; }
        public string WorkshopTypeName { get; set; }
        public bool IsOpen { get; set; }

        public int CompareTo(WorkshopListElementModel other)
        {
            return StartDate.CompareTo(other.StartDate);
        }
    }
}
 