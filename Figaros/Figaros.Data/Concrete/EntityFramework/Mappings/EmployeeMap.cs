using Figaros.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figaros.Data.Concrete.EntityFramework.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.ImageString).IsRequired();
            builder.Property(a => a.ImageString).HasMaxLength(100);
            builder.Property(a => a.FullName).IsRequired();
            builder.Property(a => a.FullName).HasMaxLength(150);
            builder.Property(a => a.InstagramUrl).IsRequired();
            builder.Property(a => a.InstagramUrl).HasMaxLength(100);
            builder.Property(a => a.FacebookUrl).IsRequired();
            builder.Property(a => a.FacebookUrl).HasMaxLength(100);
            builder.Property(a => a.TiktokUrl).IsRequired();
            builder.Property(a => a.TiktokUrl).HasMaxLength(100);
            builder.Property(a => a.WhatsAppUrl).IsRequired();
            builder.Property(a => a.WhatsAppUrl).HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(500);

            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(150);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(150);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();
            builder.HasOne<Profession>(a => a.Profession).WithMany(a => a.Employees).HasForeignKey(a => a.ProfessionId);
            builder.ToTable("Employees");
        }
    }
}
