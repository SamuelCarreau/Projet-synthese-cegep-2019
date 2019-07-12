using MediatR;
using System;

namespace ParentEspoir.Application
{
    public class CreateObjectiveCommand : IRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int WorkshopTypeId { get; set; }
        public int CustomerId { get; set; }
        public double NbHourDue { get; set; }
        public DateTime StartDate { get; set; }
    }
}