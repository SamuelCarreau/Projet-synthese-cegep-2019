using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParentEspoir.Application
{
    public class CreateSupportGroupCommand : IRequest
    {
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
    }
}