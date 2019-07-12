using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetDocumentQuery : IRequest<GetDocumentModel>
    {
        public int DocumentId { get; set; }
    }
}