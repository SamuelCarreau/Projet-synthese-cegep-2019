using MediatR;
using ParentEspoir.Domain.Entities;
using System.Collections.Generic;

namespace ParentEspoir.Application
{
    public class GetUsersQuery : IRequest<IEnumerable<AppUser>>
    {

    }
}
