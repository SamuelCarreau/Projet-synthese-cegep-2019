using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class HeardOfUsFromConfiguration : IEntityTypeConfiguration<HeardOfUsFrom>
    {
        public void Configure(EntityTypeBuilder<HeardOfUsFrom> builder)
        {

        }
    }
}
