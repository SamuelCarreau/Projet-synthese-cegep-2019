using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Persistence.Configurations
{
     public class CustomerChildrenAgeBracketConfiguration : IEntityTypeConfiguration<CustomerChildrenAgeBracket>
    {
        public void Configure(EntityTypeBuilder<CustomerChildrenAgeBracket> builder)
        {
            builder.HasKey(c => new { c.AgeBracketId, c.CustomerId });
        }
    }
}
