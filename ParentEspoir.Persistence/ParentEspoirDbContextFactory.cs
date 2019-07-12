using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence.Infrastructure;

namespace ParentEspoir.Persistence
{
    class ParentEspoirDbContextFactory : DesignTimeDbContextFactoryBase<ParentEspoirDbContext>
    {
        protected override ParentEspoirDbContext CreateNewInstance(DbContextOptions<ParentEspoirDbContext> options)
        {
            return new ParentEspoirDbContext(options);
        }
    }
}
