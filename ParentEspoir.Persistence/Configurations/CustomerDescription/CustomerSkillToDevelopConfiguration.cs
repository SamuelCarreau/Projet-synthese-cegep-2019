using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    public class CustomerSkillToDevelopConfiguration : IEntityTypeConfiguration<CustomerSkillToDevelop>
    {
        public void Configure(EntityTypeBuilder<CustomerSkillToDevelop> builder)
        {
            builder.HasKey(c => new { c.CustomerId, c.SkillId });
        }
    }
}
