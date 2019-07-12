using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetDocumentListQuery : IRequest<IEnumerable<Document>>
    {
        public int CustomerId { get; set; }
    }
}