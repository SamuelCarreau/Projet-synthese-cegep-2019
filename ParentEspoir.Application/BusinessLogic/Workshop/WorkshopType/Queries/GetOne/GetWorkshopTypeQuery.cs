using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class GetWorkshopTypeQuery : IRequest<WorkshopTypeModel>
    {
        public int Id { get; set; }
    }
}
