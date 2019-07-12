using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteDocumentCommand : IRequest
    {
        public int DocumentId { get; set; }
    }
}