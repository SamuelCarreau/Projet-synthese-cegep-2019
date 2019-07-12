using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class SupportGroupConfiguration : IEntityTypeConfiguration<SupportGroup>
    {
        public void Configure(EntityTypeBuilder<SupportGroup> builder)
        {
            builder.Property(s => s.Name)
                .HasMaxLength(SupportGroupConstant.NAME_MAX_LENGHT);

            builder.HasOne(s => s.User)
                .WithMany(u => u.SupportGroup)
                .HasForeignKey(s => s.UserId);
        }
    }
}
