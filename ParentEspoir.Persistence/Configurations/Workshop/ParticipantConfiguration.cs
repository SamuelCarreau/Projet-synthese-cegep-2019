using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasOne(p => p.Customer)
                .WithMany(c => c.WorkshopParticipations)
                .HasForeignKey(p => p.CustomerId);

            builder.HasOne(p => p.Seance)
                .WithMany(s => s.Participants)
                .HasForeignKey(p => p.SeanceId);
        }
    }
}
