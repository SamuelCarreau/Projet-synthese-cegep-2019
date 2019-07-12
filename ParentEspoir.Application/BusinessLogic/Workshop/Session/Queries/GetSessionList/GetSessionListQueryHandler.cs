using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ParentEspoir.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class GetSessionListQueryHandler : IRequestHandler<GetSessionListQuery, SessionListViewModel>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMemoryCache _memory;

        public GetSessionListQueryHandler(ParentEspoirDbContext context, IHttpContextAccessor httpContext, IMemoryCache memory)
        {
            _context = context;
            _httpContext = httpContext;
            _memory = memory;
        }

        public async Task<SessionListViewModel> Handle(GetSessionListQuery request, CancellationToken cancellationToken)
        {
            if (!_memory.TryGetValue("SESSIONLIST", out SessionListViewModel model))
            {
                var list = await _context.Sessions
                .Where(session => session.IsDelete == false)
                .ToListAsync();

                list.Sort();

                model = new SessionListViewModel
                {
                    Sessions = list.Select(s => new SessionModel
                    {
                        SeasonName = TranslateSeason(s.Season),
                        SessionId = s.SessionId,
                        StartDate = s.StartDate,
                        Year = s.Year
                    })
                };

                _memory.Set("SESSIONLIST", model);
            }
            

            if (_httpContext.HttpContext != null && _httpContext.HttpContext.User != null)
            {
                model.UserCanManageSession = _httpContext.HttpContext.User.IsInRole("Administrateur");
            }
            else
            {
                model.UserCanManageSession = false;
            }

            return model;
        }

        private string TranslateSeason(Season season)
        {
            switch (season)
            {
                case Season.Fall:
                    return "Automne";
                case Season.Winter:
                    return "Hiver";
                case Season.Spring:
                    return "Printemps";
                case Season.Summer:
                    return "Été";
                default:
                    throw new System.InvalidOperationException(season.ToString() + " is not valid.");
            }
        }
    }
}