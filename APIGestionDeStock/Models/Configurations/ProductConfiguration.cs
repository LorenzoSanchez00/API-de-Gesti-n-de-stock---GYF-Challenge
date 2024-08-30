using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIGestionDeStock.Models.Configurations
{
    public class ProductConfiguration : EntityTypeBaseConfiguration<Product>
    {
        protected override void ConfigurateConstrains(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
        }

        protected override void ConfigurateProperties(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(p => p.Date).IsRequired().HasColumnType("date");
            builder.Property(p => p.Category).IsRequired();
        }

        protected override void ConfigurateTableName(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
        }
    }
}
