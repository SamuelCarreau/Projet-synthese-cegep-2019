using MediatR;

namespace ParentEspoir.Application
{
    public class DeleteWorkshopTypeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
