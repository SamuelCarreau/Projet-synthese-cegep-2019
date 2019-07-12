using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class PersonnalFollowUpConfiguration : IEntityTypeConfiguration<PersonnalFollowUp>
    {
        public void Configure(EntityTypeBuilder<PersonnalFollowUp> builder)
        {

        }
    }
}
