using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class DeleteSupportGroupCommand : IRequest
    {
        public int SupportGroupId { get; set; }
    }
}