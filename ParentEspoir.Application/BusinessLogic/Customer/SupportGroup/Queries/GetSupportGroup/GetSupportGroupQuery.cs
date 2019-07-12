using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetSupportGroupQuery : IRequest<SupportGroup>
    {
        public int SupportGroupId { get; set; }
    }
}