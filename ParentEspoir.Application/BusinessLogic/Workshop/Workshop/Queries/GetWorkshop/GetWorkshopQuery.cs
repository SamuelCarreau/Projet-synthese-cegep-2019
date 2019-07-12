using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetWorkshopQuery : IRequest<GetWorkshopModel>
    {
        public int WorkshopId { get; set; }
    }
}