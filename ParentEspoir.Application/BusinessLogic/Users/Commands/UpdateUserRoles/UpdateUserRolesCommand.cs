using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class UpdateUserRolesCommand : IRequest
    {
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
