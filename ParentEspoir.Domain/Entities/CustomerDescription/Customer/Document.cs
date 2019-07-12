namespace ParentEspoir.Domain.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DocumentType DocumentType { get; set; }
        public bool IsDelete { get; set; }
    }
}