using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence
{
    public class ParentEspoirDbContext : IdentityDbContext<AppUser>
    {
        public ParentEspoirDbContext(DbContextOptions<ParentEspoirDbContext> options)
            : base(options)
        {
        }
        
        #region DbSets<>
        
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<ChildrenAgeBracket> ChildrenAgeBrackets { get; set; }
        public DbSet<CitizenStatus> CitizenStatuses { get; set; }
        public DbSet<CustomerChildrenAgeBracket> CustomerChildrenAgeBrackets { get; set; }
        public DbSet<CustomerDescription> CustomerDescriptions { get; set; }
        public DbSet<CustomerSocialService> CustomerSocialServices { get; set; }
        public DbSet<CustomerSkillToDevelop> CustomerSkillToDevelops { get; set; }
        public DbSet<FamilyType> FamilyTypes { get; set; }
        public DbSet<HomeType> HomeTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LegalCustody> LegalCustodies { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<PersonnalFollowUp> PersonnalFollowUps { get; set; }
        public DbSet<PreferedDay> PreferedDays { get; set; }
        public DbSet<Pregnancy> Pregnancies { get; set; }
        public DbSet<YearlyIncome> YearlyIncomes { get; set; }
        public DbSet<IncomeSource> IncomeSources { get; set; }
        public DbSet<Schooling> Schoolings { get; set; }
        public DbSet<Sex> Sexs { get; set; }
        public DbSet<SkillToDevelop> SkillToDevelops { get; set; }
        public DbSet<SocialService> SocialServices { get; set; }
        public DbSet<TransportType> TransportTypes { get; set; }

        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteType> NoteTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HeardOfUsFrom> HeardOfUsFroms { get; set; }
        public DbSet<ReferenceType> ReferenceTypes { get; set; }
        public DbSet<SupportGroup> SupportGroups { get; set; }
        public DbSet<CustomerActivation> CustomerActivation { get; set; }

        public DbSet<Objective> Objectives { get; set; }

        public DbSet<Volunteering> Volunteerings { get; set; }
        public DbSet<VolunteeringType> VolunteeringTypes { get; set; }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<WorkshopType> WorkshopTypes { get; set; }

        public DbSet<Log> Logs { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParentEspoirDbContext).Assembly);
        }
    }
}
