using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    public class CustomerSocialServiceConfiguration : IEntityTypeConfiguration<CustomerSocialService>
    {
        public void Configure(EntityTypeBuilder<CustomerSocialService> builder)
        {
            builder.HasKey(c => new { c.CustomerId, c.SocialServiceId });
        }
    }
}
