﻿using Figaros.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figaros.Data.Concrete.EntityFramework.Mappings
{
    public class SettingMap : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.HeaderLogo).IsRequired();
            builder.Property(a => a.HeaderLogo).HasMaxLength(100);
            builder.Property(a => a.FooterLogo).IsRequired();
            builder.Property(a => a.FooterLogo).HasMaxLength(100);
            builder.Property(a => a.FooterDescription).IsRequired();
            builder.Property(a => a.FooterDescription).HasMaxLength(300);
            builder.Property(a => a.OurService).IsRequired();
            builder.Property(a => a.OurService).HasMaxLength(150);
            builder.Property(a => a.OurServiceDescription).IsRequired();
            builder.Property(a => a.OurServiceDescription).HasMaxLength(300);
            builder.Property(a => a.OurPrice).IsRequired();
            builder.Property(a => a.OurPrice).HasMaxLength(150);
            builder.Property(a => a.OurPriceDescription).IsRequired();
            builder.Property(a => a.OurPriceDescription).HasMaxLength(300);
            builder.Property(a => a.OurBarber).IsRequired();
            builder.Property(a => a.OurBarber).HasMaxLength(150);
            builder.Property(a => a.OurBarberDescription).IsRequired();
            builder.Property(a => a.OurBarberDescription).HasMaxLength(300);
            builder.Property(a => a.Location).IsRequired();
            builder.Property(a => a.Location).HasMaxLength(300);
            builder.Property(a => a.MondayFridayWorkHours).IsRequired();
            builder.Property(a => a.MondayFridayWorkHours).HasMaxLength(100);
            builder.Property(a => a.SaturdayWorkHours).IsRequired();
            builder.Property(a => a.SaturdayWorkHours).HasMaxLength(100);
            builder.Property(a => a.SundayWorkHours).IsRequired();
            builder.Property(a => a.SundayWorkHours).HasMaxLength(100);
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Email).HasMaxLength(200);
            builder.Property(a => a.Phone).IsRequired();
            builder.Property(a => a.Phone).HasMaxLength(50);
            builder.Property(a => a.InstagramUrl).IsRequired();
            builder.Property(a => a.InstagramUrl).HasMaxLength(100);
            builder.Property(a => a.FacebookUrl).IsRequired();
            builder.Property(a => a.FacebookUrl).HasMaxLength(100);
            builder.Property(a => a.WhatsAppUrl).IsRequired();
            builder.Property(a => a.WhatsAppUrl).HasMaxLength(100);
            builder.Property(a => a.YoutubeUrl).IsRequired();
            builder.Property(a => a.YoutubeUrl).HasMaxLength(100);
            builder.Property(a => a.TwitterUrl).IsRequired();
            builder.Property(a => a.TwitterUrl).HasMaxLength(100);
            builder.Property(a => a.TiktokUrl).IsRequired();
            builder.Property(a => a.TiktokUrl).HasMaxLength(100);
            builder.Property(a => a.IsActiceRequest).HasDefaultValue(true).IsRequired();

            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(150);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(150);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();
            builder.ToTable("Settings");
        }
    }
}
