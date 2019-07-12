using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class HomeTypeConfiguration : IEntityTypeConfiguration<HomeType>
    {
        public void Configure(EntityTypeBuilder<HomeType> builder)
        {

        }
    }
}
