using System;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetVolunteeringModel
    {
        public int VolunteeringId { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public string VolunteeringTypeName { get; set; }
        public int? VolunteeringTypeId { get; set; }
        public int HourCount { get; set; }
        public string Details { get; set; }
        public int CustomerId { get; set; }
        public string Acknowledgment { get; set; }
        public decimal Amount { get; set; }
    }
}