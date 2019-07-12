using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Persistence.Configurations
{
    class CustomerDescriptionConfiguration : IEntityTypeConfiguration<CustomerDescription>
    {
        public void Configure(EntityTypeBuilder<CustomerDescription> builder)
        {
            builder.HasOne(cd => cd.PersonnalFollowUp).WithOne(pf => pf.CustomerDescription)
               .HasForeignKey<PersonnalFollowUp>(pf => pf.PersonnalFollowUpId);

            builder.HasOne(cd => cd.Pregnancy).WithOne(p => p.CustomerDescription)
                .HasForeignKey<Pregnancy>(p => p.PregnancyId);

            builder.Property(cd => cd.ChildrenCount).IsRequired().HasDefaultValue(0);
        }
    }
}
