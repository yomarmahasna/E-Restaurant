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
        public DbSet<MenuCategory> MenuCategories { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seeding LookupTypes
            modelBuilder.Entity<LookupType>().HasData(
                new LookupType { Id = 1, Name = "OrderStatus", IsActive = true, CreationDate = DateTime.Now },
                new LookupType { Id = 2, Name = "PaymentMethod", IsActive = true, CreationDate = DateTime.Now },
                new LookupType { Id = 3, Name = "ItemCategory", IsActive = true, CreationDate = DateTime.Now },
                new LookupType { Id = 4, Name = "UserRoles", IsActive = true, CreationDate = DateTime.Now }
            );

            // Seeding LookupItems
            modelBuilder.Entity<LookupItem>().HasData(
                // Order Status Lookup Items
                new LookupItem { Id = 1, Name = "Pending", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 1 },
                new LookupItem { Id = 2, Name = "Completed", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 1 },
                new LookupItem { Id = 3, Name = "Cancelled", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 1 },

                // Payment Method Lookup Items
                new LookupItem { Id = 4, Name = "Credit Card", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 2 },
                new LookupItem { Id = 5, Name = "Cash", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 2 },
                new LookupItem { Id = 6, Name = "Online Transfer", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 2 },

                // Item Category Lookup Items
                new LookupItem { Id = 7, Name = "Appetizers", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 3 },
                new LookupItem { Id = 8, Name = "Main Course", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 3 },
                new LookupItem { Id = 9, Name = "Desserts", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 3 },

                // User Roles Lookup Items
                new LookupItem { Id = 10, Name = "Admin", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 4 },
                new LookupItem { Id = 11, Name = "Customer", IsActive = true, CreationDate = DateTime.Now, LookupTypeId = 4 });

            modelBuilder.Entity<Person>().HasData(
    new Person
    {
        Id = 1,
        Name ="omar",
        Email = "omar.mahasneh.dev@gmail.com",
        PhoneNumber = "0796275753",
        Role = 10,
        Password = "omar1234"
    });

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




        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Appetizers", IsActive = true, CreationDate = DateTime.Now, Description = "Starters or small dishes served before the main course." },
        new Category { Id = 2, Name = "Main Course", IsActive = true, CreationDate = DateTime.Now, Description = "The main part of a meal." },
        new Category { Id = 3, Name = "Desserts", IsActive = true, CreationDate = DateTime.Now, Description = "Sweet dishes served at the end of a meal." }
            );

            // Seed Menus
            modelBuilder.Entity<Menu>().HasData(
                new Menu { Id = 1, Title = "Lunch Special",  Name = "Lunch Special", Description = "A delicious midday meal", CreationDate = DateTime.Now },
                new Menu { Id = 2, Title = "Dinner Feast", Name = "Dinner Feast", Description = "Hearty evening meal", CreationDate = DateTime.Now }
            );

            // Configure Many-to-Many Relationship without cascading deletes
            modelBuilder.Entity<MenuCategory>()
                .HasKey(mc => new { mc.MenuId, mc.CategoryId });

            modelBuilder.Entity<MenuCategory>()
                .HasOne(mc => mc.Menu)
                .WithMany(m => m.MenuCategories)
                .HasForeignKey(mc => mc.MenuId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<MenuCategory>()
                .HasOne(mc => mc.Category)
                .WithMany(c => c.MenuCategories)
                .HasForeignKey(mc => mc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<MenuCategory>().HasData(
                new MenuCategory { MenuId = 1, CategoryId = 1 },
                new MenuCategory { MenuId = 1, CategoryId = 2 },
                new MenuCategory { MenuId = 2, CategoryId = 2 },
                new MenuCategory { MenuId = 2, CategoryId = 3 }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-G6TNHE4\\SQLEXPRESS;Initial Catalog=ERestaurant;Integrated Security=True;Trust Server Certificate=True")
                          .EnableSensitiveDataLogging();
        }

    }
    
}
