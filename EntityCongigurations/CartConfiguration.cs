using E_Restaurant.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.EntityCongigurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.TotalAmount).HasColumnType("decimal(18,2)");
            builder.HasOne<Person>()
                   .WithMany()
                   .HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
