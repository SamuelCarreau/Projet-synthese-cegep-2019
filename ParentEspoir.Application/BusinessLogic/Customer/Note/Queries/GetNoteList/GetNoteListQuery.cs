using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetNoteListQuery : IRequest<IEnumerable<Note>>
    {
        public int CustomerId { get; set; }
    }
}