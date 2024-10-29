using E_Restaurant.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Restaurant.EntityCongigurations
{
    public class LookupItemEntityTypeConfiguration : IEntityTypeConfiguration<LookupItem>
    {
        public void Configure(EntityTypeBuilder<LookupItem> builder)
        {
            builder.ToTable("LookupItems");
            builder.HasKey(x => x.Id);
            //Set Identity 
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getdate()");

            builder.Property(x => x.Name).IsUnicode(false);


        }
    }
}
