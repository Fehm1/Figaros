using Figaros.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figaros.Data.Concrete.EntityFramework.Mappings
{
    public class RequestMap : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Fullname).IsRequired();
            builder.Property(a => a.Fullname).HasMaxLength(200);
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Email).HasMaxLength(200);
            builder.Property(a => a.Phone).IsRequired();
            builder.Property(a => a.Phone).HasMaxLength(50);
            builder.Property(a => a.Message).IsRequired();
            builder.Property(a => a.Message).HasMaxLength(500);
            builder.Property(a => a.CV).IsRequired();
            builder.Property(a => a.CV).HasMaxLength(100);
            builder.Property(a => a.IntagramURl).IsRequired();
            builder.Property(a => a.IntagramURl).HasMaxLength(200);
            builder.Property(a => a.TiktokUrl).IsRequired();
            builder.Property(a => a.TiktokUrl).HasMaxLength(200);

            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(150);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(150);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();
            builder.ToTable("Requests");
        }
    }
}
