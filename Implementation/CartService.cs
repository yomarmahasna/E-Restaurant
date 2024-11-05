using E_Restaurant.Context;
using E_Restaurant.DTOs.CartDTO.Response;
using E_Restaurant.DTOs.MenuDTO.Response;
using E_Restaurant.Entities;
using E_Restaurant.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.Implementation
{
    public class CartService : ICartService
    {
        private readonly RestaurantDbContext _context;

        public CartService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<CartDTO> GetCartByUserIdAsync(int customerId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .Where(c => c.CustomerId == customerId)
                .Select(c => new CartDTO
                {
                    Id = c.Id,
                    CustomerId = c.CustomerId,
                    TotalAmount = c.TotalAmount,
                    Items = c.Items.Select(i => new MenuItemDTO
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Price = i.Price,
                        Quantity = i.Quantity
                    }).ToList()
                }).FirstOrDefaultAsync();

            return cart;
        }

        public async Task AddToCartAsync(int customerId, int menuItemId, int quantity)
        {
            // Fetch the cart for the specified customerId and include Items
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            // If the cart doesn't exist, create a new one
            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    Items = new List<MenuItem>(), // Initialize the items list
                    TotalAmount = 0,
                    CreationDate = DateTime.Now,
                    IsActive = true
                };
                await _context.Carts.AddAsync(cart);
            }

            // Fetch the menu item being added to the cart
            var menuItem = await _context.MenuItems.FindAsync(menuItemId);
            if (menuItem == null) return; 

            // Check if the item already exists in the cart
            var existingItem = cart.Items.FirstOrDefault(i => i.Id == menuItemId);
            if (existingItem != null)
            {
                // If it exists, increase the quantity
                existingItem.Quantity += quantity;
            }
            else
            {
                // If it doesn't exist, add a new item to the cart
                // Reference the existing menuItem instead of creating a new one
                cart.Items.Add(new MenuItem
                {
                    Id = menuItem.Id + 1000,
                    Name = menuItem.Name,
                    Price = menuItem.Price,
                    Description = menuItem.Description,
                    CategoryId = menuItem.CategoryId,
                    Stock = menuItem.Stock,
                    IsActive = menuItem.IsActive,
                    CreationDate = menuItem.CreationDate,
                    Quantity = quantity 
                });
            }

            // Calculate the total amount
            cart.TotalAmount = cart.Items.Sum(i => i.Price * i.Quantity);
            cart.CreationDate = DateTime.Now;
            cart.IsActive = true;
            cart.CustomerId = customerId;
            cart.Name = "";
            // Save the changes to the database
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveItemFromCartAsync(int customerId, int menuItemId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart != null)
            {
                var item = cart.Items.FirstOrDefault(i => i.Id == menuItemId);
                if (item != null)
                {
                    cart.Items.Remove(item);
                    cart.TotalAmount = cart.Items.Sum(i => i.Price * i.Quantity);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task ClearCartAsync(int customerId)
        {
            var cart = await _context.Carts.Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart != null)
            {
                cart.Items.Clear();
                cart.TotalAmount = 0;
                await _context.SaveChangesAsync();
            }
        }
    }
}
