using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class GetSeanceListQuery : IRequest<SeanceListModel>
    {
        public int WorkshopId { get; set; }
    }
}
