using ParentEspoir.Persistence;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace ParentEspoir.Application.Test
{
    public class DocumentTest : TestBase
    {
        private static readonly string CREATE = "CREATE";
        private static readonly string CREATE_DESCRIPTION = "Testing create document";
        private static readonly string FIND = "FIND";
        private static readonly string FIND_DESCRIPTION = "My description";
        private static readonly string UPDATE = "UPDATE";
        private static readonly string AFTER_UPDATE = "AFTER_UPDATE";
        private static readonly string UPDATE_DESCRIPTION = "My other description";
        private static readonly string AFTER_UPDATE_DESCRIPTION = "IIIII";
        private static readonly string DELETE = "DELETE";
        private static readonly string DELETED = "DELETED";
        private static readonly string DOCUMENT_TYPE_NAME = "Official";
        private static readonly string AFTER_UPDATE_TYPE = "Unofficial";

        private ParentEspoirDbContext _context;
        private string _testFileDirectory;
        private string _findPath;

        public DocumentTest()
        {
            _context = GetDbContext();

            var johnDoe = new Customer
            {
                Address = "288, northway-south",
                City = "Saint-Alfredos",
                Country = "Canada",
                CustomerDescription = null,
                DateOfBirth = new DateTime(1994, 01, 21, 11, 36, 34),
                FirstName = "John",
                LastName = "Doe",
                Phone = "458-895-5623",
                PostalCode = "G0G 0G0",
                Province = "Québec"
            };

            _context.Add(johnDoe);

            var documentType = new DocumentType
            {
                Name = DOCUMENT_TYPE_NAME
            };

            var afterupdateDocType = new DocumentType
            {
                Name = AFTER_UPDATE_TYPE
            };

            _context.Add(documentType);
            _context.Add(afterupdateDocType);

            _testFileDirectory = $"{Directory.GetCurrentDirectory()}\\DocumentTestFiles";

            Directory.CreateDirectory(_testFileDirectory);

            _findPath = $"{_testFileDirectory}\\{FIND}.txt";
            var updateFilePath = $"{_testFileDirectory}\\{UPDATE}.txt";
            var deleteFilePath = $"{_testFileDirectory}\\{DELETE}.txt";
            var deletedFilePath = $"{_testFileDirectory}\\{DELETED}.txt";

            File.WriteAllText(_findPath, FIND);
            File.WriteAllText(updateFilePath, UPDATE);
            File.WriteAllText(deleteFilePath, DELETE);

            _context.Documents.Add(new Document
            {
                DocumentName = FIND,
                Description = FIND_DESCRIPTION,
                Path = _findPath,
                Customer = johnDoe,
                DocumentType = documentType
            });

            _context.Documents.Add(new Document
            {
                DocumentName = UPDATE,
                Path = updateFilePath,
                Description = UPDATE_DESCRIPTION,
                Customer = johnDoe,
                DocumentType = documentType
            });

            _context.Documents.Add(new Document
            {
                DocumentName = DELETE,
                Path = deleteFilePath,
                Customer = johnDoe,
                DocumentType = documentType
            });

            _context.Documents.Add(new Document
            {
                DocumentName = DELETED,
                Path = deleteFilePath,
                Customer = johnDoe,
                IsDelete = true
            });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetDocumentTest()
        {
            var response = await _mediator.Send(new GetDocumentQuery
            {
                DocumentId = _context.Documents.Where(d => d.DocumentName == FIND).Single().DocumentId
            });

            response.Name.ShouldBe(FIND);
            response.Description.ShouldBe(FIND_DESCRIPTION);
            response.FileUrl.ShouldBe(_findPath);
            response.DocumentTypeName.ShouldBe(DOCUMENT_TYPE_NAME);
        }

        [Fact]
        public async Task GetListDocumentTest()
        {
            var response = await _mediator.Send(new GetDocumentListQuery
            {
                CustomerId = _context.Customers.First().CustomerId
            });

            response.GetType().GetInterfaces().ShouldContain(typeof(IEnumerable));

            response.Count().ShouldBe(3);
            response.ShouldContain(d => d.DocumentName == FIND && d.Description == FIND_DESCRIPTION && d.Path.Contains($"{FIND}.txt"));
            response.ShouldContain(d => d.DocumentName == UPDATE && d.Path.Contains($"{UPDATE}.txt"));
            response.ShouldContain(d => d.DocumentName == DELETE && d.Path.Contains($"{DELETE}.txt"));
            response.ShouldAllBe(d => d.DocumentType.Name == DOCUMENT_TYPE_NAME);
            response.ShouldNotContain(d => d.DocumentName == DELETED);
        }

        //[Fact]
        //public void CreateDocumentTest()
        //{
        //    var command = new CreateDocumentCommand
        //    {
        //        CustomerId = _context.Customers.First().CustomerId,
        //        DocumentName = CREATE,
        //        Description = CREATE_DESCRIPTION,
        //        DocumentTypeId = _context.DocumentTypes.Where(n => n.Name == DOCUMENT_TYPE_NAME).Single().Id
        //    };

        //    command.File = new FormFile(null, 0, 0, "", "");

        //    true.ShouldBe(false);
        //}

        [Fact]
        public async Task UpdateDocumentTest()
        {
            _context.Documents.Any(d => d.DocumentName == AFTER_UPDATE).ShouldBe(false);

            await _mediator.Send(new UpdateDocumentCommand
            {
                DocumentId = _context.Documents.Where(d => d.DocumentName == UPDATE).Single().DocumentId,
                DocumentName = AFTER_UPDATE,
                Description = AFTER_UPDATE_DESCRIPTION,
                DocumentTypeId = _context.DocumentTypes.Where(n => n.Name == AFTER_UPDATE_TYPE).Single().Id
            });

            var doc = _context.Documents.Include(d => d.DocumentType).Where(n => n.DocumentName == AFTER_UPDATE).Single();

            doc.DocumentName.ShouldBe(AFTER_UPDATE);
            doc.Description.ShouldBe(AFTER_UPDATE_DESCRIPTION);
            doc.DocumentType.Name.ShouldBe(AFTER_UPDATE_TYPE);
        }

        [Fact]
        public async Task UpdateDocumentToNullDocumentTypeTest()
        {
            _context.Documents.Any(d => d.DocumentName == AFTER_UPDATE).ShouldBe(false);

            await _mediator.Send(new UpdateDocumentCommand
            {
                DocumentId = _context.Documents.Where(d => d.DocumentName == UPDATE).Single().DocumentId,
                DocumentName = AFTER_UPDATE,
                Description = AFTER_UPDATE_DESCRIPTION,
                DocumentTypeId = null
            });

            var doc = _context.Documents.Include(d => d.DocumentType).Where(n => n.DocumentName == AFTER_UPDATE).Single();

            doc.DocumentName.ShouldBe(AFTER_UPDATE);
            doc.Description.ShouldBe(AFTER_UPDATE_DESCRIPTION);
            doc.DocumentType.ShouldBe(null);
        }

        [Fact]
        public void DeleteDocumentTest()
        {
            var doc = _context.Documents.Single(d => d.DocumentName == DELETE && d.IsDelete == false);

            var result = _mediator.Send(new DeleteDocumentCommand
            {
                DocumentId = doc.DocumentId
            }).Result;

            result.ShouldBeOfType(typeof(Unit));

            _context.Documents.Find(doc.DocumentId).ShouldNotBeNull();

            doc.IsDelete.ShouldBe(true);
        }
    }
}