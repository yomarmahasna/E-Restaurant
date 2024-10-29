using E_Restaurant.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.EntityCongigurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems");
            builder.HasKey(mi => mi.Id);
            builder.Property(mi => mi.Name).IsRequired().HasMaxLength(100);
            builder.Property(mi => mi.Description).HasMaxLength(500);
            builder.Property(mi => mi.Price).HasColumnType("decimal(18,2)");
           
        }
    }
}
