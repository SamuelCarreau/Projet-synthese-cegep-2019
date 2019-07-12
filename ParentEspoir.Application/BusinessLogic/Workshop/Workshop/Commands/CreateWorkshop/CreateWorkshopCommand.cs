using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using System;

namespace ParentEspoir.Application
{
    public class CreateWorkshopCommand : IRequest
    {
        public string WorkshopName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkshopDescription { get; set; }
        public bool? IsOpen { get; set; }

        public int? SeanceCount { get; set; }
        public TimeSpan? SeanceLenght { get; set; }
        public DateTime? DateTimeFirstSeance { get; set; }
        public int? IntervalNbDays { get; set; }

        public int SessionId { get; set; }
        public int? WorkshopTypeId { get; set; }
    }
}