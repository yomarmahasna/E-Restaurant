using E_Restaurant.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.EntityCongigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Rating).IsRequired();
            builder.Property(r => r.Comment).HasMaxLength(500);
            builder.Property(r => r.ReviewDate).IsRequired();
            builder.HasOne<MenuItem>()
                   .WithMany()
                   .HasForeignKey(r => r.MenuItemId);
            builder.HasOne<Person>()
                   .WithMany()
                   .HasForeignKey(r => r.CustomerId);
        }
    }
}
