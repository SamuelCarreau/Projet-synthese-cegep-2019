using MediatR;
using ParentEspoir.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class UpdateProfilOptionCommand<TProfilOption> : IRequest where TProfilOption : class, IProfileOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
