using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Diagnostics;
using System;

namespace ParentEspoir.Application
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public CreateDocumentCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = new Document
            {
                DocumentName = request.DocumentName,
                Description = request.Description,
                Path = SaveFileToDisk(request.File, request.CustomerId),
                DocumentType = await _context.DocumentTypes.FindAsync(request.DocumentTypeId),
                Customer = await _context.Customers.FindAsync(request.CustomerId)
            };

            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        /// <summary>
        /// This function save the IFormFile to the disk and return the path of
        /// the document saved.
        /// </summary>
        /// <param name="file">The file recieved from a HTML form</param>
        /// <param name="customerId">The id of the customer is user to build the path</param>
        /// <returns>The path of the document saved</returns>
        private static string SaveFileToDisk(IFormFile file, int customerId)
        {
            if (!Directory.Exists($"wwwroot/documents/customer/{customerId}"))
            {
                Directory.CreateDirectory($"wwwroot/documents/customer/{customerId}");
            }

            var guid = Guid.NewGuid().ToString();
            guid = guid.Remove(guid.Length / 2);
            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}{guid}{Path.GetExtension(file.FileName)}";
            
            var filePath = $"wwwroot/documents/customer/{customerId}/{uniqueFileName}";

            file.CopyTo(new FileStream(filePath, FileMode.Create));

            return $"/documents/customer/{customerId}/{uniqueFileName}";
        }
    }
}