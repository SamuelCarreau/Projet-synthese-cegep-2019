using ParentEspoir.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParentEspoir.Domain.Entities
{
    public class Pregnancy
    {
        public int PregnancyId { get; set; }
        public CustomerDescription CustomerDescription { get; set; }
        public DateTime ChildBirthExpectedDate { get; set; }
        public bool IsDelete { get; set; }

        public int WeekCount
        {
            get
            {
                DateTime miracleDate = ChildBirthExpectedDate - PregnancyConstant.NB_DAYS_FOR_WHOLE_PREGNENCY;
                return (DateTime.Now - miracleDate).Days / 7;
            }
        }
    }
}