using MediatR;

namespace ParentEspoir.Application
{
    public class GetUserEditQuery : IRequest<UpdateUserCommand>
    {
        public string Id { get; set; }
    }
}
