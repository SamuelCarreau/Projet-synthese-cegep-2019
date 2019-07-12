using MediatR;
using System;

namespace ParentEspoir.Application
{
    public class UpdateWorkshopCommand : IRequest
    {
        public int SessionId { get; set; }
        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkshopDescription { get; set; }
        public int? WorkshopTypeId { get; set; }
        public bool? IsOpen { get; set; }
    }
}