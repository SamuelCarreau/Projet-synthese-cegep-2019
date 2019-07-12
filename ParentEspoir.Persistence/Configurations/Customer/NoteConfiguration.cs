using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasOne(n => n.Customer)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CustomerId);

            builder.Property(n => n.NoteName)
                .IsRequired()
                .HasMaxLength(NoteConstant.NAME_MAX_LENGHT);

            builder.Property(n => n.CreationDate)
                .ValueGeneratedOnAdd();
        }
    }
}
