using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class ChildrenAgeBracketConfiguration : IEntityTypeConfiguration<ChildrenAgeBracket>
    {
        public void Configure(EntityTypeBuilder<ChildrenAgeBracket> builder)
        {
            
        }
    }
}
