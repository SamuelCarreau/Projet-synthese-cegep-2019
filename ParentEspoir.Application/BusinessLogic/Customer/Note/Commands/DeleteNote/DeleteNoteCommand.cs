using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteNoteCommand : IRequest
    {
        public int NoteId { get; set; }
    }
}