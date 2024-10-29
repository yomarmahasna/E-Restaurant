using E_Restaurant.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.EntityCongigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CreationDate).IsRequired();
            builder.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Property(o => o.Status).IsRequired().HasMaxLength(50);
            builder.HasOne<Person>()
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
