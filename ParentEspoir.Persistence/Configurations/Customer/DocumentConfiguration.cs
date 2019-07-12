using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(d => d.DocumentName)
                .HasMaxLength(DocumentConstant.NAME_MAX_LENGHT)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasMaxLength(DocumentConstant.DESCRIPTION_MAX_LENGHT);

            builder.Property(d => d.Path)
                .IsRequired();

            builder.HasOne(d => d.Customer)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.CustomerId);
        }
    }
}
