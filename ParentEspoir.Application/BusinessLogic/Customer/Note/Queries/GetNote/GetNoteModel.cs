using System;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetNoteModel
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public DateTime? CreationDate { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorTitle { get; set;   }
        public string Body { get; set; }
        public int CustomerId { get; set; } 
        public int? NoteTypeId { get; set; }
        public string NoteTypeName { get; set; }
    }
}