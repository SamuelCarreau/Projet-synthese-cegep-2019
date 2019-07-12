using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class FamilyTypeConfiguration : IEntityTypeConfiguration<FamilyType>
    {
        public void Configure(EntityTypeBuilder<FamilyType> builder)
        {

        }
    }
}
