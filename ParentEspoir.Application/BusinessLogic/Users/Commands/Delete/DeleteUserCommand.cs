using MediatR;

namespace ParentEspoir.Application
{
    public class DeleteUserCommand : IRequest
    {
        public string Id { get; set; }
    }
}
