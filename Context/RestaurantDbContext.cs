using E_Restaurant.Entities;
using E_Restaurant.EntityCongigurations;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.Context
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TableReservation> TableReservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<LookupType> LookupTypes { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new TableReservationConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new LookupTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LookupItemEntityTypeConfiguration());
        }
    }
}
