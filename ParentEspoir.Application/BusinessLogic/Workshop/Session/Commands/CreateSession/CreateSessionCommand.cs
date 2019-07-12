using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Domain.Enums;

namespace ParentEspoir.Application
{
    public class CreateSessionCommand : IRequest
    {
        public int? Year { get; set; }
        public Season? Season { get; set; }
    }
}