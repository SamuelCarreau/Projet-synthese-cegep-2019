using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.HasMany(w => w.Seances)
                .WithOne(s => s.Workshop)
                .HasForeignKey(w => w.WorkshopId);

            builder.HasOne(w => w.Session)
                .WithMany(s => s.Workshops)
                .HasForeignKey(w => w.SessionId);
        }
    }
}
