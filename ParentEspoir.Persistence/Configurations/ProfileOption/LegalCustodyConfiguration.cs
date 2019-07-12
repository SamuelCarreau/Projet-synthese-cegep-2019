using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class LegalCustodyConfiguration : IEntityTypeConfiguration<LegalCustody>
    {
        public void Configure(EntityTypeBuilder<LegalCustody> builder)
        {

        }
    }
}
