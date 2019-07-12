using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ParentEspoir.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application.Infrastructure
{
    public class RequestCacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMemoryCache _memory;

        public RequestCacheBehaviour(IMemoryCache memory)
        {
            _memory = memory;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var type = typeof(TRequest);

            if (type.Name.Contains("ProfilOptionCommand"))
            {
                if (type.GetGenericArguments().First().GetInterfaces().Contains(typeof(IProfileOption)))
                {
                    _memory.Remove($"{type.GenericTypeArguments[0].Name}List");
                }
            }

            return await next();
        }
    }
}
