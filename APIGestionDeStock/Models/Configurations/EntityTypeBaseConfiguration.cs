using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace APIGestionDeStock.Models.Configurations
{
    public abstract class EntityTypeBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseModel
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigurateProperties(builder);
            ConfigurateConstrains(builder);
            ConfigurateTableName(builder);
        }
        protected abstract void ConfigurateTableName(EntityTypeBuilder<T> builder);
        protected abstract void ConfigurateConstrains(EntityTypeBuilder<T> builder);
        protected abstract void ConfigurateProperties(EntityTypeBuilder<T> builder);
    }
}
