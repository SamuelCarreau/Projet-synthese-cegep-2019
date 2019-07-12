using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class GetWorkshopListQuery : IRequest<IEnumerable<WorkshopListElementModel>>
    {
        public int SessionId { get; set; }
    }
}
