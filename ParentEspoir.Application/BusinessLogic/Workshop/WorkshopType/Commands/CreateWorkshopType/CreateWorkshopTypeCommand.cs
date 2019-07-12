using MediatR;

namespace ParentEspoir.Application
{
    public class CreateWorkshopTypeCommand : IRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
