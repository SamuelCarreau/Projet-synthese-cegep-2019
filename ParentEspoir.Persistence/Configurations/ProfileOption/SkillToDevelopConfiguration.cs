using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class SkillToDevelopConfiguration : IEntityTypeConfiguration<SkillToDevelop>
    {
        public void Configure(EntityTypeBuilder<SkillToDevelop> builder)
        {

        }
    }
}
