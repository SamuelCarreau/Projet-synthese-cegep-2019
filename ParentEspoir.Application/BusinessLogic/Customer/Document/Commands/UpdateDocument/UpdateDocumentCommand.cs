using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ParentEspoir.Application
{
    public class UpdateDocumentCommand : IRequest
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public int? DocumentTypeId { get; set; }

    }
}