using System;
using System.ComponentModel.DataAnnotations;

namespace ParentEspoir.Application
{
    public class ObjectiveModel
    {
        public int Id { get; set; }
        [Display(Name = "Objectif")]
        public string Name { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "Date début")]
        public DateTime StartDate { get; set; }
        public TimeSpan HourCount { get; set; }
        public TimeSpan HourGoal { get; set; }
        public string ObjectiveState { get; set; }
        public string WorkshopTypeName { get; set; }
        public string Comment { get; set; }
        public string Code { get; set; }
    }
}