using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class GetWorkshopTypeListQuery : IRequest<IEnumerable<WorkshopTypeModel>>
    {

    }
}
