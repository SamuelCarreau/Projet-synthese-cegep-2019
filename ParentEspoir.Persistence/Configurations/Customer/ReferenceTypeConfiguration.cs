using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class ReferenceTypeConfiguration : IEntityTypeConfiguration<ReferenceType>
    {
        public void Configure(EntityTypeBuilder<ReferenceType> builder)
        {

        }
    }
}
