using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class RevenuConfiguration : IEntityTypeConfiguration<YearlyIncome>
    {
        public void Configure(EntityTypeBuilder<YearlyIncome> builder)
        {

        }
    }
}
