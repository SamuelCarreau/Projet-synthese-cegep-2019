using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class WorkshopTypeConfiguration : IEntityTypeConfiguration<WorkshopType>
    {
        public void Configure(EntityTypeBuilder<WorkshopType> builder)
        {
            builder.Property(w => w.Name).HasMaxLength(WorkshopTypeConstant.NAME_MAX_LENGHT);

            builder.Property(w => w.Code).HasMaxLength(WorkshopTypeConstant.CODE_MAX_LENGHT);
        }
    }
}
