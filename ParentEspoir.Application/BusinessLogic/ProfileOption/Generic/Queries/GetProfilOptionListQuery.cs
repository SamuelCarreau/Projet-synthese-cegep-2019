using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using ParentEspoir.Persistence;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace ParentEspoir.Application
{
    public class GetProfilOptionQuery<TProfilOption> : IRequest<IEnumerable<IProfileOption>> where TProfilOption : class, IProfileOption
    {
        public async Task<List<TProfilOption>> Handle(IMemoryCache memory, ParentEspoirDbContext context)
        {
            string optionName = $"{typeof(TProfilOption).Name}List";

            if (!memory.TryGetValue($"{optionName}", out List<TProfilOption> options))
            {
                options = await context.Set<TProfilOption>().Where(t => t.IsDelete == false).ToListAsync();

                memory.CreateEntry(optionName);
                memory.Set(optionName, options);
            }

            return options;
        }
    }
}