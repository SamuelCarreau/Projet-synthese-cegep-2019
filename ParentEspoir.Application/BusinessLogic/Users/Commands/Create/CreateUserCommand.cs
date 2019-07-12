using MediatR;

namespace ParentEspoir.Application
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
