using MediatR;

namespace ParentEspoir.Application
{
    public class GetRolesQuery : IRequest<GetRolesModel>
    {
        public string UserId { get; set; }
    }
}
