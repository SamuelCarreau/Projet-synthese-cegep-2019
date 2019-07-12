using System;

namespace ParentEspoir.Domain.Entities
{
    public class Note
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public DateTime? CreationDate { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorTitle { get; set; }
        public string Body { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public NoteType NoteType { get; set; }
        public int? NoteTypeId { get; set; }
        public bool IsDelete { get; set; }
    }
}