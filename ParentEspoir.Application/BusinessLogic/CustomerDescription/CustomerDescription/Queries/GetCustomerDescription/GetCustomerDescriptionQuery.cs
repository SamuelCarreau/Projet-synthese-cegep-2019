using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetCustomerDescriptionQuery : IRequest<GetCustomerDescriptionModel>
    {
        public int CustomerDescriptionId { get; set; }
    }
}