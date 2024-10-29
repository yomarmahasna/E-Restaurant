using E_Restaurant.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace E_Restaurant.EntityCongigurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Description).HasMaxLength(500);
            //builder.HasMany<Category>().WithOne().HasForeignKey(x => x.MenuId);
        }
    }
}
