using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application.Infrastructure
{
    public class RequestLogBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ParentEspoirDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMemoryCache _cache;

        public RequestLogBehaviour(
            ParentEspoirDbContext context, 
            UserManager<AppUser> userManager, 
            IHttpContextAccessor httpContext,
            IMemoryCache memoryCache)
        {
            _timer = new Stopwatch();
            _context = context;
            _userManager = userManager;
            _httpContext = httpContext;
            _cache = memoryCache;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            await Log(cancellationToken, request);

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                await Log(cancellationToken, request, $"The request take {_timer.ElapsedMilliseconds} miliseconds");
            }

            return response;
        }

        private async Task Log(CancellationToken cancellationToken, TRequest request ,string info = null)
        {
            var username = _httpContext.HttpContext?.User?.Identity.Name;
            string userId = null;

            if (username != null)
            {
                if (!_cache.TryGetValue<string>(username, out userId))
                {
                    userId = (await _userManager.FindByNameAsync(username)).Id;
                    _cache.CreateEntry(username);
                    _cache.Set<string>(username, userId);
                }
            }

            await _context.AddAsync(new Log
            {
                UserId = userId,
                UserName = username,
                DateTime = DateTime.Now,
                CommandName = CommandName(typeof(TRequest)),
                CommandJSON = JsonConvert.SerializeObject(request),
                Information = info
            });

            await _context.SaveChangesAsync(cancellationToken);
        }

        private string CommandName(Type type)
        {
            StringBuilder sb = new StringBuilder();

            if (type.GenericTypeArguments.Length > 0)
            {
                sb.Append(type.Name.Remove(type.Name.IndexOf("`")));
                foreach (var genericType in type.GenericTypeArguments)
                {
                    sb.Append($"<{genericType.Name}>");
                }
            }
            else
            {
                sb.Append(type.Name);
            }

            return sb.ToString();
        }
    }
}
