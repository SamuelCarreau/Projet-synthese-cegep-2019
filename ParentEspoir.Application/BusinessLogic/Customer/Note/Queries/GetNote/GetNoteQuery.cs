using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetNoteQuery : IRequest<GetNoteModel>
    {
        public int NoteId { get; set; }
    }
}