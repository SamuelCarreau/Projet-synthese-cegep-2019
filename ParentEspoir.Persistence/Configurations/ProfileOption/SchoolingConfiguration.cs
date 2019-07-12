using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class SchoolingConfiguration : IEntityTypeConfiguration<Schooling>
    {
        public void Configure(EntityTypeBuilder<Schooling> builder)
        {

        }
    }
}
