using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteSessionCommand : IRequest
    {
        public int SessionId { get; set; }
    }
}