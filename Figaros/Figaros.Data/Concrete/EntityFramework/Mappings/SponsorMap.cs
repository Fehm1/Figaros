using Figaros.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figaros.Data.Concrete.EntityFramework.Mappings
{
    public class SponsorMap : IEntityTypeConfiguration<Sponsor>
    {
        public void Configure(EntityTypeBuilder<Sponsor> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.CompanyImageString).IsRequired();
            builder.Property(a => a.CompanyImageString).HasMaxLength(100);

            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(150);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(150);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();
            builder.ToTable("Sponsors");
        }
    }
}
