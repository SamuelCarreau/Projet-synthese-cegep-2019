using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasMany(s => s.Workshops)
                .WithOne(w => w.Session)
                .HasForeignKey(w => w.SessionId);
        }
    }
}
