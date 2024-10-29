using E_Restaurant.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Restaurant.EntityCongigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.HasOne<Menu>()
                   .WithMany()
                   .HasForeignKey(c => c.MenuId);
        }
    }
}
