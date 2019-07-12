using ParentEspoir.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class Workshop
    {
        public Workshop()
        {
            Seances = new HashSet<Seance>();
            Participants = new HashSet<Participant>();
        }

        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WorkshopDescription { get; set; }
        public int WorkshopTypeId { get; set; }
        public WorkshopType WorkshopType { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public bool IsDelete { get; set; }
        public bool IsOpen { get; set; }

        public ICollection<Seance> Seances { get; private set; }
        public ICollection<Participant> Participants { get; private set; }

        //Ajouter un objectif a l'atelier
    }
}
