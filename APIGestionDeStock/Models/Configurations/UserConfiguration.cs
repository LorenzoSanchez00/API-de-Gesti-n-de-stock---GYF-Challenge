using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIGestionDeStock.Models.Configurations
{
    public class UserConfiguration : EntityTypeBaseConfiguration<User>
    {
        protected override void ConfigurateConstrains(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
        }

        protected override void ConfigurateProperties(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(U =>U.Password).IsRequired().HasMaxLength(100);
        }

        protected override void ConfigurateTableName(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
        }
    }
}
