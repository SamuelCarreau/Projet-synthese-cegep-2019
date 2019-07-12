using ParentEspoir.Persistence;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using FluentValidation;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class DocumentTypeTest : ProfilOptionTestBase<DocumentType>
    {
        private ParentEspoirDbContext _context;
        public DocumentTypeTest() : base()
        {
            _context = GetDbContext();
        }

        [Fact]
        public override void CantDeleteRelatedData()
        {
            var document = new Document
            {
                DocumentType = new DocumentType { Name = "test" }
            };

            _context.Documents.Add(document);

            _context.SaveChanges();

            var result = _mediator.Send(new DeleteProfilOptionCommand<DocumentType>
            {
                Id = _context.DocumentTypes.Where(t => t.Name == "test").Single().Id
            }).ShouldThrow(typeof(ValidationException));
        }
    }
}