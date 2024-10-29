using E_Restaurant.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.EntityCongigurations
{
    public class TableReservationConfiguration : IEntityTypeConfiguration<TableReservation>
    {
        public void Configure(EntityTypeBuilder<TableReservation> builder)
        {
            builder.ToTable("TableReservations");
            builder.HasKey(tr => tr.Id);
            builder.Property(tr => tr.CreationDate).IsRequired();
            builder.Property(tr => tr.NumberOfGuests).IsRequired();
            builder.Property(tr => tr.TableNumber).IsRequired().HasMaxLength(50);
            builder.Property(tr => tr.Status).IsRequired().HasMaxLength(50);
            builder.HasOne<Person>()
                   .WithMany()
                   .HasForeignKey(tr => tr.CustomerId);
        }
    }
}
