using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentEspoir.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Persistence.Configurations
{
    public class PreferedDayConfiguration : IEntityTypeConfiguration<PreferedDay>
    {
        public void Configure(EntityTypeBuilder<PreferedDay> builder)
        {
            builder.HasKey(pd => new { pd.CustomerDescriptionID, pd.Day });
        }
    }
}
