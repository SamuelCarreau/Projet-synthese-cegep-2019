using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class DocumentType : IProfileOption
    {
        public DocumentType()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<Document> Documents { get; private set; }

    }
}