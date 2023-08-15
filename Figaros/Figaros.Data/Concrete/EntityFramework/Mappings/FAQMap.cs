using Figaros.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figaros.Data.Concrete.EntityFramework.Mappings
{
    public class FAQMap : IEntityTypeConfiguration<FAQ>
    {
        public void Configure(EntityTypeBuilder<FAQ> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Question).IsRequired();
            builder.Property(a => a.Question).HasMaxLength(200);
            builder.Property(a => a.Answer).IsRequired();
            builder.Property(a => a.Answer).HasMaxLength(500);

            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(150);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(150);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();
            builder.ToTable("FAQs");
        }
    }
}
