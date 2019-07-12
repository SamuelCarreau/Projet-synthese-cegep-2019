using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class VolunteeringTypeConfiguration : IEntityTypeConfiguration<VolunteeringType>
    {
        public void Configure(EntityTypeBuilder<VolunteeringType> builder)
        {
            
        }
    }
}
