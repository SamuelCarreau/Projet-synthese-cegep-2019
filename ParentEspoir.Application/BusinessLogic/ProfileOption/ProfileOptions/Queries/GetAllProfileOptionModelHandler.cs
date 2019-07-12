using MediatR;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ParentEspoir.Application
{
    public class GetAllProfileOptionModelHandler : IRequestHandler<GetAllProfileOptionQuery, GetAllProfileOptionModel>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IMemoryCache _cache;

        public GetAllProfileOptionModelHandler(ParentEspoirDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }

        public async Task<GetAllProfileOptionModel> Handle(GetAllProfileOptionQuery request, CancellationToken cancellationToken)
        {
            GetAllProfileOptionModel profilOptions = new GetAllProfileOptionModel();

            profilOptions.Add(typeof(Availability).Name, await GetProfilOption<Availability>());
            profilOptions.Add(typeof(ChildrenAgeBracket).Name, await GetProfilOption<ChildrenAgeBracket>());
            profilOptions.Add(typeof(CitizenStatus).Name, await GetProfilOption<CitizenStatus>());
            profilOptions.Add(typeof(DocumentType).Name, await GetProfilOption<DocumentType>());
            profilOptions.Add(typeof(FamilyType).Name, await GetProfilOption<FamilyType>());
            profilOptions.Add(typeof(HeardOfUsFrom).Name, await GetProfilOption<HeardOfUsFrom>());
            profilOptions.Add(typeof(HomeType).Name, await GetProfilOption<HomeType>());
            profilOptions.Add(typeof(IncomeSource).Name, await GetProfilOption<IncomeSource>());
            profilOptions.Add(typeof(Language).Name, await GetProfilOption<Language>());
            profilOptions.Add(typeof(LegalCustody).Name, await GetProfilOption<LegalCustody>());
            profilOptions.Add(typeof(MaritalStatus).Name, await GetProfilOption<MaritalStatus>());
            profilOptions.Add(typeof(NoteType).Name, await GetProfilOption<NoteType>());
            profilOptions.Add(typeof(Parent).Name, await GetProfilOption<Parent>());
            profilOptions.Add(typeof(ReferenceType).Name, await GetProfilOption<ReferenceType>());
            profilOptions.Add(typeof(Schooling).Name, await GetProfilOption<Schooling>());
            profilOptions.Add(typeof(Sex).Name, await GetProfilOption<Sex>());
            profilOptions.Add(typeof(SkillToDevelop).Name, await GetProfilOption<SkillToDevelop>());
            profilOptions.Add(typeof(SocialService).Name, await GetProfilOption<SocialService>());
            profilOptions.Add(typeof(TransportType).Name, await GetProfilOption<TransportType>());
            profilOptions.Add(typeof(VolunteeringType).Name, await GetProfilOption<VolunteeringType>());
            profilOptions.Add(typeof(YearlyIncome).Name, await GetProfilOption<YearlyIncome>());

            return profilOptions;
        }

        private async Task<List<IProfileOption>> GetProfilOption<T>() where T : class, IProfileOption
        {
            string key = $"{typeof(T).Name}List";

            if (!_cache.TryGetValue(key, out List<IProfileOption> profilOption))
            {
                profilOption = await _context.Set<T>().Where(po => po.IsDelete == false).ToListAsync<IProfileOption>();

                _cache.Set(key, profilOption, TimeSpan.FromHours(3));
            }

            return profilOption;
        }
    }
}
