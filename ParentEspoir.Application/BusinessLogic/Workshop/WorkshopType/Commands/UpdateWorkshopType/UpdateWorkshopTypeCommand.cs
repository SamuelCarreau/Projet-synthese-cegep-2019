using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class UpdateWorkshopTypeCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
