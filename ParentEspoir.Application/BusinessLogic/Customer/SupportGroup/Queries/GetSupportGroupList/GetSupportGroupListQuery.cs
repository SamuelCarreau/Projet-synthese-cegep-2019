using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetSupportGroupListQuery : IRequest<IEnumerable<SupportGrougModel>>
    {
        
    }
}