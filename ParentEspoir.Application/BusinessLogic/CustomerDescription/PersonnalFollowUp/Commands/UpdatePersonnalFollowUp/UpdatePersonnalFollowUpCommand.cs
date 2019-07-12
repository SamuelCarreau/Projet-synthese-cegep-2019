using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class UpdatePersonnalFollowUpCommand : IRequest
    {
        public int PersonnalFollowUpId { get; set; }
    }
}