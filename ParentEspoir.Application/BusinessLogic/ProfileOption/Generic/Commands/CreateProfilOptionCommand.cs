using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class CreateProfilOptionCommand<TProfilOption> : IRequest where TProfilOption : class, IProfileOption
    {
        public string Name { get; set; }
    }
}