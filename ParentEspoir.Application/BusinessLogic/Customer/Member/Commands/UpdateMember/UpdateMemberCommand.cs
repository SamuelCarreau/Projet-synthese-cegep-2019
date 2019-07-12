using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using System;

namespace ParentEspoir.Application
{
    public class UpdateMemberCommand : IRequest
    {
        public int MemberId { get; set; }
        public int CustomerDescriptionId { get; set; }
        public int VolunteeringHourCountByMonth { get; set; }
        public decimal AmountByMonth { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}