using MediatR;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Application.Users;
using ParentEspoir.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class GetUserQuery : IRequest<AppUser>
    {
        public string Id { get; set; }
    }
}
