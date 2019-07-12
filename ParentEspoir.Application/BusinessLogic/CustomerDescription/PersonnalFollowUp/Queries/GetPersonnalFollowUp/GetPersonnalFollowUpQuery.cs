using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetPersonnalFollowUpQuery : IRequest<GetPersonnalFollowUpModel>
    {
        public int PersonnalFollowUpId { get; set; }
    }
}