using Figaros.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figaros.Data.Concrete.EntityFramework.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.ImageString).IsRequired();
            builder.Property(a => a.ImageString).HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Title).HasMaxLength(200);
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(500);
            builder.Property(a => a.CostPrice).IsRequired();
            builder.Property(a => a.SalePrice).IsRequired();
            builder.Property(a => a.ProductAmount).IsRequired();
            builder.Property(a => a.SaleCount).IsRequired();
            builder.Property(a => a.DiscountPercent).IsRequired();
            builder.Property(a => a.IsNew).HasDefaultValue(true).IsRequired();
            builder.Property(a => a.IsPopular).HasDefaultValue(false).IsRequired();

            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(150);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(150);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();
            builder.ToTable("Products");
        }
    }
}
