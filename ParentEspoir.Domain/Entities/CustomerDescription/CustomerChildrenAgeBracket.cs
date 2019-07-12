
namespace ParentEspoir.Domain.Entities
{
    public class CustomerChildrenAgeBracket
    {
        public int CustomerId { get; set; }
        public CustomerDescription Customer { get; set; }
        public int AgeBracketId { get; set; }
        public ChildrenAgeBracket AgeBracket { get; set; }
        public bool IsDelete { get; set; }
    }
}