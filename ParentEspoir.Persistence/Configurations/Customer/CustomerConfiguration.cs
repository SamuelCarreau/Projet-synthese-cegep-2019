using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Persistence.Configurations
{
    class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasOne(c => c.CustomerDescription).WithOne(cd => cd.Customer)
                .HasForeignKey<CustomerDescription>(cd => cd.CustomerDescriptionId);

            builder.HasOne(cd => cd.Member).WithOne(m => m.Customer)
               .HasForeignKey<Member>(m => m.MemberId);

            builder.HasMany(c => c.WorkshopParticipations)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            builder.Property(c => c.FirstName)
                .HasMaxLength(CustomerConstant.FIRST_NAME_MAX_LENGHT);

            builder.Property(c => c.LastName)
                .HasMaxLength(CustomerConstant.LAST_NAME_MAX_LENGHT);

            builder.Property(c => c.Address)
                .HasMaxLength(CustomerConstant.ADDRESS_MAX_LENGHT);

            builder.Property(c => c.City)
                .HasMaxLength(CustomerConstant.CITY_MAX_LENGHT);

            builder.Property(c => c.PostalCode)
                .HasMaxLength(CustomerConstant.POSTAL_CODE_MAX_LENGHT);

            builder.Property(c => c.Province)
                .HasMaxLength(CustomerConstant.PROVINCE_MAX_LENGHT);

            builder.Property(c => c.Country)
                .HasMaxLength(CustomerConstant.COUNTRY_MAX_LENGHT);

            builder.Property(c => c.Phone)
                .HasMaxLength(CustomerConstant.PHONE_MAX_LENGHT);

            builder.Property(c => c.SecondaryPhone)
                .HasMaxLength(CustomerConstant.SECONDARY_PHONE_MAX_LENGHT);
        }
    }
}
