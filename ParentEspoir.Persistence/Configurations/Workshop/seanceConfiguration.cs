using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class SeanceConfiguration : IEntityTypeConfiguration<Seance>
    {
        public void Configure(EntityTypeBuilder<Seance> builder)
        {
            builder.HasOne(s => s.Workshop)
                .WithMany(w => w.Seances)
                .HasForeignKey(s => s.WorkshopId);

            builder.HasMany(s => s.Participants)
                .WithOne(w => w.Seance)
                .HasForeignKey(s => s.SeanceId);
        }
    }
}
