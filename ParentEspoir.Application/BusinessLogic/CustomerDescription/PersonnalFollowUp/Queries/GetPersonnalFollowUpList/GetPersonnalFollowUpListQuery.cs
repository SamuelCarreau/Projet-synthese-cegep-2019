using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetPersonnalFollowUpListQuery : IRequest<GetPersonnalFollowUpListModel>
    {
        public int PersonnalFollowUpId { get; set; }
    }
}