using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class VolunteeringConfiguration : IEntityTypeConfiguration<Volunteering>
    {
        public void Configure(EntityTypeBuilder<Volunteering> builder)
        {
            builder.HasOne(v => v.Type).
                WithMany(v => v.Volunteerings).
                HasForeignKey(v => v.VolonteeringTypeId);

            builder.Property(v => v.Title).HasMaxLength(VolunteeringConstant.MAXTITLELENGHTDB);

        }
    }
}
