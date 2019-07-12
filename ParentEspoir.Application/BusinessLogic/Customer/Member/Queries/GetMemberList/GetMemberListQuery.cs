using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetMemberListQuery : IRequest<System.Collections.Generic.IEnumerable<Member>>
    {
        public int MemberId { get; set; }
    }
}