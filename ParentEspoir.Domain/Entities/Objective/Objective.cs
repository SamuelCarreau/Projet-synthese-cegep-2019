using ParentEspoir.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class Objective
    {
        //A changer
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int WorkshopTypeId { get; set; }
        public WorkshopType WorkshopType { get; set; }
        public ObjectiveState State { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsDelete { get; set; }
    }
}
