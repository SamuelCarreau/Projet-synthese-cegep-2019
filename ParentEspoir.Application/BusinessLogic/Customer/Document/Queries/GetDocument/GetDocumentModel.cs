using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetDocumentModel
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public string Description { get; set; }
        public int? DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public int CustomerId { get; set; }
    }
}