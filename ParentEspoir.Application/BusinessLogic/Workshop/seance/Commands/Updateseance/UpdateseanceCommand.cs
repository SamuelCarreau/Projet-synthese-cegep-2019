using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using System;

namespace ParentEspoir.Application
{
    public class UpdateSeanceCommand : IRequest
    {
        public int? WorkshopId { get; set; }
        public int? SeanceId { get; set; }
        public string SeanceName { get; set; }
        public DateTime? SeanceDate { get; set; }
        public TimeSpan? SeanceTimeSpan { get; set; }
        public string SeanceDescription { get; set; }
    }
}