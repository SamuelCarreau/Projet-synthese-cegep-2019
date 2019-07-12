using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetSeanceQuery : IRequest<GetSeanceModel>
    {
        public int SeanceId { get; set; }
    }
}