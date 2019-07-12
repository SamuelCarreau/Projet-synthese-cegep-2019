using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using System;

namespace ParentEspoir.Application
{
    public class CreateNoteCommand : IRequest
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorTitle { get; set; }
        public string Body { get; set; }
        public int CustomerId { get; set; }
        public int? NoteTypeId { get; set; }
        public bool IsDelete { get; set; }
    }
}