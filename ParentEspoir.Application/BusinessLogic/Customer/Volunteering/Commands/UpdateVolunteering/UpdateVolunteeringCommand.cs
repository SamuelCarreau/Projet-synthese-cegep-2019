using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParentEspoir.Application
{
    public class UpdateVolunteeringCommand : IRequest
    {
        public int? VolunteeringTypeId { get; set; }
        public int VolunteeringId { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public string VolonteeringTypeName { get; set; }
        public int HourCount { get; set; }
        public string Details { get; set; }
        public int CustomerId { get; set; }
        public string Acknowledgment { get; set; }
        public string Amount { get; set; }
    }
}
