using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class SocialServiceConfiguration : IEntityTypeConfiguration<SocialService>
    {
        public void Configure(EntityTypeBuilder<SocialService> builder)
        {

        }
    }
}
