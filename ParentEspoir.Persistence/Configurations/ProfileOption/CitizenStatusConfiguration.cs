using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class CitizenStatusConfiguration : IEntityTypeConfiguration<CitizenStatus>
    {
        public void Configure(EntityTypeBuilder<CitizenStatus> builder)
        {

        }
    }
}
