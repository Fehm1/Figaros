﻿using Figaros.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figaros.Data.Concrete.EntityFramework.Mappings
{
    public class ServiceMap : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.ImageString).IsRequired();
            builder.Property(a => a.ImageString).HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Title).HasMaxLength(150);
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(500);
            builder.Property(a => a.IsPoster).IsRequired();
            builder.Property(a => a.IsPoster).HasDefaultValue(false);

            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(150);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(150);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();
            builder.ToTable("Services");
        }
    }
}
