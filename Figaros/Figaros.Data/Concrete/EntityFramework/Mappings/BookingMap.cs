using Figaros.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figaros.Data.Concrete.EntityFramework.Mappings
{
    public class BookingMap : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Fullname).IsRequired();
            builder.Property(a => a.Fullname).HasMaxLength(150);
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Email).HasMaxLength(150);
            builder.Property(a => a.Phone).IsRequired();
            builder.Property(a => a.Phone).HasMaxLength(50);
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a => a.Message).IsRequired();
            builder.Property(a => a.Message).HasMaxLength(500);
            builder.Property(a => a.IsCompleted).HasDefaultValue(false).IsRequired();

            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(150);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(150);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();
            builder.HasOne<Employee>(a => a.Employee).WithMany(a => a.Bookings).HasForeignKey(a => a.EmployeeId);
            builder.HasOne<Price>(a => a.Price).WithMany(a => a.Bookings).HasForeignKey(a => a.PriceId);
            builder.HasOne<Time>(a => a.Time).WithMany(a => a.Bookings).HasForeignKey(a => a.TimeId);
            builder.ToTable("Bookings");
        }
    }
}
